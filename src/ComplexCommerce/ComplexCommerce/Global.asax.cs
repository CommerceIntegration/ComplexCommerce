using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ComplexCommerce.Web;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Business.Context;
using ComplexCommerce.Web.Mvc.ErrorHandling;

using System.Globalization;

namespace ComplexCommerce
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication 
        : HttpApplication
    {
        private static IDependencyInjectionContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            container = DIConfig.Register();
            MvcSiteMapProviderConfig.Register(container);
            ModelConfig.Register(container);
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
            // TODO: 302 redirect if the preferred culture from the 
            // HttpContext.Current.Request.UserLanguages exists for the given page and
            // the UrlReferrer is not the current site.            
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var errorHandler = container.Resolve<ISystemErrorHandler>();
            if (errorHandler != null)
                errorHandler.ProcessUnhandledError();
        }
    }
}