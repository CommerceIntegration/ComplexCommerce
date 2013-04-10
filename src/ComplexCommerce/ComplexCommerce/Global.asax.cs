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

using System.Globalization;

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

            //var stringProvider = new ResourceStringProvider(Resources.LocalizedStrings.ResourceManager);

            //var stringProvider = new ComplexCommerce.Web.Mvc.Globalization.LocalizedStringProvider();

            var stringProvider = container.Resolve<ComplexCommerce.Web.Mvc.Globalization.ILocalizedStringProvider>();

            //ModelMetadataProviders.Current = new LocalizedModelMetadataProvider(stringProvider);
            ////ModelValidatorProviders.Providers.Clear();
            ////ModelValidatorProviders.Providers.Add(new LocalizedModelValidatorProvider(stringProvider)); 


            // Crappy dependency resolver required by Griffin.MvcContrib - gotta go.
            //DependencyResolver.SetResolver(new DI.SMDependencyResolver(container));


            ModelValidatorProviders.Providers.Clear();
            ModelMetadataProviders.Current = new ComplexCommerce.Web.Mvc.Globalization.LocalizedModelMetadataProvider(stringProvider);
            //ModelValidatorProviders.Providers.Add(container.Resolve<ILocalizedModelValidatorProvider>());
            ModelValidatorProviders.Providers.Add(new ComplexCommerce.Web.Mvc.Globalization.LocalizedModelValidatorProvider(new ComplexCommerce.Web.Mvc.Globalization.ValidationAttributeAdaptorFactory()));
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
    }
}