using System;
using CslaLibrary = Csla;

namespace ComplexCommerce.Csla
{
    [Serializable]
    public abstract class CslaCommandBase<T> :
        Persistence.ContextualCommandBase<T>
        where T : CslaLibrary.CommandBase<T>
    {
    }
}
