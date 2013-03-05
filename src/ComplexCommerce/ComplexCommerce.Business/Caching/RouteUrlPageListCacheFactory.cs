//using System;
//using System.Runtime.Caching;
//using ComplexCommerce.Business.Context;

//namespace ComplexCommerce.Business.Caching
//{
//    public class RouteUrlPageListCacheFactory
//        : IRouteUrlPageListCacheFactory
//    {
//        public RouteUrlPageListCacheFactory(ObjectCache cache, ICachePolicy cachePolicy, IApplicationContext appContext)
//        {
//            if (cache == null)
//                throw new ArgumentNullException("cache");
//            if (cachePolicy == null)
//                throw new ArgumentNullException("cachePolicy");
//            if (appContext == null)
//                throw new ArgumentNullException("appContext");

//            this.cache = cache;
//            this.cachePolicy = cachePolicy;
//            this.appContext = appContext;
//        }

//        private readonly ObjectCache cache;
//        private readonly ICachePolicy cachePolicy;
//        private readonly IApplicationContext appContext;

//        #region IRouteUrlPageListCacheFactory Members

//        SingletonObjectCache<RouteUrlPageList> IRouteUrlPageListCacheFactory.Create(string key)
//        {
//            return new RouteUrlPageListCache(key, cache, cachePolicy, appContext);
//        }

//        #endregion
//    }
//}
