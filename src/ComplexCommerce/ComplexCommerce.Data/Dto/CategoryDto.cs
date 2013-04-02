using System;

namespace ComplexCommerce.Data.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }
}
