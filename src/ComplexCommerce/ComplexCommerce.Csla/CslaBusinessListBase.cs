using System;
using CslaLibrary = Csla;
using Csla.Core;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public class CslaBusinessListBase<T, C> :
        Persistence.ContextualBusinessListBase<T, C>
        where T : CslaLibrary.BusinessListBase<T, C>
        where C : IEditableBusinessObject
    {
    }
}
