using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.Repositories
{
    public interface IPageRepository
    {
        List<SiteMapPageDto> List(int tenantId, int localeId);
    }
}
