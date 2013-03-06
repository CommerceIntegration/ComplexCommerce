using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;

namespace ComplexCommerce.Business
{
    public interface IRouteUrlListFactory
    {
        RouteUrlPageList EmptyRouteUrlPageList();
        RouteUrlPageList GetRouteUrlPageList(int tenantId, int localeId);
    }

    public class RouteUrlListFactory
        : IRouteUrlListFactory
    {

        #region IRouteUrlListFactory Members

        public RouteUrlPageList EmptyRouteUrlPageList()
        {
            return RouteUrlPageList.EmptyRouteUrlPageList();
        }

        public RouteUrlPageList GetRouteUrlPageList(int tenantId, int localeId)
        {
            return RouteUrlPageList.GetCachedRouteUrlPageList(tenantId, localeId);
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

        internal static RouteUrlPageList GetRouteUrlPageList(int tenantId, int localeId)
        {
            return DataPortal.Fetch<RouteUrlPageList>(new Criteria { TenantId = tenantId, LocaleId = localeId });
        }

        internal static RouteUrlPageList GetCachedRouteUrlPageList(int tenantId, int localeId)
        {
            var cmd = new GetCachedRouteUrlPageListCommand(tenantId, localeId);
            cmd = DataPortal.Execute<GetCachedRouteUrlPageListCommand>(cmd);
            return cmd.RouteUrlPageList;
        }

        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var isDefaultLocale = (criteria.LocaleId == appContext.CurrentTenant.DefaultLocale.LCID);
                var list = repository.ListForTenantLocale(criteria.TenantId, criteria.LocaleId);
                foreach (var item in list)
                    Add(DataPortal.FetchChild<RouteUrlPageInfo>(item));

                IsReadOnly = true;
                RaiseListChangedEvents = rlce;
            }
        }




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
    }
}
