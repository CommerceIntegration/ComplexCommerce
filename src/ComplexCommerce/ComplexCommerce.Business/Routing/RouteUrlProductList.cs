using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Context;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.Business.Routing
{
    public interface IRouteUrlProductListFactory
    {
        RouteUrlProductList EmptyRouteUrlProductList();
        RouteUrlProductList GetRouteUrlProductList(int tenantId);
    }

    public class RouteUrlProductListFactory
        : IRouteUrlProductListFactory
    {
        #region IRouteUrlListFactory Members

        public RouteUrlProductList EmptyRouteUrlProductList()
        {
            return RouteUrlProductList.EmptyRouteUrlProductList();
        }

        public RouteUrlProductList GetRouteUrlProductList(int tenantId)
        {
            return RouteUrlProductList.GetCachedRouteUrlProductList(tenantId);
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

        internal static RouteUrlProductList GetRouteUrlProductList(int tenantId)
        {
            return DataPortal.Fetch<RouteUrlProductList>(tenantId);
        }

        internal static RouteUrlProductList GetCachedRouteUrlProductList(int tenantId)
        {
            var cmd = new GetCachedRouteUrlProductListCommand(tenantId);
            cmd = DataPortal.Execute<GetCachedRouteUrlProductListCommand>(cmd);
            return cmd.RouteUrlProductList;
        }

        private void DataPortal_Fetch(int tenantId)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository.ListForRouteUrl(tenantId);
                foreach (var item in list)
                    Add(DataPortal.FetchChild<RouteUrlProductInfo>(item));

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

        #region GetCachedRouteUrlProductListCommand

        [Serializable]
        private class GetCachedRouteUrlProductListCommand
            : CslaCommandBase<GetCachedRouteUrlProductListCommand>
        {
            public GetCachedRouteUrlProductListCommand(int tenantId)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");

                this.TenantId = tenantId;
            }

            public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }

            public static PropertyInfo<RouteUrlProductList> RouteUrlProductListProperty = RegisterProperty<RouteUrlProductList>(c => c.RouteUrlProductList);
            public RouteUrlProductList RouteUrlProductList
            {
                get { return ReadProperty(RouteUrlProductListProperty); }
                private set { LoadProperty(RouteUrlProductListProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                var key = "__ML_RouteUrlProductList_" + this.TenantId + "__";
                this.RouteUrlProductList = cache.GetOrAdd(key,
                    () => RouteUrlProductList.GetRouteUrlProductList(this.TenantId));
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IMicroObjectCache<RouteUrlProductList> cache;
            public IMicroObjectCache<RouteUrlProductList> Cache
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
