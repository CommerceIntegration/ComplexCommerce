using System;
namespace ComplexCommerce.Business.Caching
{
    public interface ISingletonObjectCache<T>
    {
        bool Contains();
        T Get();
        string Key { get; }
        void Remove();
        void Set(T item, TimeSpan absoluteExpiration, TimeSpan slidingExpiration);
        bool TryGetValue(out T value);
    }
}
