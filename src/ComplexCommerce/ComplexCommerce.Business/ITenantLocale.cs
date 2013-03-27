using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Business
{
    public interface ITenantLocale
    {
        int TenantId { get; }
        int LocaleId { get; }
        int DefaultLocaleId { get; }
    }
}
