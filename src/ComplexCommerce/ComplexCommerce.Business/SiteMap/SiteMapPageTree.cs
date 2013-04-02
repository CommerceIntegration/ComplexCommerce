using System;
using System.Collections.Generic;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Text;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business.SiteMap
{
    public interface ISiteMapPageTreeFactory
    {
        SiteMapPageTree GetSiteMapPageTree();
        SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId);
    }

    public class SiteMapPageTreeFactory
        : ISiteMapPageTreeFactory
    {
        public SiteMapPageTreeFactory(
            IApplicationContext appContext
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            this.appContext = appContext;
        }

        private readonly IApplicationContext appContext;

        #region ISiteMapPageTreeFactory Members

        public SiteMapPageTree GetSiteMapPageTree()
        {
            return SiteMapPageTree.GetSiteMapPageTree(
                appContext.CurrentTenant.Id, appContext.CurrentLocaleId);
        }

        public SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId)
        {
            return SiteMapPageTree.GetSiteMapPageTree(tenantId, localeId);
        }

        #endregion
    }

    public class SiteMapPageTree
        : CslaReadOnlyBase<SiteMapPageTree>
    {
        internal static SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId)
        {
            return DataPortal.Fetch<SiteMapPageTree>(new Criteria(tenantId, localeId));
        }

        public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            private set { LoadProperty(TitleProperty, value); }
        }

        public static PropertyInfo<string> MetaRobotsProperty = RegisterProperty<string>(c => c.MetaRobots);
        public string MetaRobots
        {
            get { return GetProperty(MetaRobotsProperty); }
            private set { LoadProperty(MetaRobotsProperty, value); }
        }

        public static PropertyInfo<bool> IsVisibleOnMainMenuProperty = RegisterProperty<bool>(c => c.IsVisibleOnMainMenu);
        public bool IsVisibleOnMainMenu
        {
            get { return GetProperty(IsVisibleOnMainMenuProperty); }
            private set { LoadProperty(IsVisibleOnMainMenuProperty, value); }
        }

        #region Calculated Properties

        public static PropertyInfo<string> UrlPathProperty = RegisterProperty<string>(c => c.UrlPath);
        public string UrlPath
        {
            get { return GetProperty(UrlPathProperty); }
            private set { LoadProperty(UrlPathProperty, value); }
        }

        #endregion


        public static readonly PropertyInfo<SiteMapPageList> ChildPagesProperty = RegisterProperty<SiteMapPageList>(p => p.ChildPages);
        public SiteMapPageList ChildPages
        {
            get { return GetProperty(ChildPagesProperty); }
            private set { LoadProperty(ChildPagesProperty, value); }
        }

        public static readonly PropertyInfo<SiteMapProductList> ProductsProperty = RegisterProperty<SiteMapProductList>(p => p.Products);
        public SiteMapProductList Products
        {
            get { return GetProperty(ProductsProperty); }
            private set { LoadProperty(ProductsProperty, value); }
        }

        // Used for entry point
        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var pageList = pageRepository.ListForSiteMap(criteria.TenantId, criteria.LocaleId);
                var productList = productRepository.ListForSiteMap(criteria.TenantId, criteria.LocaleId);

                foreach (var page in pageList)
                {
                    // Find root node
                    if (page.ParentId == Guid.Empty)
                    {
                        this.Child_Fetch(page, pageList, productList);
                        break;
                    }
                }
            }
        }

        // Used for nested calls for pages
        private void Child_Fetch(SiteMapPageDto page, IEnumerable<SiteMapPageDto> pageList, IEnumerable<SiteMapProductDto> productList)
        {
            Id = page.Id;
            Title = page.Title;
            MetaRobots = page.MetaRobots;
            IsVisibleOnMainMenu = page.IsVisibleOnMainMenu;

            UrlPath = urlBuilder.BuildPath(
                page.Url, 
                page.IsUrlAbsolute, 
                page.ParentId, 
                page.TenantId,
                page.LocaleId);

            ChildPages = DataPortal.FetchChild<SiteMapPageList>(page.Id, pageList, productList);
            Products = DataPortal.FetchChild<SiteMapProductList>(page.ContentId, productList);
        }

        #region Criteria

        [Serializable]
        private class Criteria
            : CriteriaBase<Criteria>
        {
            public Criteria(
                int tenantId,
                int localeId)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");
                if (localeId < 1)
                    throw new ArgumentOutOfRangeException("localeId");

                this.TenantId = tenantId;
                this.LocaleId = localeId;
            }

            public static readonly PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }

            public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
            public int LocaleId
            {
                get { return ReadProperty(LocaleIdProperty); }
                private set { LoadProperty(LocaleIdProperty, value); }
            }
        }

        #endregion

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IPageRepository pageRepository;
        public IPageRepository PageRepository
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.pageRepository != null)
                {
                    throw new InvalidOperationException();
                }
                this.pageRepository = value;
            }
        }

        [NonSerialized]
        [NotUndoable]
        private IProductRepository productRepository;
        public IProductRepository ProductRepository
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.productRepository != null)
                {
                    throw new InvalidOperationException();
                }
                this.productRepository = value;
            }
        }

        [NonSerialized]
        [NotUndoable]
        private IUrlBuilder urlBuilder;
        public IUrlBuilder UrlBuilder
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.urlBuilder != null)
                {
                    throw new InvalidOperationException();
                }
                this.urlBuilder = value;
            }
        }

        #endregion
    }
}
