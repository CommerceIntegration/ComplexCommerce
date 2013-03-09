using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComplexCommerce.Data.SqlServer.Model
{

    public class ComplexCommerceInitializer
        : DropCreateDatabaseIfModelChanges<ComplexCommerce>
    {
        protected override void Seed(ComplexCommerce context)
        {
            this.SeedChain(context);
            this.SeedTenant(context);
            this.SeedTenantLocale(context);
            this.SeedPage(context);
            this.SeedCategory(context);
            this.SeedProduct(context);
            this.SeedProductXTenantLocale(context);
            this.SeedCategoryXProductXTenantLocale(context);
        }

        private void SeedCategory(ComplexCommerce context)
        {
            var table = new List<Category>
            {
                new Category { Id = new Guid("db4f46b2-9335-4d3a-95c8-43490b6ef6f6"), TenantLocaleId = new Guid("af51370a-8d54-4891-b50e-f3511d530c90"), Description = "This is all about bees, bee hives, and so forth." },
                new Category { Id = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), TenantLocaleId = new Guid("af51370a-8d54-4891-b50e-f3511d530c90"), Description = "This is the sea category. Anything about the sea can be found here." }
            };
            table.ForEach(x => context.Category.Add(x));
            context.SaveChanges();
        }

        private void SeedCategoryXProductXTenantLocale(ComplexCommerce context)
        {
            var table = new List<CategoryXProductXTenantLocale>
            {
                new CategoryXProductXTenantLocale { Id = new Guid("bbb4112f-a933-4190-a374-bc75b1fd97b9"), CategoryId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), ProductXTenantLocaleId = new Guid("209f1271-2cd9-41bc-986d-7af5bd8198b9") }
            };
            table.ForEach(x => context.CategoryXProductXTenantLocale.Add(x));
            context.SaveChanges();
        }

        private void SeedChain(ComplexCommerce context)
        {
            var table = new List<Chain>
            {
                new Chain { Name = "The Letter Guys Enterprises", Description = "" }, // id = 1
                new Chain { Name = "Music Stores, Inc", Description = "" }, // id = 2
                new Chain { Name = "All Electronics Corp", Description = "" } // id = 3
            };
            table.ForEach(x => context.Chain.Add(x));
            context.SaveChanges();
        }

        private void SeedPage(ComplexCommerce context)
        {
            var table = new List<Page>
            {
                new Page { Id = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), ParentId = null, TenantLocaleId = new Guid("af51370a-8d54-4891-b50e-f3511d530c90"), Title = "Home", MetaKeywords = "Easy ABC, Bees, Seas", MetaDescription = "This is the #1 store for purchasing ABC products. Free Shipping, Secure Checkout, Best Experience.", MetaRobots = "index, follow, noyahoo", ContentType = 1, ContentId = Guid.Empty },
                new Page { Id = new Guid("d1ee8545-dd7d-4304-99de-397f880b553e"), ParentId = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), TenantLocaleId = new Guid("af51370a-8d54-4891-b50e-f3511d530c90"), Title = "Bees", MetaKeywords = "bees, bee stuff, whatever", MetaDescription = "&#9733;Trying this out &#9733;", MetaRobots = "index, follow, nosnippet", ContentType = 2, ContentId = new Guid("db4f46b2-9335-4d3a-95c8-43490b6ef6f6") },
                new Page { Id = new Guid("505b5f18-e21a-4c99-8bfb-5a758e7012ce"), ParentId = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), TenantLocaleId = new Guid("af51370a-8d54-4891-b50e-f3511d530c90"), Title = "The Sea", MetaKeywords = "sea, ocean, bay, shore, shell", MetaDescription = "Testing & Whatnot", MetaRobots = "", ContentType = 2, ContentId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa") }
            };
            table.ForEach(x => context.Page.Add(x));
            context.SaveChanges();
        }

        private void SeedProduct(ComplexCommerce context)
        {
            var table = new List<Product>
            {
                new Product { Id = new Guid("78fd9f59-66e0-40d1-96a0-293e6fa0844a"), ChainId = 1, SKU = "SHELL123", Price = Convert.ToDecimal(6.99) }
            };
            table.ForEach(x => context.Product.Add(x));
            context.SaveChanges();
        }

        private void SeedProductXTenantLocale(ComplexCommerce context)
        {
            var table = new List<ProductXTenantLocale>
            {
                new ProductXTenantLocale { Id = new Guid("209f1271-2cd9-41bc-986d-7af5bd8198b9") , ProductId = new Guid("78fd9f59-66e0-40d1-96a0-293e6fa0844a"), TenantLocaleId = new Guid("af51370a-8d54-4891-b50e-f3511d530c90"), Name = "All Purpose Shell Kit", Description = "A collection of sea shells from around the world.", UrlSlug = "all-purpose-shell-kit", MetaKeywords = null, MetaDescription = "A collection of sea shells from around the world.", DefaultCategoryId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa") }
            };
            table.ForEach(x => context.ProductXTenantLocale.Add(x));
            context.SaveChanges();
        }

        private void SeedTenant(ComplexCommerce context)
        {
            var table = new List<Tenant>
            {
                new Tenant { ChainId = 1, Name = "The Letter Guys Store Manager", LogoUrl = "", DefaultLocaleId = 1033, Host = "admin.theletterguys.com", TenantType = 2 }, // id = 1
                new Tenant { ChainId = 1, Name = "The Letter Guys Store Index", LogoUrl = "", DefaultLocaleId = 1033, Host = "www.theletterguys.com", TenantType = 3 }, // id = 2
                new Tenant { ChainId = 1, Name = "Easy ABC Store", LogoUrl = "", DefaultLocaleId = 1033, Host = "www.easyabcstore.com", TenantType = 1 }, // id = 3
                new Tenant { ChainId = 1, Name = "A to Z Store", LogoUrl = "", DefaultLocaleId = 1033, Host = "www.a2zstore.com", TenantType = 1 } // id = 4
            };
            table.ForEach(x => context.Tenant.Add(x));
            context.SaveChanges();
        }

        private void SeedTenantLocale(ComplexCommerce context)
        {
            var table = new List<TenantLocale>
            {
                new TenantLocale { Id = new Guid("2451c426-965c-423f-8b8b-c0ada988064f"), TenantId = 1, LocaleId = 1033 }, 
                new TenantLocale { Id = new Guid("0e073dca-35cc-48bb-b8bb-92c5a813f1ad"), TenantId = 2, LocaleId = 1033 },
                new TenantLocale { Id = new Guid("273c5fbb-67bc-4bec-b3be-de082dccb5f2"), TenantId = 2, LocaleId = 2058 },
                new TenantLocale { Id = new Guid("af51370a-8d54-4891-b50e-f3511d530c90"), TenantId = 3, LocaleId = 1033 },
                new TenantLocale { Id = new Guid("f1b3807f-1e7e-4b1e-895b-3ccf7194f784"), TenantId = 3, LocaleId = 2058 },
                new TenantLocale { Id = new Guid("e419051f-058a-40c0-8a82-4fa1eafe8f8f"), TenantId = 3, LocaleId = 3084 },
                new TenantLocale { Id = new Guid("802ea8d5-116d-4712-ab57-b1f41ae37c40"), TenantId = 3, LocaleId = 1038 },
                new TenantLocale { Id = new Guid("716b0e2e-3dff-45ba-b45b-337a53fa4571"), TenantId = 4, LocaleId = 1033 },
                new TenantLocale { Id = new Guid("ed92ac8c-b866-41c6-af79-a918f6f7ade4"), TenantId = 4, LocaleId = 1039 }
            };
            table.ForEach(x => context.TenantLocale.Add(x));
            context.SaveChanges();
        }
    }
}
