using System;
using System.Threading;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class GetCachedRouteUrlPageListCommand
        : CslaCommandBase<GetCachedRouteUrlPageListCommand>
    {
        public GetCachedRouteUrlPageListCommand(int tenantId, int localeId)
        {
            if (tenantId < 1)
                throw new ArgumentOutOfRangeException("tenantId");
            if (localeId < 1)
                throw new ArgumentOutOfRangeException("localeId");
            
            this.TenantId = tenantId;
            this.LocaleId = localeId;
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
            this.RouteUrlPageList = cache.GetOrLoad(
                () => RouteUrlPageList.GetRouteUrlPageList(this.TenantId, this.LocaleId));
        }

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private ISingletonObjectCache<RouteUrlPageList> cache;
        public ISingletonObjectCache<RouteUrlPageList> Cache
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
}
