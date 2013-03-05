using System;
using System.Collections.Generic;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.Repositories
{
    public interface IProductRepository
    {
        IList<CategoryProductDto> ListForCategory(Guid categoryId);
        IList<RouteUrlProductDto> ListForTenantLocale(int tenantId, int localeId);
    }
}
