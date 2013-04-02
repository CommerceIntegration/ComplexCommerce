using System;

namespace ComplexCommerce.Data.Dto
{
    public class CategoryProductDto
    {
        //public Guid ProductXTenantLocaleId { get; set; } // Key for individual product/locale/url combination.
        //public Guid CategoryXProductXTenantLocaleId { get; set; }
        public Guid CategoryXProductId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
