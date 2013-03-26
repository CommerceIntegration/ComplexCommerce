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

namespace ComplexCommerce.Business
{
    public interface ISiteMapPageTreeFactory
    {
        SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId);
    }

    public class SiteMapPageTreeFactory
        : ISiteMapPageTreeFactory
    {

        #region ISiteMapPageTreeFactory Members

        public SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId)
        {
            return SiteMapPageTree.GetSiteMapPageTree(tenantId, localeId);
        }

        #endregion
    }

    public class SiteMapPageTree
        : CslaReadOnlyBase<SiteMapPageTree>
    {
        public static SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId)
        {
            var criteria = new Criteria() { TenantId = tenantId, LocaleId = localeId };
            return DataPortal.Fetch<SiteMapPageTree>(criteria);
        }

        public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        //public static PropertyInfo<Guid> ParentIdProperty = RegisterProperty<Guid>(c => c.ParentId);
        //public Guid ParentId
        //{
        //    get { return GetProperty(ParentIdProperty); }
        //    private set { LoadProperty(ParentIdProperty, value); }
        //}

        //public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        //public int LocaleId
        //{
        //    get { return GetProperty(LocaleIdProperty); }
        //    private set { LoadProperty(LocaleIdProperty, value); }
        //}

        //public static PropertyInfo<ContentTypeEnum> ContentTypeProperty = RegisterProperty<ContentTypeEnum>(c => c.ContentType);
        //public ContentTypeEnum ContentType
        //{
        //    get { return GetProperty(ContentTypeProperty); }
        //    private set { LoadProperty(ContentTypeProperty, value); }
        //}

        //public static PropertyInfo<Guid> ContentIdProperty = RegisterProperty<Guid>(c => c.ContentId);
        //public Guid ContentId
        //{
        //    get { return GetProperty(ContentIdProperty); }
        //    private set { LoadProperty(ContentIdProperty, value); }
        //}

        public static PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            private set { LoadProperty(TitleProperty, value); }
        }

        //public static PropertyInfo<string> UrlProperty = RegisterProperty<string>(c => c.Url);
        //public string Url
        //{
        //    get { return GetProperty(UrlProperty); }
        //    private set { LoadProperty(UrlProperty, value); }
        //}

        //public static PropertyInfo<bool> IsUrlAbsoluteProperty = RegisterProperty<bool>(c => c.IsUrlAbsolute);
        //public bool IsUrlAbsolute
        //{
        //    get { return GetProperty(IsUrlAbsoluteProperty); }
        //    private set { LoadProperty(IsUrlAbsoluteProperty, value); }
        //}

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




        

        //protected override void AddBusinessRules()
        //{
        //    base.AddBusinessRules();

        //    // Route URL
        //    BusinessRules.AddRule(new UrlPathPageRule(UrlPathProperty, UrlProperty, IsUrlAbsoluteProperty, ParentIdProperty, appContext) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathTrailingSlashRule(UrlPathProperty) { Priority = 2 });
        //    BusinessRules.AddRule(new UrlPathLeadingSlashRule(UrlPathProperty) { Priority = 3 });
        //    BusinessRules.AddRule(new UrlPathLocaleRule(UrlPathProperty, LocaleIdProperty, appContext) { Priority = 4 });
        //}

        // Used for entry point
        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                //var pageList = pageRepository.ListForSiteMap(criteria.TenantId, criteria.LocaleId);
                var pageList = parentUrlPageListFactory.GetParentUrlPageList(criteria.TenantId, criteria.LocaleId);
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
        private void Child_Fetch(ParentUrlPageInfo page, IEnumerable<ParentUrlPageInfo> pageList, IEnumerable<SiteMapProductDto> productList)
        {
            Id = page.Id;
            //ParentId = page.ParentId;
            //LocaleId = page.LocaleId;
            //ContentType = (ContentTypeEnum)page.ContentType;
            //ContentId = page.ContentId;
            Title = page.Title;
            //Url = page.Url;
            //IsUrlAbsolute = page.IsUrlAbsolute;
            MetaRobots = page.MetaRobots;
            IsVisibleOnMainMenu = page.IsVisibleOnMainMenu;

            UrlPath = urlBuilder.BuildPath(
                page.Url, 
                page.IsUrlAbsolute, 
                page.ParentId, 
                appContext.CurrentTenant.Id, 
                appContext.CurrentLocaleId, 
                appContext.CurrentTenant.DefaultLocale.LCID);

            ChildPages = DataPortal.FetchChild<SiteMapPageList>(page.Id, pageList, productList);
            Products = DataPortal.FetchChild<SiteMapProductList>(page.ContentId, productList);

            //// Force the BusinessRules to execute
            //this.BusinessRules.CheckRules();
        }

        #region Dependency Injection

        //[NonSerialized]
        //[NotUndoable]
        //private IPageRepository pageRepository;
        //public IPageRepository PageRepository
        //{
        //    set
        //    {
        //        // Don't allow the value to be set to null
        //        if (value == null)
        //        {
        //            throw new ArgumentNullException("value");
        //        }
        //        // Don't allow the value to be set more than once
        //        if (this.pageRepository != null)
        //        {
        //            throw new InvalidOperationException();
        //        }
        //        this.pageRepository = value;
        //    }
        //}

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
        private Context.IApplicationContext appContext;
        public Context.IApplicationContext AppContext
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.appContext != null)
                {
                    throw new InvalidOperationException();
                }
                this.appContext = value;
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


        // TODO: Make a global TenantLocaleCriteria that includes DefaultLocaleId 
        // with an interface that can be passed into child collections during population.
        // This criteria class will need to be passed into several other classes.

        // TODO: Determine best location for criteria class
        [Serializable()]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                set { LoadProperty(TenantIdProperty, value); }
            }

            public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
            public int LocaleId
            {
                get { return ReadProperty(LocaleIdProperty); }
                set { LoadProperty(LocaleIdProperty, value); }
            }
        }
    }
}
