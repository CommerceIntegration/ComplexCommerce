using System;
using CslaLibrary = Csla;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaReadOnlyBase<T> :
        Persistence.ContextualReadOnlyBase<T>
        where T : CslaLibrary.ReadOnlyBase<T>
    {
    }
}
