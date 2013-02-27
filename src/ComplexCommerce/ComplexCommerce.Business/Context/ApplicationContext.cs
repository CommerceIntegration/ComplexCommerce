using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Business;
using CslaLibrary = Csla;


namespace ComplexCommerce.Business.Context
{
    public class ApplicationContext
    {

        const string CurrentTenantName = "complexcommerce.tenant";

        public static ITenant CurrentTenant
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
    }
}
