using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.Repositories
{
    public interface IViewRepository
    {
        IEnumerable<ViewLocaleDto> List(int tenantId, int localeId, int hashCode, string virtualPath);
    }
}
