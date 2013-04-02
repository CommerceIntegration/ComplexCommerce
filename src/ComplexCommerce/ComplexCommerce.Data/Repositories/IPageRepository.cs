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
        IList<ParentUrlPageDto> ListForParentUrl(int tenantId);
        IList<SiteMapPageDto> ListForSiteMap(int tenantId, int localeId);
        IList<PageLocaleDto> ListLocales(Guid pageId);
    }
}
