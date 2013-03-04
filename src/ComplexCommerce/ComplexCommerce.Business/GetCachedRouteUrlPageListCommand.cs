using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class GetCachedRouteUrlPageListCommand
        : CslaCommandBase<GetCachedRouteUrlPageListCommand>
    {
        public GetCachedRouteUrlPageListCommand(int tenantId, int localeId, TimeSpan cacheExpiration, bool useAbsolute)
        {
            if (tenantId < 1)
                throw new ArgumentOutOfRangeException("tenantId");
            if (localeId < 1)
                throw new ArgumentOutOfRangeException("localeId");
            if (cacheExpiration == null)
                throw new ArgumentNullException("cacheExpiration");

            this.TenantId = tenantId;
            this.LocaleId = localeId;

            if (useAbsolute)
            {
                this.AbsoluteExpiration = cacheExpiration;
                this.SlidingExpiration = TimeSpan.Zero;
            }
            else
            {
                this.AbsoluteExpiration = TimeSpan.Zero;
                this.SlidingExpiration = cacheExpiration;
            }
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

        public static PropertyInfo<TimeSpan> AbsoluteExpirationProperty = RegisterProperty<TimeSpan>(c => c.AbsoluteExpiration);
        public TimeSpan AbsoluteExpiration
        {
            get { return ReadProperty(AbsoluteExpirationProperty); }
            private set { LoadProperty(AbsoluteExpirationProperty, value); }
        }

        public static PropertyInfo<TimeSpan> SlidingExpirationProperty = RegisterProperty<TimeSpan>(c => c.SlidingExpiration);
        public TimeSpan SlidingExpiration
        {
            get { return ReadProperty(SlidingExpirationProperty); }
            private set { LoadProperty(SlidingExpirationProperty, value); }
        }

        public static PropertyInfo<RouteUrlPageList> RouteUrlPageListProperty = RegisterProperty<RouteUrlPageList>(c => c.RouteUrlPageList);
        public RouteUrlPageList RouteUrlPageList
        {
            get { return ReadProperty(RouteUrlPageListProperty); }
            private set { LoadProperty(RouteUrlPageListProperty, value); }
        }

        /// <summary>
        /// We work with the cache on the server side 
        /// </summary>
        protected override void DataPortal_Execute()
        {
            var list = cache.Get();
            if (list == null)
            {
                list = RouteUrlPageList.GetRouteUrlPageList(this.TenantId, this.LocaleId);
                if (list != null)
                {
                    cache.Set(list, this.AbsoluteExpiration, this.SlidingExpiration);
                }
            }
            this.RouteUrlPageList = list;
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
