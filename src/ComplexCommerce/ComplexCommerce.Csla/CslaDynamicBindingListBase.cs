using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
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
