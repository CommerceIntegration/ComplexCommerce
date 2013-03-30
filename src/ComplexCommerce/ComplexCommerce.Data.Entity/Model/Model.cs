using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ComplexCommerce.Data.Entity.Model
{
    // Context class
    public class ComplexCommerce
        : DbContext, IComplexCommerce
    {
        public ComplexCommerce(string connectionString)
            : base(connectionString)
        {
            //this.Configuration.LazyLoadingEnabled = true;
        }

        public IDbSet<Chain> Chain { get; set; }
        public IDbSet<Tenant> Tenant { get; set; }
        public IDbSet<TenantLocale> TenantLocale { get; set; }
        public IDbSet<Page> Page { get; set; }
        public IDbSet<PageLocale> PageLocale { get; set; }
        public IDbSet<Category> Category { get; set; }
        public IDbSet<CategoryLocale> CategoryLocale { get; set; }
        public IDbSet<CategoryXProduct> CategoryXProduct { get; set; }
        public IDbSet<Product> Product { get; set; }
        public IDbSet<ProductXTenantXLocale> ProductXTenantXLocale { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // TODO: add foreign keys here
            //http://stackoverflow.com/questions/5656159/entity-framework-4-1-code-first-foreign-key-ids
        }
    }

    public class Chain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Tenant
    {
        public int Id { get; set; }
        public int ChainId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Host { get; set; }
        public int DefaultLocaleId { get; set; }
        public int TenantType { get; set; }
    }

    public class TenantLocale
    {
        public Guid Id { get; set; }
        public int TenantId { get; set; }
        public int LocaleId { get; set; }
    }

    public class Page
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public int TenantId { get; set; }
        public int ContentType { get; set; }
        public Guid ContentId { get; set; }
        public bool IsVisibleOnMainMenu { get; set; }
        public string MetaRobots { get; set; }
    }

    public class PageLocale
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public int LocaleId { get; set; }
        public string Url { get; set; }
        public bool IsUrlAbsolute { get; set; }
        public string Title { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }

    public class Category
    {
        public Guid Id { get; set; }
        public int TenantId { get; set; }
    }

    public class CategoryLocale
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public int LocaleId { get; set; }
        public string Description { get; set; }
    }

    public class CategoryXProduct
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public int ChainId { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public Guid DefaultCategoryId { get; set; }
        public string MetaRobots { get; set; }
    }

    public class ProductXTenantXLocale
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int TenantId { get; set; }
        public int LocaleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsUrlAbsolute { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }
}
