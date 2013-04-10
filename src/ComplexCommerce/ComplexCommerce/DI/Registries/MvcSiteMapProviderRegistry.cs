using System;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web.Mvc.Filters;
using MvcSiteMapProvider.Web.Compilation;
using MvcSiteMapProvider.Web.UrlResolver;
using MvcSiteMapProvider.Security;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.Visitor;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Caching;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.DI.Conventions;
using ComplexCommerce.Web.Mvc.SiteMapProvider;
using ComplexCommerce.Web.Mvc.DI;


namespace ComplexCommerce.DI.Registries
{
    internal class MvcSiteMapProviderRegistry
        : Registry
    {
        public MvcSiteMapProviderRegistry()
        {
            this.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<SiteMaps>();
                scan.WithDefaultConventions();
            });

            this.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<SiteMaps>();
                scan.AssemblyContainingType<InjectableControllerFactory>();
                scan.WithDefaultConventions();
                scan.AddAllTypesOf<IMvcContextFactory>();
                scan.AddAllTypesOf<ISiteMapCacheKeyToBuilderSetMapper>();
                scan.AddAllTypesOf<IDynamicNodeProvider>();
                scan.AddAllTypesOf<ISiteMapNodeVisibilityProvider>();
                scan.AddAllTypesOf<ISiteMapNodeUrlResolver>();
                scan.AddAllTypesOf<IDynamicNodeProviderStrategy>();
                scan.AddAllTypesOf<ISiteMapNodeUrlResolverStrategy>();
                scan.AddAllTypesOf<ISiteMapNodeVisibilityProviderStrategy>();
                scan.AddAllTypesOf<IFilterProvider>();
                scan.AddAllTypesOf<IControllerDescriptorFactory>();
                scan.AddAllTypesOf<IObjectCopier>();
                scan.Convention<SingletonConvention>();
            });

            // Pass in the global controllerBuilder reference
            this.For<ControllerBuilder>()
                .Use(x => ControllerBuilder.Current);

            this.For<IControllerBuilder>()
                .Use<ControllerBuilderAdaptor>();

            this.For<IBuildManager>()
                .Use<BuildManagerAdaptor>();

            this.For<System.Web.Routing.RouteCollection>().Use(System.Web.Routing.RouteTable.Routes);

            // Configure Security
            this.For<IAclModule>().Use<CompositeAclModule>()
                .EnumerableOf<IAclModule>().Contains(x =>
                {
                    x.Type<AuthorizeAttributeAclModule>();
                    x.Type<XmlRolesAclModule>();
                });

            // Setup cache
            this.For<ISiteMapCache>()
                .Use<RuntimeSiteMapCache>();

            var cacheDependency = 
                this.For<ICacheDependency>().Use<NullCacheDependency>();

            var cacheDetails = 
                this.For<ICacheDetails>().Use<CacheDetails>()
                    .Ctor<TimeSpan>("absoluteCacheExpiration").Is(TimeSpan.FromMinutes(5))
                    .Ctor<TimeSpan>("slidingCacheExpiration").Is(TimeSpan.MinValue)
                    .Ctor<ICacheDependency>().Is(cacheDependency);


            // Register the custom sitemap builder
            var builder = this.For<ISiteMapBuilder>().Use<SiteMapBuilder>();

            this.For<ISiteMapBuilderSetStrategy>().Use<SiteMapBuilderSetStrategy>()
                .EnumerableOf<ISiteMapBuilderSet>().Contains(x =>
                {
                    x.Type<SiteMapBuilderSet>()
                        .Ctor<string>("instanceName").Is("default")
                        .Ctor<ISiteMapBuilder>().Is(builder)
                        .Ctor<ICacheDetails>().Is(cacheDetails);
                });
        }
    }
}