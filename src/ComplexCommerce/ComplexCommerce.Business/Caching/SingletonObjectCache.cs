using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace ComplexCommerce.Business.Caching
{
    /// <summary>
    /// Base class used to create a type safe cache that is an application-specific singleton instance
    /// based on a derived key.
    /// </summary>
    public abstract class SingletonObjectCache<T> 
        : ISingletonObjectCache<T>
    {
        public SingletonObjectCache(ObjectCache cache)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");

            this.cache = cache;
            //// TODO: Figure out if there is a way to pass this.
            //this.cache = MemoryCache.Default;
        }

        private readonly ObjectCache cache;

        public abstract string Key { get; }


        public void Set(T item, TimeSpan absoluteExpiration, TimeSpan slidingExpiration)
        {
            var policy = new CacheItemPolicy();

            policy.Priority = CacheItemPriority.NotRemovable;
            if (IsTimespanSet(absoluteExpiration))
            {
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpiration);
            }
            else if (IsTimespanSet(slidingExpiration))
            {
                policy.SlidingExpiration = slidingExpiration;
            }

            // TODO: Figure out how to add dependencies (SQL Server in particular)

            cache.Set(this.Key, item, policy);
        }

        public bool Contains()
        {
            return cache.Contains(this.Key);
        }

        public void Remove()
        {
            cache.Remove(this.Key);
        }

        public T Get()
        {
            return (T)cache.Get(this.Key);
        }

        public bool TryGetValue(out T value)
        {
            value = this.Get();
            if (value != null)
            {
                return true;
            }
            return false;
        }

        private bool IsTimespanSet(TimeSpan timeSpan)
        {
            return (!timeSpan.Equals(TimeSpan.MinValue) && !timeSpan.Equals(TimeSpan.Zero));
        }

    }
}
