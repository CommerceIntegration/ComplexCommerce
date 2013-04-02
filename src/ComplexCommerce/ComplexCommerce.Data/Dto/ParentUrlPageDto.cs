using System;

namespace ComplexCommerce.Data.Dto
{
    public class ParentUrlPageDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public int TenantId { get; set; }
        public int LocaleId { get; set; }
        public int ContentType { get; set; }
        public Guid ContentId { get; set; }
        public string Url { get; set; }
        public bool IsUrlAbsolute { get; set; }
    }
}
