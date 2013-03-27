using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business
{
    public interface IRouteUrlProductListFactory
    {
        RouteUrlProductList EmptyRouteUrlProductList();
        //RouteUrlProductList GetRouteUrlProductList();
        RouteUrlProductList GetRouteUrlProductList(int tenantId, int localeId, int defaultLocaleId);
    }

    public class RouteUrlProductListFactory
        : IRouteUrlProductListFactory
    {
        //public RouteUrlProductListFactory(
        //    IApplicationContext appContext
        //    )
        //{
        //    if (appContext == null)
        //        throw new ArgumentNullException("appContext");
        //    this.appContext = appContext;
        //}

        //private readonly IApplicationContext appContext;

        #region IRouteUrlListFactory Members

        public RouteUrlProductList EmptyRouteUrlProductList()
        {
            return RouteUrlProductList.EmptyRouteUrlProductList();
        }

        //public RouteUrlProductList GetRouteUrlProductList()
        //{
        //    return RouteUrlProductList.GetCachedRouteUrlProductList(
        //        appContext.CurrentTenant.Id, appContext.CurrentLocaleId, appContext.CurrentTenant.DefaultLocale.LCID);
        //}

        public RouteUrlProductList GetRouteUrlProductList(int tenantId, int localeId, int defaultLocaleId)
        {
            return RouteUrlProductList.GetCachedRouteUrlProductList(tenantId, localeId, defaultLocaleId);
        }

        #endregion
    }

    [Serializable]
    public class RouteUrlProductList
        : CslaReadOnlyListBase<RouteUrlProductList, RouteUrlProductInfo>
    {

        internal static RouteUrlProductList EmptyRouteUrlProductList()
        {
            return new RouteUrlProductList();
        }

        internal static RouteUrlProductList GetRouteUrlProductList(int tenantId, int localeId, int defaultLocaleId)
        {
            return DataPortal.Fetch<RouteUrlProductList>(new TenantLocaleCriteria(tenantId, localeId, defaultLocaleId));
        }

        internal static RouteUrlProductList GetCachedRouteUrlProductList(int tenantId, int localeId, int defaultLocaleId)
        {
            var cmd = new GetCachedRouteUrlProductListCommand(tenantId, localeId, defaultLocaleId);
            cmd = DataPortal.Execute<GetCachedRouteUrlProductListCommand>(cmd);
            return cmd.RouteUrlProductList;
        }

        private void DataPortal_Fetch(TenantLocaleCriteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository.ListForRouteUrl(criteria.TenantId, criteria.LocaleId);
                foreach (var item in list)
                    Add(DataPortal.FetchChild<RouteUrlProductInfo>(item, criteria));

                IsReadOnly = true;
                RaiseListChangedEvents = rlce;
            }
        }

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IProductRepository repository;
        public IProductRepository Repository
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
    }
}
