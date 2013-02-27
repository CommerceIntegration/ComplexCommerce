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

            CslaConfig.RegisterCsla();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            container = DIConfig.RegisterDependencyInjectionContainer();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            // Lookup the current tenant and put it into ambient context on the current request
            var contextUtil = container.Resolve(typeof(IContextUtilities)) as IContextUtilities;
            var context = new HttpContextWrapper(HttpContext.Current);
            var tenant = contextUtil.GetTenantFromContext(context);
            ApplicationContext.CurrentTenant = tenant;

            // Set the current culture
            var culture = contextUtil.GetLocaleFromContext(context, tenant.DefaultLocale);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}