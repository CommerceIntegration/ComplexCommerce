using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComplexCommerce.Business;
using CslaLibrary = Csla;


namespace ComplexCommerce.Business.Context
{
    public class ApplicationContext
        : IApplicationContext
    {

        const string CurrentTenantName = "complexcommerce.tenant";

        public ITenant CurrentTenant
        {
            get
            {
                var obj = (Tenant)CslaLibrary.ApplicationContext.ClientContext[CurrentTenantName];
                if (obj != null)
                    return obj;
                else
                    return Tenant.NewTenant();
            }
            set
            {
                CslaLibrary.ApplicationContext.ClientContext[CurrentTenantName] = value;
            }
        }

        public int CurrentLocaleId
        {
            get { return Thread.CurrentThread.CurrentUICulture.LCID; }
        }
    }
}
