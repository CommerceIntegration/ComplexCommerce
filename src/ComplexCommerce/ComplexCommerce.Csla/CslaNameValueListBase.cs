using System;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaNameValueListBase<K, V> :
        Persistence.ContextualNameValueListBase<K, V>
    {
    }
}
