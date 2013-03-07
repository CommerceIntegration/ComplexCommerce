using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;

namespace ComplexCommerce.Business
{
    public interface IRouteUrlProductListFactory
    {
        RouteUrlProductList EmptyRouteUrlProductList();
        RouteUrlProductList GetRouteUrlProductList(int tenantId, int localeId);
    }

    public class RouteUrlProductListFactory
        : IRouteUrlProductListFactory
    {

        #region IRouteUrlListFactory Members

        public RouteUrlProductList EmptyRouteUrlProductList()
        {
            return RouteUrlProductList.EmptyRouteUrlProductList();
        }

        public RouteUrlProductList GetRouteUrlProductList(int tenantId, int localeId)
        {
            return RouteUrlProductList.GetCachedRouteUrlProductList(tenantId, localeId);
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

        internal static RouteUrlProductList GetRouteUrlProductList(int tenantId, int localeId)
        {
            return DataPortal.Fetch<RouteUrlProductList>(new Criteria { TenantId = tenantId, LocaleId = localeId });
        }

        internal static RouteUrlProductList GetCachedRouteUrlProductList(int tenantId, int localeId)
        {
            var cmd = new GetCachedRouteUrlProductListCommand(tenantId, localeId);
            cmd = DataPortal.Execute<GetCachedRouteUrlProductListCommand>(cmd);
            return cmd.RouteUrlProductList;
        }

        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository.ListForRouteUrl(criteria.TenantId, criteria.LocaleId);
                foreach (var item in list)
                    Add(DataPortal.FetchChild<RouteUrlProductInfo>(item));

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
