using System;
using Csla.Core;
using Csla.Serialization.Mobile;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaDynamicListBase<T> :
        Persistence.ContextualDynamicListBase<T>
        where T : IEditableBusinessObject, IUndoableObject, ISavable, IMobileObject
    {
    }
}
