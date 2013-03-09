using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using MvcSiteMapProvider.Loader;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.DI.Conventions;
using ComplexCommerce.Web.Mvc.SiteMapProvider;


namespace ComplexCommerce.DI.Registries
{
    internal class MvcSiteMapProviderRegistry
        : Registry
    {
        public MvcSiteMapProviderRegistry(IDependencyInjectionContainer container)
        {
            this.For<IMvcContextFactory>()
                .Use<MvcContextFactory>();

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
                scan.WithDefaultConventions();
                scan.AddAllTypesOf<IDynamicNodeProvider>();
                scan.AddAllTypesOf<ISiteMapNodeVisibilityProvider>();
                scan.AddAllTypesOf<ISiteMapNodeUrlResolver>();
                scan.Convention<SingletonConvention>();
            });






            this.For<ISiteMap>()
                .Use<RequestCacheableSiteMap>();

            this.For<ISiteMapNode>()
                .Use<RequestCacheableSiteMapNode>();

            // Pass in the global controllerBuilder reference
            this.For<ControllerBuilder>()
                .Use(x => ControllerBuilder.Current);

            this.For<IControllerBuilder>()
                .Use<ControllerBuilderAdaptor>();

            this.For<IBuildManager>()
                .Use<BuildManagerAdaptor>();

            //// Configure default filter provider with one that provides filters
            //// from the global filter collection.
            //this.Scan(scan =>
            //{
            //    scan.AssemblyContainingType<SiteMaps>();
            //    //scan.WithDefaultConventions();
            //    //scan.SingleImplementationsOfInterface();
            //    scan.With(new SingletonConvention<IFilterProvider>());
            //    scan.With(new SingletonConvention<IControllerDescriptorFactory>());
            //    //scan.With(new SingletonConvention<IObjectCopier>());
            //    //scan.With(new SingletonConvention<IDynamicNodeProviderStrategy>());
            //    //scan.With(new SingletonConvention<ISiteMapNodeUrlResolverStrategy>());
            //    //scan.With(new SingletonConvention<ISiteMapNodeVisibilityProviderStrategy>());

            //    //scan.With(new SingletonConvention<INodeKeyGenerator>());
            //    //scan.With(new SingletonConvention<IControllerDescriptorFactory>());
            //    //scan.With(new SingletonConvention<IControllerDescriptorFactory>());
            //});

            this.For<IFilterProvider>()
                .Singleton()
                .Use<FilterProvider>();

            this.For<IControllerDescriptorFactory>()
                .Singleton()
                .Use<ControllerDescriptorFactory>();

            this.For<IObjectCopier>()
                .Singleton()
                .Use<ObjectCopier>();

            this.For<IDynamicNodeProviderStrategy>()
                .Singleton()
                .Use<DynamicNodeProviderStrategy>();

            this.For<ISiteMapNodeUrlResolverStrategy>()
                .Singleton()
                .Use<SiteMapNodeUrlResolverStrategy>();

            this.For<IControllerDescriptorFactory>()
                .Singleton()
                .Use<ControllerDescriptorFactory>();

            this.For<ISiteMapNodeVisibilityProviderStrategy>()
                .Singleton()
                .Use<SiteMapNodeVisibilityProviderStrategy>();




            ////@Html.MvcSiteMap().SiteMapPath()

            ////var f = new MvcContextFactory();

            ////this.For<IMvcContextFactory>()
            ////    .Use(f);

            


            //var factory = container.Resolve<IMvcContextFactory>();

            //var module = container.Resolve<AuthorizeAttributeAclModule>();

            // Configure Security
            //var aclModules = new CompositeAclModule(
            //    container.Resolve<AuthorizeAttributeAclModule>(),
            //    container.Resolve<XmlRolesAclModule>()
            //);

            //this.For<IAclModule>()
            //    .Use(aclModules);

            // Configure Security
            this.For<IAclModule>().Use<CompositeAclModule>()
                .EnumerableOf<IAclModule>().Contains(x =>
                {
                    x.Type<AuthorizeAttributeAclModule>();
                    x.Type<XmlRolesAclModule>();
                });


            


            this.For<ISiteMapNodeVisitor>()
               .Use<UrlResolvingSiteMapNodeVisitor>();

            

            // Setup cache
            this.For<ISiteMapCache>()
                .Use<AspNetSiteMapCache>();

            this.For<ICacheDependencyFactory>()
                .Use<AspNetCacheDependencyFactory>()
                .Ctor<IEnumerable<string>>().Is(new string[0]);

            this.For<ICacheDetails>().Use<CacheDetails>()
                .Ctor<TimeSpan>("absoluteCacheExpiration").Is(TimeSpan.FromMinutes(5))
                .Ctor<TimeSpan>("slidingCacheExpiration").Is(TimeSpan.MinValue);


            // Register the custom sitemap builder
            this.For<ISiteMapBuilder>().Use<CompositeSiteMapBuilder>()
                .EnumerableOf<ISiteMapBuilder>().Contains(x =>
                {
                    x.Type<SiteMapBuilder>();
                    x.Type<VisitingSiteMapBuilder>();
                });

            

            //this.For<ISiteMapBuilderSet>().Use<SiteMapBuilderSet>()
            //   .Ctor<string>("name").Is("default");
            //   //.Ctor<ISiteMapBuilder>().Is<CompositeSiteMapBuilder>()
            //   //.Ctor<ICacheDetails>().

            this.For<ISiteMapBuilderSetStrategy>().Use<SiteMapBuilderSetStrategy>()
                .EnumerableOf<ISiteMapBuilderSet>().Contains(x =>
                {
                    x.Type<SiteMapBuilderSet>().Ctor<string>("name").Is("default");
                });


            //// Setup global sitemap loader
            //MvcSiteMapProvider.SiteMaps.Loader = container.Resolve<ISiteMapLoader>();
        }
    }
}