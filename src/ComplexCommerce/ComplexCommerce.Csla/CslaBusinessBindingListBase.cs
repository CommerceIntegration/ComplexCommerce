using System;
using CslaLibrary = Csla;
using Csla.Core;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public class CslaBusinessBindingListBase<T, C> :
        Persistence.ContextualBusinessBindingListBase<T, C>
        where T : CslaLibrary.BusinessBindingListBase<T, C>
        where C : IEditableBusinessObject
    {
    }
}
