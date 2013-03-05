using System;
namespace ComplexCommerce.Business.Caching
{
    public interface ISingletonObjectCache<T>
    {
        bool Contains();
        string Key { get; }
        void Remove();
        T GetOrLoad(Func<T> loadFunction);
    }
}
