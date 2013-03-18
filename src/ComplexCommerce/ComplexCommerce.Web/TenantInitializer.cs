using System;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web
{
    public class TenantInitializer
    {
        public TenantInitializer(
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

            // Lookup the current tenant and put it into ambient context on the current request
            var context = httpContextFactory.GetHttpContext();
            appContext.CurrentTenant = contextUtilities.GetTenantFromContext(context);
        }
    }
}
