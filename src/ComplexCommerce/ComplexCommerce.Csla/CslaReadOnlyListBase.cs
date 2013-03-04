using System;
using CslaLibrary = Csla;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaReadOnlyListBase<T, C> :
        Persistence.ContextualReadOnlyListBase<T, C>
        where T : CslaLibrary.ReadOnlyListBase<T, C>
    {
    }
}
