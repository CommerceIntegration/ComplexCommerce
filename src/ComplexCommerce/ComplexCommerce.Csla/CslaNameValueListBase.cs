using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaNameValueListBase<K, V> :
        Persistence.ContextualNameValueListBase<K, V>
    {
    }
}
