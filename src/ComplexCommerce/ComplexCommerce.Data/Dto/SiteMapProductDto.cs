using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class SiteMapProductDto
    {
        //public Guid ProductXTenantLocaleId { get; set; }
        public Guid CategoryId { get; set; }
        public int LocaleId { get; set; }
        public string Name { get; set; }
        public string ProductUrlSlug { get; set; }
        public string ParentPageRouteUrl { get; set; }
        public string MetaRobots { get; set; }
        public string DefaultCategoryRouteUrl { get; set; }
    }
}
