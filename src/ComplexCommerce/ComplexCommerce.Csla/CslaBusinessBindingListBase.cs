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
    public class CslaBusinessBindingListBase<T, C> :
        Persistence.ContextualBusinessBindingListBase<T, C>
        where T : BusinessBindingListBase<T, C>
        where C : IEditableBusinessObject
    {
    }
}
