using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Text;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business
{
    public interface ISiteMapPageTreeFactory
    {
        SiteMapPageTree GetSiteMapPageTree();
        SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId, int defaultLocaleId);
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
                appContext.CurrentTenant.Id, appContext.CurrentLocaleId, appContext.CurrentTenant.DefaultLocale.LCID);
        }

        public SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId, int defaultLocaleId)
        {
            return SiteMapPageTree.GetSiteMapPageTree(tenantId, localeId, defaultLocaleId);
        }

        #endregion
    }

    public class SiteMapPageTree
        : CslaReadOnlyBase<SiteMapPageTree>
    {
        internal static SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId, int defaultLocaleId)
        {
            return DataPortal.Fetch<SiteMapPageTree>(new TenantLocaleCriteria(tenantId, localeId, defaultLocaleId));
        }

        //public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
        //public Guid Id
        //{
        //    get { return GetProperty(IdProperty); }
        //    private set { LoadProperty(IdProperty, value); }
        //}

        public static PropertyInfo<Guid> PageLocaleIdProperty = RegisterProperty<Guid>(c => c.PageLocaleId);
        public Guid PageLocaleId
        {
            get { return GetProperty(PageLocaleIdProperty); }
            private set { LoadProperty(PageLocaleIdProperty, value); }
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
        private void DataPortal_Fetch(TenantLocaleCriteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var pageList = parentUrlPageListFactory.GetParentUrlPageList(criteria.TenantId, criteria.LocaleId, criteria.DefaultLocaleId);
                var productList = productRepository.ListForSiteMap(criteria.TenantId, criteria.LocaleId);

                foreach (var page in pageList)
                {
                    // Find root node
                    if (page.ParentId == Guid.Empty)
                    {
                        this.Child_Fetch(page, pageList, productList, criteria);
                        break;
                    }
                }
            }
        }

        // Used for nested calls for pages
        private void Child_Fetch(ParentUrlPageInfo page, IEnumerable<ParentUrlPageInfo> pageList, IEnumerable<SiteMapProductDto> productList, ITenantLocale tenantLocale)
        {
            //Id = page.Id;
            PageLocaleId = page.PageLocaleId;
            Title = page.Title;
            MetaRobots = page.MetaRobots;
            IsVisibleOnMainMenu = page.IsVisibleOnMainMenu;

            UrlPath = urlBuilder.BuildPath(
                page.Url, 
                page.IsUrlAbsolute, 
                page.ParentId, 
                tenantLocale);

            ChildPages = DataPortal.FetchChild<SiteMapPageList>(page.Id, pageList, productList, tenantLocale);
            Products = DataPortal.FetchChild<SiteMapProductList>(page.ContentId, productList, tenantLocale);
        }

        #region Dependency Injection

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

        [NonSerialized]
        [NotUndoable]
        private IParentUrlPageListFactory parentUrlPageListFactory;
        public IParentUrlPageListFactory ParentUrlPageListFactory
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.parentUrlPageListFactory != null)
                {
                    throw new InvalidOperationException();
                }
                this.parentUrlPageListFactory = value;
            }
        }

        #endregion
    }
}
