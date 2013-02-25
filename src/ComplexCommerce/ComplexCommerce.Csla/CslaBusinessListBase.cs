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
    public class CslaBusinessListBase<T, C> :
        Persistence.ContextualBusinessListBase<T, C>
        where T : BusinessListBase<T, C>
        where C : IEditableBusinessObject
    {
    }
}
