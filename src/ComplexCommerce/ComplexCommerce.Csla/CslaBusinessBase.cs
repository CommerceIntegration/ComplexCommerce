using System;
using CslaLibrary = Csla;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaBusinessBase<T> :
        Persistence.ContextualBusinessBase<T>
        where T : CslaLibrary.BusinessBase<T>
    {
    }
}
