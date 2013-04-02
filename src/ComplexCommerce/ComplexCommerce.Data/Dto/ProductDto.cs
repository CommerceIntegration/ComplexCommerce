using System;

namespace ComplexCommerce.Data.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
