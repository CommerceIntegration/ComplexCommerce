using System;

namespace ComplexCommerce.Business.Caching
{
    public interface IMicroObjectCache<T>
    {
        bool Contains(string key);
        T GetOrAdd(string key, Func<T> loadFunction);
        void Remove(string key);
    }
}
