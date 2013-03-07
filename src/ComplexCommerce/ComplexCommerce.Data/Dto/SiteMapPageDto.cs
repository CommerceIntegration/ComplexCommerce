using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class SiteMapPageDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public int LocaleId { get; set; }
        public string Title { get; set; }
        public string RouteUrl { get; set; }
        public string MetaRobots { get; set; }
        public int ContentType { get; set; }
        public Guid ContentId { get; set; }
    }
}
