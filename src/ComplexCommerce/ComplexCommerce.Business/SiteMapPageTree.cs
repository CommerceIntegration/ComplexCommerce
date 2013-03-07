using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;

namespace ComplexCommerce.Business
{
    public class SiteMapPageTree
        : CslaReadOnlyBase<SiteMapPageTree>
    {
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

        // TODO: Add Url property that combines the locale with the route in a business rule.
        // http://www.somesite.com.../en-us/the/route/url

        public static PropertyInfo<string> MetaRobotsProperty = RegisterProperty<string>(c => c.MetaRobots);
        public string MetaRobots
        {
            get { return GetProperty(MetaRobotsProperty); }
            private set { LoadProperty(MetaRobotsProperty, value); }
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

        public static readonly PropertyInfo<SiteMapPageList> ChildPagesProperty = RegisterProperty<SiteMapPageList>(p => p.ChildPages);
        public SiteMapPageList ChildPages
        {
            get { return GetProperty(ChildPagesProperty); }
            private set { LoadProperty(ChildPagesProperty, value); }
        }



        public static SiteMapPageTree GetSiteMapPageTree(int storeId, int localeId)
        {
            // TODO: Should the BO be aware of its locale and tenantId or just get it from ambient
            // context through a service?
            //int localeId = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
            var criteria = new Criteria() { StoreId = storeId, LocaleId = localeId };
            return DataPortal.Fetch<SiteMapPageTree>(criteria);
        }

        // Used for entry point
        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var list = repository.ListForTenantLocale(criteria.StoreId, criteria.LocaleId);

                foreach (var item in list)
                {
                    // Find root node
                    if (item.ParentId == Guid.Empty)
                    {
                        this.Child_Fetch(item, list);
                        break;
                    }
                }
            }
        }

        // Used for nested calls
        private void Child_Fetch(SiteMapPageDto item, IEnumerable<SiteMapPageDto> list)
        {
            Id = item.Id;
            LocaleId = item.LocaleId;
            Title = item.Title;
            RouteUrl = item.RouteUrl;
            MetaRobots = item.MetaRobots;
            ContentType = (ContentTypeEnum)item.ContentType;
            ContentId = item.ContentId;

            ChildPages = DataPortal.FetchChild<SiteMapPageList>(item.Id, list);
        }



        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IPageRepository repository;
        public IPageRepository Repository
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.repository != null)
                {
                    throw new InvalidOperationException();
                }
                this.repository = value;
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
