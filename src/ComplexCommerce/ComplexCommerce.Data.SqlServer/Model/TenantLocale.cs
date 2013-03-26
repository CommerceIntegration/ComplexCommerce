//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComplexCommerce.Data.SqlServer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TenantLocale
    {
        public TenantLocale()
        {
            this.Category = new HashSet<Category>();
            this.Page = new HashSet<Page>();
            this.ProductXTenantLocale = new HashSet<ProductXTenantLocale>();
        }
    
        public System.Guid Id { get; set; }
        public int TenantId { get; set; }
        public int LocaleId { get; set; }
    
        public virtual ICollection<Category> Category { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<Page> Page { get; set; }
        public virtual ICollection<ProductXTenantLocale> ProductXTenantLocale { get; set; }
    }
}
