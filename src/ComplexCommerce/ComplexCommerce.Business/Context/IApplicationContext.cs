using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Business;

namespace ComplexCommerce.Business.Context
{
    public interface IApplicationContext
    {
        ITenant CurrentTenant { get; set; }
        int CurrentLocaleId { get; }
    }
}
