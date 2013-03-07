using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string DefaultCategoryRouteUrl { get; set; }
    }
}
