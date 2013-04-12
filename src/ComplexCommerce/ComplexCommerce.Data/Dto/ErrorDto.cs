using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class ErrorDto
    {
        public Guid Id { get; set; }
        public string Application { get; set; }
        public int ChainId { get; set; }
        public int TenantId { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public int StatusCode { get; set; }
        public DateTime UtcTime { get; set; }
        public int Sequence { get; set; }
        public string Xml { get; set; }
    }
}
