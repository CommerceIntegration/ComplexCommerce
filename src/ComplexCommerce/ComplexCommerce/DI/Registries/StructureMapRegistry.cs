using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Csla.DI;

namespace ComplexCommerce.DI.Registries
{
    internal class StructureMapRegistry
        : Registry
    {
        public StructureMapRegistry(IContainer container)
        {
            this.For<IContainer>()
                .Use(container);

            this.For<IDependencyInjectionContainer>()
                .Singleton()
                .Use<StructureMapContainer>();

            this.For<IResolver>()
                .Singleton()
                .Use<StructureMapResolver>();
        }
    }
}