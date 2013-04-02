using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Web.Mvc.Routing;

namespace ComplexCommerce
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes, IDependencyInjectionContainer container)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // TODO: Make custom FavIcons per tenant
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: "Error - 404",
                url: "not-found",
                defaults: new { controller = "System", action = "Status404" }
                );

            routes.MapRoute(
                name: "Error - 500",
                url: "server-error",
                defaults: new { controller = "System", action = "Status500" }
                );


            routes.Add(container.Resolve<LowercaseRedirectRoute>());
            routes.Add(container.Resolve<TrailingSlashRedirectRoute>());
            routes.Add(container.Resolve<DefaultLocaleRedirectRoute>());
            routes.Add("Page", container.Resolve<PageRoute>());
            routes.Add("Product", container.Resolve<ProductRoute>());

            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}