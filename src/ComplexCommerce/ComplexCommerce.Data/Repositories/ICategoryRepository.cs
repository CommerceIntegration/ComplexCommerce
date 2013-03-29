using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.Repositories
{
    public interface ICategoryRepository
    {
        IList<ProductCategoryDto> ListForProduct(Guid productId, int localeId);
        CategoryDto Fetch(Guid Id, int localeId);
    }
}
