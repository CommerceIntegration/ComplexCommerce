using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
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
