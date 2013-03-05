using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class CategoryProductDto
    {
        public Guid ProductXTenantLocaleId { get; set; } // Key for individual product/locale/url combination.
        public string Name { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
