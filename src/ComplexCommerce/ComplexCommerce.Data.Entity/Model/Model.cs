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
        public IDbSet<View> View { get; set; }
        public IDbSet<ViewText> ViewText { get; set; }
        public IDbSet<ViewTextLocale> ViewTextLocale { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // TODO: add foreign keys here
            //http://stackoverflow.com/questions/5656159/entity-framework-4-1-code-first-foreign-key-ids


            // Add Foreign Keys (Require a navigation property on at least one side)
            modelBuilder.Entity<Tenant>()
                .HasRequired(x => x.Chain)
                .WithMany()
                .HasForeignKey(x => x.ChainId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TenantLocale>()
                .HasRequired(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Page>()
                .HasOptional(x => x.Parent)
                .WithMany()
                .HasForeignKey(x => x.ParentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Page>()
                .HasRequired(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PageLocale>()
                .HasRequired(x => x.Page)
                .WithMany()
                .HasForeignKey(x => x.PageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasRequired(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryLocale>()
                .HasRequired(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryXProduct>()
                .HasRequired(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryXProduct>()
                .HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoryXProduct>()
                .HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(x => x.Chain)
                .WithMany()
                .HasForeignKey(x => x.ChainId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductXTenantXLocale>()
                .HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductXTenantXLocale>()
               .HasRequired(x => x.Tenant)
               .WithMany()
               .HasForeignKey(x => x.TenantId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ViewText>()
               .HasRequired(x => x.View)
               .WithMany()
               .HasForeignKey(x => x.ViewId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ViewTextLocale>()
               .HasRequired(x => x.ViewText)
               .WithMany()
               .HasForeignKey(x => x.ViewTextId)
               .WillCascadeOnDelete(false);

            // Sample: Probably want to do it this way instead.
            //modelBuilder.Configurations.Add(new TenantConfiguration());
        }
    }

    // Sample: Probably want to do it this way instead.
    class TenantConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Tenant>
    {
        internal TenantConfiguration()
        {
            this.HasRequired(x => x.Chain)
                .WithMany()
                .HasForeignKey(x => x.ChainId);
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
        public Chain Chain { get; set; }
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
        public Tenant Tenant { get; set; }
        public int LocaleId { get; set; }
    }

    public class Page
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public Page Parent { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public int ContentType { get; set; }
        public Guid ContentId { get; set; }
        public bool IsVisibleOnMainMenu { get; set; }
        public string MetaRobots { get; set; }
    }

    public class PageLocale
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Page Page { get; set; }
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
        public Tenant Tenant { get; set; }
    }

    public class CategoryLocale
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int LocaleId { get; set; }
        public string Description { get; set; }
    }

    public class CategoryXProduct
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public int ChainId { get; set; }
        public Chain Chain { get; set; }
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
        public Product Product { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public int LocaleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsUrlAbsolute { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }

    public class View
    {
        public Guid Id { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string VirtualPath { get; set; }
        public int VirtualPathHashCode { get; set; }
    }

    public class ViewText
    {
        public Guid Id { get; set; }
        public Guid ViewId { get; set; }
        public View View { get; set; }
        public string TextName { get; set; }
    }

    public class ViewTextLocale
    {
        public Guid Id { get; set; }
        public Guid ViewTextId { get; set; }
        public ViewText ViewText { get; set; }
        public int LocaleId { get; set; }
        public string Value { get; set; }
    }
}
