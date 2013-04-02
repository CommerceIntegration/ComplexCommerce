using System;

namespace ComplexCommerce.Data.Dto
{
    public class SiteMapPageDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsUrlAbsolute { get; set; }
        public string MetaRobots { get; set; }
        public bool IsVisibleOnMainMenu { get; set; }
        public int TenantId { get; set; }
        public int LocaleId { get; set; }
    }
}