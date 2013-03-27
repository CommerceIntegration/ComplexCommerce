using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Business
{
    public interface IRouteUrlPageListFactory
    {
        RouteUrlPageList EmptyRouteUrlPageList();
        RouteUrlPageList GetRouteUrlPageList(int tenantId, int localeId);
    }

    public class RouteUrlPageListFactory
        : IRouteUrlPageListFactory
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

                // This list will automatically be request cached
                var list = parentUrlPageListFactory.GetParentUrlPageList(criteria.TenantId, criteria.LocaleId);

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
