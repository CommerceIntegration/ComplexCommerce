using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class TenantDto
    {
        public int Id { get; set; }
        public int ChainId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int DefaultLocaleId { get; set; }
        public int TenantType { get; set; }
    }
}
