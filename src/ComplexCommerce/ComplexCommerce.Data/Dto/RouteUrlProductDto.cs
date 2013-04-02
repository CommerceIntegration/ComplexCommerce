using System;

namespace ComplexCommerce.Data.Dto
{
    public class RouteUrlProductDto
    {
        public Guid CategoryXProductId { get; set; }
        public Guid ParentId { get; set; }
        public string Url { get; set; }
        public bool IsUrlAbsolute { get; set; }
        public int TenantId { get; set; }
        public int LocaleId { get; set; }
    }
}
