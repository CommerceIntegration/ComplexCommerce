using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ComplexCommerce.Data.Entity.Model
{
    public class ComplexCommerceSeeder
        : DropCreateDatabaseIfModelChanges<ComplexCommerce>
    {
        protected override void Seed(ComplexCommerce context)
        {
            this.SeedChain(context);
            this.SeedTenant(context);
            this.SeedTenantLocale(context);
            this.SeedPage(context);
            this.SeedPageLocale(context);
            this.SeedCategory(context);
            this.SeedCategoryLocale(context);
            this.SeedProduct(context);
            this.SeedProductXTenantXLocale(context);
            this.SeedCategoryXProduct(context);
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

        private void SeedTenant(ComplexCommerce context)
        {
            var table = new List<Tenant>
            {
                new Tenant { ChainId = 1, Name = "The Letter Guys Store Manager", LogoUrl = "", Host = "admin.theletterguys.com", DefaultLocaleId = 1033, TenantType = 2 }, // id = 1
                new Tenant { ChainId = 1, Name = "The Letter Guys Store Index", LogoUrl = "", Host = "www.theletterguys.com", DefaultLocaleId = 1033, TenantType = 3 }, // id = 2
                new Tenant { ChainId = 1, Name = "Easy ABC Store", LogoUrl = "", Host = "www.easyabcstore.com", DefaultLocaleId = 1033, TenantType = 1 }, // id = 3
                new Tenant { ChainId = 1, Name = "A to Z Store", LogoUrl = "", Host = "www.a2zstore.com", DefaultLocaleId = 1033, TenantType = 1 } // id = 4
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

        private void SeedPage(ComplexCommerce context)
        {
            var table = new List<Page>
            {
                new Page { Id = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), ParentId = null, TenantId = 3, ContentType = 1, ContentId = Guid.Empty, IsVisibleOnMainMenu = false, MetaRobots = "index, follow, noydir" },
                new Page { Id = new Guid("d1ee8545-dd7d-4304-99de-397f880b553e"), ParentId = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), TenantId = 3, ContentType = 2, ContentId = new Guid("db4f46b2-9335-4d3a-95c8-43490b6ef6f6"), IsVisibleOnMainMenu = true, MetaRobots = "index, follow, nosnippet" },
                new Page { Id = new Guid("505b5f18-e21a-4c99-8bfb-5a758e7012ce"), ParentId = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), TenantId = 3, ContentType = 2, ContentId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), IsVisibleOnMainMenu = true, MetaRobots = "" }
            };
            table.ForEach(x => context.Page.Add(x));
            context.SaveChanges();
        }

        private void SeedPageLocale(ComplexCommerce context)
        {
            var table = new List<PageLocale>
            {
                new PageLocale { Id = new Guid("6e2ba5f4-0fd6-4a0e-8cde-9ca221bf9423"), PageId = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), LocaleId = 1033, Url = "", Title = "Home", MetaKeywords = "Easy ABC, Bees, Seas", MetaDescription = "This is the #1 store for purchasing ABC products. Free Shipping, Secure Checkout, Best Experience." },
                new PageLocale { Id = new Guid("5d2daf5d-6b4f-4799-94b1-47146cc77ead"), PageId = new Guid("bbd915c0-4083-4c9f-949a-3541d7a1a278"), LocaleId = 2058, Url = "", Title = "Casa", MetaKeywords = "Easy ABC, Abejas, Mar", MetaDescription = "Esta es la tienda n º 1 para la compra de productos de ABC. Envío Gratis, Secure Checkout, Best Experience." },
                new PageLocale { Id = new Guid("5e33d5ae-ddcf-4dcb-9032-9ee98beeed98"), PageId = new Guid("d1ee8545-dd7d-4304-99de-397f880b553e"), LocaleId = 1033, Url = "bees", Title = "Bees", MetaKeywords = "bees, bee stuff, whatever", MetaDescription = "&#9733;Trying this out &#9733;" },
                new PageLocale { Id = new Guid("ae6d3023-3c75-4b38-a7b7-bd06ecbc7f91"), PageId = new Guid("d1ee8545-dd7d-4304-99de-397f880b553e"), LocaleId = 2058, Url = "abejas", Title = "Abejas", MetaKeywords = "abejas, abeja cosas, cualesquiera que sean", MetaDescription = "&#9733;Intentar esto &#9733;" },
                new PageLocale { Id = new Guid("c320cb36-f0a5-460a-91cf-5c86442375ee"), PageId = new Guid("505b5f18-e21a-4c99-8bfb-5a758e7012ce"), LocaleId = 1033, Url = "sea", Title = "The Sea", MetaKeywords = "sea, ocean, bay, shore, shell", MetaDescription = "Testing & Whatnot" },
                new PageLocale { Id = new Guid("842ebbd5-afe3-4c00-9cad-29d3257c974e"), PageId = new Guid("505b5f18-e21a-4c99-8bfb-5a758e7012ce"), LocaleId = 2058, Url = "mar", Title = "El Mar", MetaKeywords = "mar, océano, bahía, playa, concha", MetaDescription = "Pruebas y qué sé yo" }
            };
            table.ForEach(x => context.PageLocale.Add(x));
            context.SaveChanges();
        }

        private void SeedCategory(ComplexCommerce context)
        {
            var table = new List<Category>
            {
                new Category { Id = new Guid("db4f46b2-9335-4d3a-95c8-43490b6ef6f6"), TenantId = 3 },
                new Category { Id = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), TenantId = 3 }
            };
            table.ForEach(x => context.Category.Add(x));
            context.SaveChanges();
        }

        private void SeedCategoryLocale(ComplexCommerce context)
        {
            var table = new List<CategoryLocale>
            {
                new CategoryLocale { Id = new Guid("3fe8b207-d34d-4ffd-9f4a-dcd9dba86e72"), CategoryId = new Guid("db4f46b2-9335-4d3a-95c8-43490b6ef6f6"), LocaleId = 1033, Description = "This is all about bees, bee hives, and so forth." },
                new CategoryLocale { Id = new Guid("ced0bab7-03b5-4384-92be-af97c549a293"), CategoryId = new Guid("db4f46b2-9335-4d3a-95c8-43490b6ef6f6"), LocaleId = 2058, Description = "Esto es todo sobre las abejas, colmenas, etc." },
                new CategoryLocale { Id = new Guid("9a5138be-d519-4b57-9274-8e017dacb777"), CategoryId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), LocaleId = 1033, Description = "This is the sea category. Anything about the sea can be found here." },
                new CategoryLocale { Id = new Guid("fb3c68f5-31c5-41e5-a50f-8430493e4374"), CategoryId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), LocaleId = 2058, Description = "Esta es la categoría del mar. Cualquier cosa sobre el mar se puede encontrar aquí." }
            };
            table.ForEach(x => context.CategoryLocale.Add(x));
            context.SaveChanges();
        }

        private void SeedProduct(ComplexCommerce context)
        {
            var table = new List<Product>
            {
                new Product { Id = new Guid("78fd9f59-66e0-40d1-96a0-293e6fa0844a"), ChainId = 1, SKU = "SHELL123", ImageUrl = "", Price = Convert.ToDecimal(6.99), DefaultCategoryId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), MetaRobots = "" }
            };
            table.ForEach(x => context.Product.Add(x));
            context.SaveChanges();
        }

        private void SeedProductXTenantXLocale(ComplexCommerce context)
        {
            var table = new List<ProductXTenantXLocale>
            {
                new ProductXTenantXLocale { Id = new Guid("209f1271-2cd9-41bc-986d-7af5bd8198b9") , ProductId = new Guid("78fd9f59-66e0-40d1-96a0-293e6fa0844a"), TenantId = 3, LocaleId = 1033, Name = "All Purpose Shell Kit", Description = "A collection of sea shells from around the world.", Url = "all-purpose-shell-kit", IsUrlAbsolute = false, MetaKeywords = null, MetaDescription = "A collection of sea shells from around the world." },
                new ProductXTenantXLocale { Id = new Guid("ef092e1d-ac51-4026-ab52-0e7e1576eac3") , ProductId = new Guid("78fd9f59-66e0-40d1-96a0-293e6fa0844a"), TenantId = 3, LocaleId = 2058, Name = "Todo Propósito Concha Estuche", Description = "Una colección de conchas de mar de todo el mundo.", Url = "todo-propósito-concha-estuche", IsUrlAbsolute = false, MetaKeywords = null, MetaDescription = "Una colección de conchas de mar de todo el mundo." }
            };
            table.ForEach(x => context.ProductXTenantXLocale.Add(x));
            context.SaveChanges();
        }

        private void SeedCategoryXProduct(ComplexCommerce context)
        {
            var table = new List<CategoryXProduct>
            {
                new CategoryXProduct { Id = new Guid("bbb4112f-a933-4190-a374-bc75b1fd97b9"), CategoryId = new Guid("db4f46b2-9335-4d3a-95c8-43490b6ef6f6"), ProductId = new Guid("78fd9f59-66e0-40d1-96a0-293e6fa0844a") },
                 new CategoryXProduct { Id = new Guid("2463dd1a-4b33-4b15-80e3-2bd55774563d"), CategoryId = new Guid("658c3fc4-77ff-44a5-8bb3-eab48c151faa"), ProductId = new Guid("78fd9f59-66e0-40d1-96a0-293e6fa0844a") }
            };
            table.ForEach(x => context.CategoryXProduct.Add(x));
            context.SaveChanges();
        }
    }
}
