using System;
using System.Threading;
using System.Runtime.Caching;

namespace ComplexCommerce.Business.Caching
{
    public class MicroObjectCache<T> 
        : IMicroObjectCache<T>
    {
        public MicroObjectCache(ObjectCache cache, ICachePolicy cachePolicy)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");
            if (cachePolicy == null)
                throw new ArgumentNullException("cachePolicy");

            this.cache = cache;
            this.cachePolicy = cachePolicy;
        }

        private readonly ObjectCache cache;
        private readonly ICachePolicy cachePolicy;
        private ReaderWriterLockSlim synclock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public bool Contains(string key)
        {
            return cache.Contains(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public T GetOrAdd(string key, Func<T> loadFunction)
        {
            LazyLock lazy;
            bool success;

            synclock.EnterReadLock();
            try
            {
                success = this.TryGetValue(key, out lazy);
            }
            finally
            {
                synclock.ExitReadLock();
            }

            if (!success)
            {
                synclock.EnterWriteLock();
                try
                {
                    if (!this.TryGetValue(key, out lazy))
                    {
                        lazy = new LazyLock();
                        this.Add(key, lazy);
                    }
                }
                finally
                {
                    synclock.ExitWriteLock();
                }
            }

            return lazy.Get(loadFunction);
        }


        private LazyLock Get(string key)
        {
            return (LazyLock)cache.Get(key);
        }

        private bool TryGetValue(string key, out LazyLock value)
        {
            value = this.Get(key);
            if (value != null)
            {
                return true;
            }
            return false;
        }

        private void Add(string key, LazyLock item)
        {
            var policy = new CacheItemPolicy();

            policy.Priority = CacheItemPriority.NotRemovable;
            if (IsTimespanSet(cachePolicy.AbsoluteExpiration))
            {
                policy.AbsoluteExpiration = DateTimeOffset.Now.Add(cachePolicy.AbsoluteExpiration);
            }
            else if (IsTimespanSet(cachePolicy.SlidingExpiration))
            {
                policy.SlidingExpiration = cachePolicy.SlidingExpiration;
            }

            cache.Add(key, item, policy);
        }

        private bool IsTimespanSet(TimeSpan timeSpan)
        {
            return (!timeSpan.Equals(TimeSpan.MinValue));
        }
    }
}
