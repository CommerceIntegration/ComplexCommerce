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

            routes.Add(container.Resolve<LowerCaseRedirectRoute>());
            routes.Add(container.Resolve<TrailingSlashRedirectRoute>());
            routes.Add(container.Resolve<DefaultLocaleRedirectRoute>());
            routes.Add(container.Resolve<PageRoute>());
            routes.Add(container.Resolve<ProductRoute>());

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}