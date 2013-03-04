using System;
using Csla.Core;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaDynamicBindingListBase<T> :
        Persistence.ContextualDynamicBindingListBase<T>
        where T : IEditableBusinessObject, IUndoableObject, ISavable
    {
    }
}
