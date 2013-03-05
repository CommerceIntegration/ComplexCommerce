using System;
namespace ComplexCommerce.Business.Caching
{
    public interface ISingletonObjectCache<T>
    {
        string Key { get; }
        bool HasValue();
        void Clear();
        T GetOrLoad(Func<T> loadFunction);
    }
}
