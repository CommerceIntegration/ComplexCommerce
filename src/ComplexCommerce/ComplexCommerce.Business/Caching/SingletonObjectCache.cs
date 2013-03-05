using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Caching;

namespace ComplexCommerce.Business.Caching
{
    /// <summary>
    /// Base class used to create a type safe cache that is an application-specific singleton instance
    /// based on a derived key.
    /// </summary>
    /// <remarks>
    /// Caching strategy inspired by the following post:
    /// http://www.superstarcoders.com/blogs/posts/micro-caching-in-asp-net.aspx
    /// </remarks>
    public abstract class SingletonObjectCache<T> 
        : ISingletonObjectCache<T>
    {
        public SingletonObjectCache(ObjectCache cache, ICachePolicy cachePolicy)
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

        public abstract string Key { get; }


        public bool Contains()
        {
            return cache.Contains(this.Key);
        }

        public void Remove()
        {
            cache.Remove(this.Key);
        }

        public T GetOrLoad(Func<T> loadFunction)
        {
            LazyLock lazy;
            bool success;

            synclock.EnterReadLock();

            try
            {
                success = this.TryGetValue(out lazy);
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
                    if (!this.TryGetValue(out lazy))
                    {
                        //cache[this.Key] = lazy = new LazyLock();
                        lazy = new LazyLock();
                        this.Set(lazy);
                    }
                }
                finally
                {
                    synclock.ExitWriteLock();
                }
            }

            return lazy.Get(loadFunction);
        }


        private LazyLock Get()
        {
            return (LazyLock)cache.Get(this.Key);
        }

        private bool TryGetValue(out LazyLock value)
        {
            value = this.Get();
            if (value != null)
            {
                return true;
            }
            return false;
        }

        private void Set(LazyLock item)
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

            cache.Set(this.Key, item, policy);
        }

        private bool IsTimespanSet(TimeSpan timeSpan)
        {
            return (!timeSpan.Equals(TimeSpan.MinValue));
        }

    }
}
