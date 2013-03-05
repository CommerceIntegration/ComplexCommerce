using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Web.Mvc;

namespace ComplexCommerce
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes, IDependencyInjectionContainer container)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.Add(container.Resolve<PageRoute>());
            routes.Add(container.Resolve<ProductRoute>());

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{culture}/{controller}/{action}/{id}",
            //    defaults: new { culture = "en-us", controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
               name: "Default2",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );


        }
    }
}