﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Dto
{
    public class RouteUrlProductDto
    {
        //public Guid ProductXTenantLocaleId { get; set; }
        public Guid CategoryXProductXTenantLocaleId { get; set; }
        public Guid ParentId { get; set; }
        public int LocaleId { get; set; }
        public string Url { get; set; }
        public bool IsUrlAbsolute { get; set; }
        //public string ParentPageUrl { get; set; }
    }
}
