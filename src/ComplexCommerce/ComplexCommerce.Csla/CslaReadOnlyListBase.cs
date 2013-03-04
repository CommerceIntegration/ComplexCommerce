using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CslaLibrary = Csla;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class ReadOnlyListBase<T, C> :
        Persistence.ContextualReadOnlyListBase<T, C>
        where T : CslaLibrary.ReadOnlyListBase<T, C>
    {
    }
}
