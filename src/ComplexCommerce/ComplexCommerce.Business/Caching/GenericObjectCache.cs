//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Runtime.Caching;

//namespace ComplexCommerce.Business.Caching
//{
//    public class GenericObjectCache<T>
//    {
//        public GenericObjectCache(

//            )
//        {
//            // TODO: Figure out if there is a way to pass this.
//            this.cache = MemoryCache.Default;
//        }

//        private readonly ObjectCache cache;

        
//        public void Set(string key, T item, TimeSpan absoluteExpiration, TimeSpan slidingExpiration)
//        {
//            var policy = new CacheItemPolicy();

//            policy.Priority = CacheItemPriority.NotRemovable;
//            if (IsTimespanSet(absoluteExpiration))
//            {
//                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpiration);
//            }
//            else if (IsTimespanSet(slidingExpiration))
//            {
//                policy.SlidingExpiration = slidingExpiration;
//            }

//            // TODO: Figure out how to add dependencies (SQL Server in particular)

//            cache.Set(key, item, policy);
//        }

//        public bool Contains(string key)
//        {
//            return cache.Contains(key);
//        }

//        public void Remove(string key)
//        {
//            cache.Remove(key);
//        }

//        public T Get(string key)
//        {
//            return (T)cache.Get(key);
//        }

//        public bool TryGetValue(string key, out T value)
//        {
//            value = this.Get(key);
//            if (value != null)
//            {
//                return true;
//            }
//            return false;
//        }


//        private bool IsTimespanSet(TimeSpan timeSpan)
//        {
//            return (!timeSpan.Equals(TimeSpan.MinValue) && !timeSpan.Equals(TimeSpan.Zero));
//        }

//    }
//}
