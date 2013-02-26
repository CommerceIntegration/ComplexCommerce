using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class PageDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Title { get; set; }
        public string RouteUrl { get; set; }
        public int ContentTypeId { get; set; }
        public Guid ContentId { get; set; }
    }
}
