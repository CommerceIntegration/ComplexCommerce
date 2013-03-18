using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using ComplexCommerce.Web;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IDependencyInjectionContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            container = DIConfig.Register();
            MvcSiteMapProviderConfig.Register(container);
            ModelBinderConfig.RegisterModelBinder();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes, container);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            DbConfig.Register(container);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            container.Resolve<TenantInitializer>();
            container.Resolve<LocaleInitializer>();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            
        }
    }
}