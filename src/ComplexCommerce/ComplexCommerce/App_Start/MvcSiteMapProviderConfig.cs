using System;
using ComplexCommerce.Shared.DI;
using MvcSiteMapProvider.Loader;

namespace ComplexCommerce
{
    public class MvcSiteMapProviderConfig
    {
        public static void Register(IDependencyInjectionContainer container)
        {
            // Setup global sitemap loader
            MvcSiteMapProvider.SiteMaps.Loader = container.Resolve<ISiteMapLoader>();
        }
    }
}