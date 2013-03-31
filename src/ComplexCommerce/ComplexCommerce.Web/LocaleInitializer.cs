using System;
using System.Threading;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web
{
    public class LocaleInitializer
    {
        public LocaleInitializer(
            IContextUtilities contextUtilities,
            IHttpContextFactory httpContextFactory,
            IApplicationContext appContext
            )
        {
            if (contextUtilities == null)
                throw new ArgumentNullException("contextUtilities");
            if (httpContextFactory == null)
                throw new ArgumentNullException("httpContextFactory");
            if (appContext == null)
                throw new ArgumentNullException("appContext");

            this.contextUtilities = contextUtilities;
            this.httpContextFactory = httpContextFactory;
            this.appContext = appContext;

            // Execute on creation
            GetLocaleFromContext();
        }

        private readonly IContextUtilities contextUtilities;
        private readonly IHttpContextFactory httpContextFactory;
        private readonly IApplicationContext appContext;
        

        public void GetLocaleFromContext()
        {
            var context = httpContextFactory.GetHttpContext();
            var tenant = appContext.CurrentTenant;

            // Set the current culture
            var culture = contextUtilities.GetLocaleFromContext(context, tenant.DefaultLocale);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
