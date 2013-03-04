using System;
using CslaLibrary = Csla;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public class CslaReadOnlyBindingListBase<T, C> :
        Persistence.ContextualReadOnlyBindingListBase<T, C>
        where T : CslaLibrary.ReadOnlyBindingListBase<T, C>
    {
    }
}
