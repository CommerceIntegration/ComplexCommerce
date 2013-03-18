using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Rules;

namespace ComplexCommerce.Business
{
    public interface ISiteMapPageTreeFactory
    {
        //SiteMapPageTree EmptySiteMapPageTree();
        SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId);
    }

    public class SiteMapPageTreeFactory
        : ISiteMapPageTreeFactory
    {

        #region ISiteMapPageTreeFactory Members

        //public SiteMapPageTree EmptySiteMapPageTree()
        //{
        //    return SiteMapPageTree.EmptyRouteUrlPageList();
        //}

        public SiteMapPageTree GetSiteMapPageTree(int tenantId, int localeId)
        {
            return SiteMapPageTree.GetSiteMapPageTree(tenantId, localeId);
        }

        #endregion
    }

    public class SiteMapPageTree
        : CslaReadOnlyBase<SiteMapPageTree>
    {
        public static SiteMapPageTree GetSiteMapPageTree(int storeId, int localeId)
        {
            var criteria = new Criteria() { StoreId = storeId, LocaleId = localeId };
            return DataPortal.Fetch<SiteMapPageTree>(criteria);
        }

        public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        public static PropertyInfo<ContentTypeEnum> ContentTypeProperty = RegisterProperty<ContentTypeEnum>(c => c.ContentType);
        public ContentTypeEnum ContentType
        {
            get { return GetProperty(ContentTypeProperty); }
            private set { LoadProperty(ContentTypeProperty, value); }
        }

        public static PropertyInfo<Guid> ContentIdProperty = RegisterProperty<Guid>(c => c.ContentId);
        public Guid ContentId
        {
            get { return GetProperty(ContentIdProperty); }
            private set { LoadProperty(ContentIdProperty, value); }
        }

        public static PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            private set { LoadProperty(TitleProperty, value); }
        }

        public static PropertyInfo<string> RouteUrlProperty = RegisterProperty<string>(c => c.RouteUrl);
        public string RouteUrl
        {
            get { return GetProperty(RouteUrlProperty); }
            private set { LoadProperty(RouteUrlProperty, value); }
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




        

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // Route URL
            BusinessRules.AddRule(new UrlPathTrailingSlashRule(RouteUrlProperty) { Priority = 2 });
            BusinessRules.AddRule(new UrlPathLeadingSlashRule(RouteUrlProperty) { Priority = 3 });
            BusinessRules.AddRule(new UrlPathLocaleRule(RouteUrlProperty, LocaleIdProperty, appContext) { Priority = 4 });

            //// Canonical URL
            //BusinessRules.AddRule(new UrlPathProductRule(CanonicalRouteUrlProperty, DefaultCategoryRouteUrlProperty, ProductUrlSlugProperty) { Priority = 1 });
            //BusinessRules.AddRule(new UrlPathTrailingSlashRule(CanonicalRouteUrlProperty) { Priority = 2 });
            //BusinessRules.AddRule(new UrlPathLeadingSlashRule(CanonicalRouteUrlProperty) { Priority = 3 });
            //BusinessRules.AddRule(new UrlPathLocaleRule(CanonicalRouteUrlProperty, LocaleIdProperty, appContext) { Priority = 4 });
        }

        // Used for entry point
        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var pageList = pageRepository.ListForSiteMap(criteria.StoreId, criteria.LocaleId);
                var productList = productRepository.ListForSiteMap(criteria.StoreId, criteria.LocaleId);

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
            LocaleId = page.LocaleId;
            ContentType = (ContentTypeEnum)page.ContentType;
            ContentId = page.ContentId;
            Title = page.Title;
            RouteUrl = page.RouteUrl;
            MetaRobots = page.MetaRobots;
            IsVisibleOnMainMenu = page.IsVisibleOnMainMenu;

            ChildPages = DataPortal.FetchChild<SiteMapPageList>(page.Id, pageList, productList);
            Products = DataPortal.FetchChild<SiteMapProductList>(page.ContentId, productList);

            // Force the BusinessRules to execute
            this.BusinessRules.CheckRules();
        }

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

        #endregion



        // TODO: Determine best location for criteria class
        [Serializable()]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<int> StoreIdProperty = RegisterProperty<int>(c => c.StoreId);
            public int StoreId
            {
                get { return ReadProperty(StoreIdProperty); }
                set { LoadProperty(StoreIdProperty, value); }
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
