using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace ComplexCommerce.Data.Entity.Model
{
    /// <summary>
    /// The interface for the specialized object context. This contains all of
    /// the <code>IDbSet</code> properties that are implemented in both the
    /// functional context class and the mock context class.
    /// </summary>
    public interface IComplexCommerce 
        : IDbContext
    {
        IDbSet<Chain> Chain { get; set; }
        IDbSet<Tenant> Tenant { get; set; }
        IDbSet<Page> Page { get; set; }
        IDbSet<PageLocale> PageLocale { get; set; }
        IDbSet<Category> Category { get; set; }
        IDbSet<CategoryLocale> CategoryLocale { get; set; }
        IDbSet<CategoryXProduct> CategoryXProduct { get; set; }
        IDbSet<Product> Product { get; set; }
        IDbSet<ProductXTenantXLocale> ProductXTenantXLocale { get; set; }
        IDbSet<View> View { get; set; }
        IDbSet<ViewText> ViewText { get; set; }
        IDbSet<ViewTextLocale> ViewTextLocale { get; set; }
        IDbSet<AssemblyType> AssemblyType { get; set; }
        IDbSet<AssemblyTypeText> AssemblyTypeText { get; set; }
        IDbSet<AssemblyTypeTextLocale> AssemblyTypeTextLocale { get; set; }
        IDbSet<Error> Error { get; set; }
    }
}
