﻿using System;
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

            // Configure Security
            this.For<IAclModule>().Use<CompositeAclModule>()
                .EnumerableOf<IAclModule>().Contains(x =>
                {
                    x.Type<AuthorizeAttributeAclModule>();
                    x.Type<XmlRolesAclModule>();
                });

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
            this.For<ISiteMapBuilder>().Use<SiteMapBuilder>();

            this.For<ISiteMapBuilderSetStrategy>().Use<SiteMapBuilderSetStrategy>()
                .EnumerableOf<ISiteMapBuilderSet>().Contains(x =>
                {
                    x.Type<SiteMapBuilderSet>().Ctor<string>("name").Is("default");
                });
        }
    }
}