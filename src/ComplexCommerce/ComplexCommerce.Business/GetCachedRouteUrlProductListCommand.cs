//using System;
//using Csla;
//using ComplexCommerce.Csla;
//using ComplexCommerce.Data;
//using ComplexCommerce.Business.Caching;

//namespace ComplexCommerce.Business
//{
//    [Serializable]
//    public class GetCachedRouteUrlProductListCommand
//        : CslaCommandBase<GetCachedRouteUrlProductListCommand>
//    {
//        public GetCachedRouteUrlProductListCommand(int tenantId, int localeId, int defaultLocaleId)
//        {
//            if (tenantId < 1)
//                throw new ArgumentOutOfRangeException("tenantId");
//            if (localeId < 1)
//                throw new ArgumentOutOfRangeException("localeId");
//            if (defaultLocaleId < 1)
//                throw new ArgumentOutOfRangeException("defaultLocaleId");
            
//            this.TenantId = tenantId;
//            this.LocaleId = localeId;
//            this.DefaultLocaleId = defaultLocaleId;
//        }

//         public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
//        public int TenantId
//        {
//            get { return ReadProperty(TenantIdProperty); }
//            private set { LoadProperty(TenantIdProperty, value); }
//        }

//        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
//        public int LocaleId
//        {
//            get { return ReadProperty(LocaleIdProperty); }
//            private set { LoadProperty(LocaleIdProperty, value); }
//        }

//        public static PropertyInfo<int> DefaultLocaleIdProperty = RegisterProperty<int>(c => c.DefaultLocaleId);
//        public int DefaultLocaleId
//        {
//            get { return ReadProperty(DefaultLocaleIdProperty); }
//            private set { LoadProperty(DefaultLocaleIdProperty, value); }
//        }

//        public static PropertyInfo<RouteUrlProductList> RouteUrlProductListProperty = RegisterProperty<RouteUrlProductList>(c => c.RouteUrlProductList);
//        public RouteUrlProductList RouteUrlProductList
//        {
//            get { return ReadProperty(RouteUrlProductListProperty); }
//            private set { LoadProperty(RouteUrlProductListProperty, value); }
//        }

//        /// <summary>
//        /// We work with the cache on the server side of the DataPortal
//        /// </summary>
//        protected override void DataPortal_Execute()
//        {
//            var key = "__ML_RouteUrlProductList_" + this.TenantId + "__";
//            this.RouteUrlProductList = cache.GetOrAdd(key,
//                () => RouteUrlProductList.GetRouteUrlProductList(this.TenantId, this.LocaleId, this.DefaultLocaleId));
//        }

//        #region Dependency Injection

//        [NonSerialized]
//        [NotUndoable]
//        private IMicroObjectCache<RouteUrlProductList> cache;
//        public IMicroObjectCache<RouteUrlProductList> Cache
//        {
//            set
//            {
//                // Don't allow the value to be set to null
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                // Don't allow the value to be set more than once
//                if (this.cache != null)
//                {
//                    throw new InvalidOperationException();
//                }
//                this.cache = value;
//            }
//        }

//        #endregion
//    }
//}
