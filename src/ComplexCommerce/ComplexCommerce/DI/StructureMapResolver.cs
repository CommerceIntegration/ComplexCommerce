using System;
using StructureMap;
using ComplexCommerce.Csla.DI;

namespace ComplexCommerce.DI
{
    // Container to pass the instance of the DI container to CSLA for resolving
    // CSLA dependencies
    public class StructureMapResolver :
        IResolver
    {
        private readonly IContainer container;

        public StructureMapResolver(IContainer container)
        {
            this.container = container;
        }

        #region IResolver Members

        public void Inject(object instance)
        {
            container.BuildUp(instance);
        }

        #endregion
    }
}