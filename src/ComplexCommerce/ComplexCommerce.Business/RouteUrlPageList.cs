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
                var list = parentUrlPageListFactory.GetParentUrlPageList(criteria.TenantId, criteria.LocaleId, criteria.DefaultLocaleId);

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
    }
}
