//using System;
//using System.Runtime.Caching;
//using ComplexCommerce.Business.Context;

//namespace ComplexCommerce.Business.Caching
//{
//    public class RouteUrlPageListCache
//        : SingletonObjectCache<RouteUrlPageList>
//    {
//        public RouteUrlPageListCache(string key, ObjectCache cache, ICachePolicy cachePolicy, IApplicationContext appContext)
//            : base(cache, cachePolicy)
//        {
//            if (string.IsNullOrEmpty(key))
//                throw new ArgumentNullException("key");
//            if (appContext == null)
//                throw new ArgumentNullException("appContext");

//            this.appContext = appContext;
//        }

//        private readonly string key;
//        private readonly IApplicationContext appContext;

//        public override string Key
//        {
//            get 
//            {
//                return this.key;
//                //// TODO: Need to use TenantLocaleId for the key, not TenantId
//                //var tenant = appContext.CurrentTenant;
//                //return "__CC_RouteUrlPageList_" + tenant.Id; 
//            }
//        }
//    }
//}
