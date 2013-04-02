using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Text;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.Business
{
    public interface IRouteUrlPageListFactory
    {
        RouteUrlPageList EmptyRouteUrlPageList();
        RouteUrlPageList GetRouteUrlPageList(int tenantId, int localeId, int defaultLocaleId);
    }

    public class RouteUrlPageListFactory
        : IRouteUrlPageListFactory
    {

        #region IRouteUrlListFactory Members

        public RouteUrlPageList EmptyRouteUrlPageList()
        {
            return RouteUrlPageList.EmptyRouteUrlPageList();
        }

        public RouteUrlPageList GetRouteUrlPageList(int tenantId, int localeId, int defaultLocaleId)
        {
            return RouteUrlPageList.GetCachedRouteUrlPageList(tenantId, localeId, defaultLocaleId);
        }

        #endregion
    }

    [Serializable]
    public class RouteUrlPageList
        : CslaReadOnlyListBase<RouteUrlPageList, RouteUrlPageInfo>
    {
        internal static RouteUrlPageList EmptyRouteUrlPageList()
        {
            return new RouteUrlPageList();
        }

        internal static RouteUrlPageList GetRouteUrlPageList(int tenantId, int localeId, int defaultLocaleId)
        {
            return DataPortal.Fetch<RouteUrlPageList>(new TenantLocaleCriteria(tenantId, localeId, defaultLocaleId));
        }

        internal static RouteUrlPageList GetCachedRouteUrlPageList(int tenantId, int localeId, int defaultLocaleId)
        {
            var cmd = new GetCachedRouteUrlPageListCommand(tenantId, localeId, defaultLocaleId);
            cmd = DataPortal.Execute<GetCachedRouteUrlPageListCommand>(cmd);
            return cmd.RouteUrlPageList;
        }

        private void DataPortal_Fetch(TenantLocaleCriteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                // This list will automatically be request cached
                var list = parentUrlPageListFactory.GetParentUrlPageList(criteria.TenantId);

                foreach (var item in list)
                    Add(DataPortal.FetchChild<RouteUrlPageInfo>(item, criteria));

                IsReadOnly = true;
                RaiseListChangedEvents = rlce;
            }
        }

        #region Dependency Injection

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

        #region GetCachedRouteUrlPageListCommand

        [Serializable]
        private class GetCachedRouteUrlPageListCommand
            : CslaCommandBase<GetCachedRouteUrlPageListCommand>
        {
            public GetCachedRouteUrlPageListCommand(int tenantId, int localeId, int defaultLocaleId)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");
                if (localeId < 1)
                    throw new ArgumentOutOfRangeException("localeId");
                if (defaultLocaleId < 1)
                    throw new ArgumentOutOfRangeException("defaultLocaleId");

                this.TenantId = tenantId;
                this.LocaleId = localeId;
                this.DefaultLocaleId = defaultLocaleId;
            }


            public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }

            public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
            public int LocaleId
            {
                get { return ReadProperty(LocaleIdProperty); }
                private set { LoadProperty(LocaleIdProperty, value); }
            }

            public static PropertyInfo<int> DefaultLocaleIdProperty = RegisterProperty<int>(c => c.DefaultLocaleId);
            public int DefaultLocaleId
            {
                get { return ReadProperty(DefaultLocaleIdProperty); }
                private set { LoadProperty(DefaultLocaleIdProperty, value); }
            }

            public static PropertyInfo<RouteUrlPageList> RouteUrlPageListProperty = RegisterProperty<RouteUrlPageList>(c => c.RouteUrlPageList);
            public RouteUrlPageList RouteUrlPageList
            {
                get { return ReadProperty(RouteUrlPageListProperty); }
                private set { LoadProperty(RouteUrlPageListProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                //var key = "__ML_RouteUrlPageList_" + this.TenantId + "_" + this.LocaleId + "__";
                var key = "__ML_RouteUrlPageList_" + this.TenantId + "__";
                this.RouteUrlPageList = cache.GetOrAdd(key,
                    () => RouteUrlPageList.GetRouteUrlPageList(this.TenantId, this.LocaleId, this.DefaultLocaleId));
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IMicroObjectCache<RouteUrlPageList> cache;
            public IMicroObjectCache<RouteUrlPageList> Cache
            {
                set
                {
                    // Don't allow the value to be set to null
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }
                    // Don't allow the value to be set more than once
                    if (this.cache != null)
                    {
                        throw new InvalidOperationException();
                    }
                    this.cache = value;
                }
            }

            #endregion

        }

        #endregion

    }
}
