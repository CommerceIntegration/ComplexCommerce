using System;
using System.Collections.Generic;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.Repositories
{
    public interface IProductRepository
    {
        IList<CategoryProductDto> ListForCategory(Guid categoryId, int localeId);
        //IList<RouteUrlProductDto> ListForRouteUrl(int tenantId, int localeId);
        IList<RouteUrlProductDto> ListForRouteUrl(int tenantId);
        IList<SiteMapProductDto> ListForSiteMap(int tenantId, int localeId);
        IList<ProductLocaleDto> ListLocales(Guid productId, int tenantId);
        ProductDto Fetch(Guid categoryXProductId, int localeId);
    }
}
