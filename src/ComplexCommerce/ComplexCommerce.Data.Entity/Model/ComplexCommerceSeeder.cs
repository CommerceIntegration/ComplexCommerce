using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ComplexCommerce.Data.Entity.Model
{
    public class ComplexCommerceSeeder
        : DropCreateDatabaseIfModelChanges<ComplexCommerce>
        //: DropCreateDatabaseAlways<ComplexCommerce>
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
            this.SeedView(context);
            this.SeedViewText(context);
            this.SeedViewTextLocale(context);
            this.SeedAssemblyType(context);
            this.SeedAssemblyTypeText(context);
            this.SeedAssemblyTypeTextLocale(context);
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

        private void SeedView(ComplexCommerce context)
        {
            var table = new List<View>
            {
                // System Views (should always populate this data)
                new View { Id = new Guid("479c8b9f-86e5-4930-9a93-f223ff45eef7"), TenantId = 3, VirtualPath = "~/Views/System/Status301.cshtml", VirtualPathHashCode = "~/Views/System/Status301.cshtml".GetHashCode() },
                new View { Id = new Guid("e6806f2b-c600-44c5-9020-4a5d02150761"), TenantId = 3, VirtualPath = "~/Views/System/Status404.cshtml", VirtualPathHashCode = "~/Views/System/Status404.cshtml".GetHashCode() },
                new View { Id = new Guid("a4a02c26-0fda-4c42-9f08-35b30615ccca"), TenantId = 3, VirtualPath = "~/Views/System/Status500.cshtml", VirtualPathHashCode = "~/Views/System/Status500.cshtml".GetHashCode() },

                new View { Id = new Guid("c9c31471-0ea7-4c19-a2a6-086cefab21a6"), TenantId = 3, VirtualPath = "~/Views/Shared/_Layout.cshtml", VirtualPathHashCode = "~/Views/Shared/_Layout.cshtml".GetHashCode() },
                new View { Id = new Guid("46ea2251-591a-4d19-9a35-4ba9a2dc9b3a"), TenantId = 3, VirtualPath = "~/Views/Shared/_LoginPartial.cshtml", VirtualPathHashCode = "~/Views/Shared/_LoginPartial.cshtml".GetHashCode() },
                new View { Id = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TenantId = 3, VirtualPath = "~/Views/Home/Index.cshtml", VirtualPathHashCode = "~/Views/Home/Index.cshtml".GetHashCode() }
                
            };
            table.ForEach(x => context.View.Add(x));
            context.SaveChanges();
        }

        private void SeedViewText(ComplexCommerce context)
        {
            var table = new List<ViewText>
            {
                new ViewText { Id = new Guid("4a77dee7-7042-47cf-b9c4-ae44f3f4d1a2"), ViewId = new Guid("479c8b9f-86e5-4930-9a93-f223ff45eef7"), TextName = "Page_Moved_Title" },
                new ViewText { Id = new Guid("a5b20f68-a83b-4f59-b06a-1693f10140a2"), ViewId = new Guid("479c8b9f-86e5-4930-9a93-f223ff45eef7"), TextName = "Page_Moved" },

                new ViewText { Id = new Guid("3bd139d1-d1c7-40a0-9d87-79fbbc1a4f43"), ViewId = new Guid("e6806f2b-c600-44c5-9020-4a5d02150761"), TextName = "Not_Found_Title" },
                new ViewText { Id = new Guid("f88ccd8b-5da1-4a37-a071-e239529f66bf"), ViewId = new Guid("e6806f2b-c600-44c5-9020-4a5d02150761"), TextName = "Not_Found" },
                new ViewText { Id = new Guid("f1cd4c9b-c64a-4c63-b320-b5a3f9b96b55"), ViewId = new Guid("e6806f2b-c600-44c5-9020-4a5d02150761"), TextName = "Click_Here" },
                new ViewText { Id = new Guid("40eccc97-2de7-47eb-99e0-bbdb9375bb3f"), ViewId = new Guid("e6806f2b-c600-44c5-9020-4a5d02150761"), TextName = "To_Return_Home" },

                new ViewText { Id = new Guid("2a9a0888-2a81-41a6-b07b-31c86e60b029"), ViewId = new Guid("a4a02c26-0fda-4c42-9f08-35b30615ccca"), TextName = "System_Error_Title" },
                new ViewText { Id = new Guid("6b2b8806-1cba-43cb-814f-89f6fd3b098f"), ViewId = new Guid("a4a02c26-0fda-4c42-9f08-35b30615ccca"), TextName = "System_Error" },


                new ViewText { Id = new Guid("9e99e65f-f900-417e-a849-3dd06d2d6918"), ViewId = new Guid("c9c31471-0ea7-4c19-a2a6-086cefab21a6"), TextName = "App_Name" },

                new ViewText { Id = new Guid("a4aea259-66dc-402c-ac1d-5187e9193929"), ViewId = new Guid("46ea2251-591a-4d19-9a35-4ba9a2dc9b3a"), TextName = "Hello" },
                new ViewText { Id = new Guid("cfe33c51-ea20-4a51-adb9-73f695ddec84"), ViewId = new Guid("46ea2251-591a-4d19-9a35-4ba9a2dc9b3a"), TextName = "Manage" },
                new ViewText { Id = new Guid("8e32a053-44fc-4493-b3f2-783dd88d51cf"), ViewId = new Guid("46ea2251-591a-4d19-9a35-4ba9a2dc9b3a"), TextName = "Log_In" },
                new ViewText { Id = new Guid("ca4b9594-d961-47d4-9b9f-16c116ac18f2"), ViewId = new Guid("46ea2251-591a-4d19-9a35-4ba9a2dc9b3a"), TextName = "Register" },
                new ViewText { Id = new Guid("9ec6c5ba-ffc1-42a8-a82e-e1d11c88c8a0"), ViewId = new Guid("46ea2251-591a-4d19-9a35-4ba9a2dc9b3a"), TextName = "Log_Off" },

                new ViewText { Id = new Guid("fef874d0-3f8c-4c3b-be94-92bb3e9d5cb2"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Test" },
                new ViewText { Id = new Guid("7e352f73-812f-4cce-aa3d-5ae57c034735"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Title" },
                new ViewText { Id = new Guid("b67fc540-aa5f-4831-a2e5-8318c85cce41"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Message" },
                new ViewText { Id = new Guid("8f852212-0056-43b0-80a4-41536eac63ef"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "To_Learn_More" },
                new ViewText { Id = new Guid("091857b8-b22c-4139-a0e1-f7276c27e5e3"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "We_Suggest" },
                new ViewText { Id = new Guid("41ac2b17-1546-4a0d-af5c-f12c8254b8ae"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Learn_More" },
                new ViewText { Id = new Guid("f248a1ac-016d-4dbb-a923-eda07d175834"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Getting_Started_Header" },
                new ViewText { Id = new Guid("14e3c2b3-66f6-4306-b3f9-627643ec3cec"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Getting_Started" },
                new ViewText { Id = new Guid("f636da7a-ee0d-44be-8373-c4148826f1ff"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Add_Nuget_Header" },
                new ViewText { Id = new Guid("346c865b-506d-460c-ba74-ce6b70c2250d"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Add_Nuget" },
                new ViewText { Id = new Guid("7f1600db-da73-4784-bff3-b648d5a95fd3"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Find_Hosting_Header" },
                new ViewText { Id = new Guid("fa5df3ad-7881-4dec-bd00-ae6fc702bc33"), ViewId = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TextName = "Find_Hosting" }
                
                };
            table.ForEach(x => context.ViewText.Add(x));
            context.SaveChanges();
        }

        private void SeedViewTextLocale(ComplexCommerce context)
        {
            var table = new List<ViewTextLocale>
            {
                new ViewTextLocale { Id = new Guid("5326852c-2032-47b0-8f84-934f9ec92405"), ViewTextId = new Guid("4a77dee7-7042-47cf-b9c4-ae44f3f4d1a2"), LocaleId = 1033, Value = @"Page Moved Permanently" },
                new ViewTextLocale { Id = new Guid("8949b6ba-4cac-46d8-a29e-158339a2396c"), ViewTextId = new Guid("4a77dee7-7042-47cf-b9c4-ae44f3f4d1a2"), LocaleId = 2058, Value = @"Página Movido Permanentemente" },
                new ViewTextLocale { Id = new Guid("66589d22-c327-41c6-8304-c15015300a58"), ViewTextId = new Guid("a5b20f68-a83b-4f59-b06a-1693f10140a2"), LocaleId = 1033, Value = @"The resource you are looking for has moved. You will be redirected to the new location automatically in 5 seconds. Please bookmark the correct page at" },
                new ViewTextLocale { Id = new Guid("a6e218db-90a2-4e35-a24b-4bcdde259849"), ViewTextId = new Guid("a5b20f68-a83b-4f59-b06a-1693f10140a2"), LocaleId = 2058, Value = @"El recurso que busca se ha movido. Usted será redirigido a la nueva ubicación automáticamente en 5 segundos. Por favor, marcar la página correcta en" },

                new ViewTextLocale { Id = new Guid("39a42a5d-308a-4b84-9140-d33b509e2af6"), ViewTextId = new Guid("3bd139d1-d1c7-40a0-9d87-79fbbc1a4f43"), LocaleId = 1033, Value = @"404 Page Not Found" },
                new ViewTextLocale { Id = new Guid("427750c7-67dd-46a9-8726-92e8f8024f8b"), ViewTextId = new Guid("3bd139d1-d1c7-40a0-9d87-79fbbc1a4f43"), LocaleId = 2058, Value = @"404 Página No Encontrada" },
                new ViewTextLocale { Id = new Guid("8ea90b16-a2cd-4453-b8a3-0918a1414b75"), ViewTextId = new Guid("f88ccd8b-5da1-4a37-a071-e239529f66bf"), LocaleId = 1033, Value = @"The resource you are looking for does not exist or is no longer available." },
                new ViewTextLocale { Id = new Guid("33e2fba3-13ba-4795-a139-53967ddc9889"), ViewTextId = new Guid("f88ccd8b-5da1-4a37-a071-e239529f66bf"), LocaleId = 2058, Value = @"El recurso que está buscando no existe o ya no está disponible." },
                new ViewTextLocale { Id = new Guid("8dee706c-c0e9-4812-a7c4-9426f51bcb00"), ViewTextId = new Guid("f1cd4c9b-c64a-4c63-b320-b5a3f9b96b55"), LocaleId = 1033, Value = @"Click here" },
                new ViewTextLocale { Id = new Guid("bf38dab6-86c4-4cff-9c37-84ed5436c0ec"), ViewTextId = new Guid("f1cd4c9b-c64a-4c63-b320-b5a3f9b96b55"), LocaleId = 2058, Value = @"Haga clic aquí" },
                new ViewTextLocale { Id = new Guid("a5455a53-3a47-4b22-9132-c8debc949d54"), ViewTextId = new Guid("40eccc97-2de7-47eb-99e0-bbdb9375bb3f"), LocaleId = 1033, Value = @"to return to our home page." },
                new ViewTextLocale { Id = new Guid("bebb1d25-61d3-4223-822d-5b6ea980716c"), ViewTextId = new Guid("40eccc97-2de7-47eb-99e0-bbdb9375bb3f"), LocaleId = 2058, Value = @"para volver a nuestra página de inicio." },

                new ViewTextLocale { Id = new Guid("21e05469-d80c-4689-87d1-2347cd214eed"), ViewTextId = new Guid("2a9a0888-2a81-41a6-b07b-31c86e60b029"), LocaleId = 1033, Value = @"System Error" },
                new ViewTextLocale { Id = new Guid("a3fc7dd7-ed00-4c00-a394-d63d1d218562"), ViewTextId = new Guid("2a9a0888-2a81-41a6-b07b-31c86e60b029"), LocaleId = 2058, Value = @"Error Del Sistema" },
                new ViewTextLocale { Id = new Guid("10cf34c5-1a9d-40fc-9447-07537d5a4030"), ViewTextId = new Guid("6b2b8806-1cba-43cb-814f-89f6fd3b098f"), LocaleId = 1033, Value = @"An error occurred while processing your request. Please click the back button on your browser and try again. If you repeatedly see this page, please contact us for assistance." },
                new ViewTextLocale { Id = new Guid("d51f9a5e-2fd9-42ae-acb7-a9685b3e20ea"), ViewTextId = new Guid("6b2b8806-1cba-43cb-814f-89f6fd3b098f"), LocaleId = 2058, Value = @"Se produjo un error al procesar su solicitud. Por favor, haga clic en el botón Atrás de su navegador e inténtelo de nuevo. Si con frecuencia se ve esta página, por favor póngase en contacto con nosotros para obtener ayuda." },


                new ViewTextLocale { Id = new Guid("4785f50b-d63a-40bc-ac41-a034d8425fc8"), ViewTextId = new Guid("9e99e65f-f900-417e-a849-3dd06d2d6918"), LocaleId = 1033, Value = @"My ASP.NET MVC Application" },
                new ViewTextLocale { Id = new Guid("6ed3bc55-6ed4-4bb0-8eee-077b93ca8763"), ViewTextId = new Guid("9e99e65f-f900-417e-a849-3dd06d2d6918"), LocaleId = 2058, Value = @"Mi aplicación ASP.NET MVC" },

                new ViewTextLocale { Id = new Guid("17f0b866-7f26-4c22-9af3-c31c28e937b2"), ViewTextId = new Guid("a4aea259-66dc-402c-ac1d-5187e9193929"), LocaleId = 1033, Value = @"Hello," },
                new ViewTextLocale { Id = new Guid("cacbd240-5fdb-42ac-aac9-32585e3e5763"), ViewTextId = new Guid("a4aea259-66dc-402c-ac1d-5187e9193929"), LocaleId = 2058, Value = @"Hola," },
                new ViewTextLocale { Id = new Guid("0b05ea80-413d-4fc2-9988-f36aeee984f5"), ViewTextId = new Guid("cfe33c51-ea20-4a51-adb9-73f695ddec84"), LocaleId = 1033, Value = @"Manage" },
                new ViewTextLocale { Id = new Guid("64284efe-2326-4322-8e1a-d91792c46b0d"), ViewTextId = new Guid("cfe33c51-ea20-4a51-adb9-73f695ddec84"), LocaleId = 2058, Value = @"Manejar" },
                new ViewTextLocale { Id = new Guid("b2da7909-60c4-4a13-b259-49a84f380c59"), ViewTextId = new Guid("8e32a053-44fc-4493-b3f2-783dd88d51cf"), LocaleId = 1033, Value = @"Log In" },
                new ViewTextLocale { Id = new Guid("91a7cda1-685a-43ef-9c3d-a4f1e4704f80"), ViewTextId = new Guid("8e32a053-44fc-4493-b3f2-783dd88d51cf"), LocaleId = 2058, Value = @"Iniciar La Sesión" },
                new ViewTextLocale { Id = new Guid("4ba1e9df-d064-4823-9d8d-bba8cd8e12ed"), ViewTextId = new Guid("ca4b9594-d961-47d4-9b9f-16c116ac18f2"), LocaleId = 1033, Value = @"Register" },
                new ViewTextLocale { Id = new Guid("0944bdcb-8f93-4644-a2c9-20d7dbf6a435"), ViewTextId = new Guid("ca4b9594-d961-47d4-9b9f-16c116ac18f2"), LocaleId = 2058, Value = @"Registro" },
                new ViewTextLocale { Id = new Guid("26d8c4f5-4ea8-47af-ad4a-beca0126313a"), ViewTextId = new Guid("9ec6c5ba-ffc1-42a8-a82e-e1d11c88c8a0"), LocaleId = 1033, Value = @"Log Off" },
                new ViewTextLocale { Id = new Guid("58067397-d113-4396-a165-782980882905"), ViewTextId = new Guid("9ec6c5ba-ffc1-42a8-a82e-e1d11c88c8a0"), LocaleId = 2058, Value = @"Salir Del Sistema" },

                new ViewTextLocale { Id = new Guid("c31673d2-587c-44e4-bb6b-c48d133e8817"), ViewTextId = new Guid("fef874d0-3f8c-4c3b-be94-92bb3e9d5cb2"), LocaleId = 1033, Value = "This is some test text" },
                new ViewTextLocale { Id = new Guid("3e3a79c6-1d97-45cd-baa1-b725cac9d383"), ViewTextId = new Guid("7e352f73-812f-4cce-aa3d-5ae57c034735"), LocaleId = 1033, Value = @"Home Page" },
                new ViewTextLocale { Id = new Guid("bf5e5e70-973e-43c2-8c1b-3b494dfa796f"), ViewTextId = new Guid("7e352f73-812f-4cce-aa3d-5ae57c034735"), LocaleId = 2058, Value = @"Página Principal" },
                new ViewTextLocale { Id = new Guid("31edad7a-a282-4607-a5d8-81ddf3718e4b"), ViewTextId = new Guid("b67fc540-aa5f-4831-a2e5-8318c85cce41"), LocaleId = 1033, Value = @"Modify this template to jump-start your ASP.NET MVC application." },
                new ViewTextLocale { Id = new Guid("4a2305f9-ae51-4cd9-b087-bc29ec43a3d7"), ViewTextId = new Guid("b67fc540-aa5f-4831-a2e5-8318c85cce41"), LocaleId = 2058, Value = @"Modifique esta plantilla para poner en marcha su aplicación ASP.NET MVC." },
                new ViewTextLocale { Id = new Guid("f04ab9fb-87a9-4a01-b943-37eac5fe83f9"), ViewTextId = new Guid("8f852212-0056-43b0-80a4-41536eac63ef"), LocaleId = 1033, Value = @"To learn more about ASP.NET MVC visit <a href=""{0}"" title=""ASP.NET MVC Website"">{0}</a>. The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET MVC. If you have any questions about ASP.NET MVC visit <a href=""{1}"" title=""ASP.NET MVC Forum"">our forums</a>." },
                new ViewTextLocale { Id = new Guid("da344c64-a370-4723-b4d4-29441e8d54bd"), ViewTextId = new Guid("8f852212-0056-43b0-80a4-41536eac63ef"), LocaleId = 2058, Value = @"Para obtener más información acerca de ASP.NET MVC visita <a href=""{0}"" title=""ASP.NET MVC Website"">{0}</a>. La página ofrece <mark> vídeos, tutoriales y muestras </mark> para ayudarle a sacar el máximo provecho de ASP.NET MVC. Si usted tiene alguna pregunta acerca de ASP.NET MVC visita <a href=""{1}"" title=""ASP.NET MVC Forum"">nuestros foros</a>." },
                new ViewTextLocale { Id = new Guid("6d0b9ee7-73cd-43d0-91a2-c8bf15c58620"), ViewTextId = new Guid("41ac2b17-1546-4a0d-af5c-f12c8254b8ae"), LocaleId = 1033, Value = @"Learn more…" },
                new ViewTextLocale { Id = new Guid("686bc643-2e6a-4f36-8f9c-a2ed852db823"), ViewTextId = new Guid("41ac2b17-1546-4a0d-af5c-f12c8254b8ae"), LocaleId = 2058, Value = @"Más información…" },
                new ViewTextLocale { Id = new Guid("1061be78-a524-48a3-914a-b79045b605a4"), ViewTextId = new Guid("091857b8-b22c-4139-a0e1-f7276c27e5e3"), LocaleId = 1033, Value = @"We suggest the following:" },
                new ViewTextLocale { Id = new Guid("49dcbbec-79af-43f1-8be7-b4dbcbb6f77a"), ViewTextId = new Guid("091857b8-b22c-4139-a0e1-f7276c27e5e3"), LocaleId = 2058, Value = @"Le sugerimos lo siguiente:" },
                new ViewTextLocale { Id = new Guid("225444b3-9d44-49ac-987f-b5edcb63a4bd"), ViewTextId = new Guid("f248a1ac-016d-4dbb-a923-eda07d175834"), LocaleId = 1033, Value = @"Getting Started" },
                new ViewTextLocale { Id = new Guid("b017405b-ee84-42c7-800d-714451c0f66a"), ViewTextId = new Guid("f248a1ac-016d-4dbb-a923-eda07d175834"), LocaleId = 2058, Value = @"Introducción" },
                new ViewTextLocale { Id = new Guid("31d14f9d-1313-4a1e-aba2-2c9485b6aca1"), ViewTextId = new Guid("14e3c2b3-66f6-4306-b3f9-627643ec3cec"), LocaleId = 1033, Value = @"ASP.NET MVC gives you a powerful, patterns-based way to build dynamic websites that enables a clean separation of concerns and that gives you full control over markup for enjoyable, agile development. ASP.NET MVC includes many features that enable fast, TDD-friendly development for creating sophisticated applications that use the latest web standards." },
                new ViewTextLocale { Id = new Guid("e686afbc-c3a2-4419-9d79-cf4b5f56fd29"), ViewTextId = new Guid("14e3c2b3-66f6-4306-b3f9-627643ec3cec"), LocaleId = 2058, Value = @"ASP.NET MVC le da una manera poderosa, basada en patrones para construir sitios web dinámicos que permiten una separación clara de las preocupaciones y que le da un control total sobre el marcado de agradable, desarrollo ágil. ASP.NET MVC incluye muchas características que permiten el desarrollo rápido, TDD de usar para la creación de aplicaciones sofisticadas que utilizan los últimos estándares web." },
                new ViewTextLocale { Id = new Guid("dd1c07ad-1f48-4f70-8ea9-90b982b02cba"), ViewTextId = new Guid("f636da7a-ee0d-44be-8373-c4148826f1ff"), LocaleId = 1033, Value = @"Add NuGet packages and jump-start your coding" },
                new ViewTextLocale { Id = new Guid("7399ce37-3bf1-4d8e-922b-9ede85d2abc6"), ViewTextId = new Guid("f636da7a-ee0d-44be-8373-c4148826f1ff"), LocaleId = 2058, Value = @"Agregar paquetes NuGet y poner en marcha su codificación" },
                new ViewTextLocale { Id = new Guid("e4d67bab-8cd2-4ef1-9df5-31d50d029814"), ViewTextId = new Guid("346c865b-506d-460c-ba74-ce6b70c2250d"), LocaleId = 1033, Value = @"NuGet makes it easy to install and update free libraries and tools." },
                new ViewTextLocale { Id = new Guid("c6675885-d99e-4b08-8446-366610b86605"), ViewTextId = new Guid("346c865b-506d-460c-ba74-ce6b70c2250d"), LocaleId = 2058, Value = @"NuGet hace que sea fácil de instalar y actualizar las bibliotecas y herramientas libres." },
                new ViewTextLocale { Id = new Guid("c8508692-2472-4c50-95b7-280bd8e042d8"), ViewTextId = new Guid("7f1600db-da73-4784-bff3-b648d5a95fd3"), LocaleId = 1033, Value = @"Find Web Hosting" },
                new ViewTextLocale { Id = new Guid("6c01f160-bae2-4bf4-bf6b-a2e5c78ad481"), ViewTextId = new Guid("7f1600db-da73-4784-bff3-b648d5a95fd3"), LocaleId = 2058, Value = @"Encontrar Web Hosting" },
                new ViewTextLocale { Id = new Guid("f5443770-d926-4627-97f0-0af0bb711898"), ViewTextId = new Guid("fa5df3ad-7881-4dec-bd00-ae6fc702bc33"), LocaleId = 1033, Value = @"You can easily find a web hosting company that offers the right mix of features and price for your applications." },
                new ViewTextLocale { Id = new Guid("77c32c2f-624b-46dc-adaf-010ce49c699b"), ViewTextId = new Guid("fa5df3ad-7881-4dec-bd00-ae6fc702bc33"), LocaleId = 2058, Value = @"Usted puede encontrar fácilmente una empresa de alojamiento web que ofrece la combinación perfecta de características y precios para sus aplicaciones." }

            };
            table.ForEach(x => context.ViewTextLocale.Add(x));
            context.SaveChanges();
        }

        private void SeedAssemblyType(ComplexCommerce context)
        {
            var table = new List<AssemblyType>
            {
                // System Views (should always populate this data)
                new AssemblyType { Id = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TenantId = 3, TypeName = "ComplexCommerce.Models.HomeViewModel", TypeNameHashCode = "ComplexCommerce.Models.HomeViewModel".GetHashCode() },
                new AssemblyType { Id = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TenantId = 3, TypeName = "ComplexCommerce.Business.Catalog.IProduct", TypeNameHashCode = "ComplexCommerce.Business.Catalog.IProduct".GetHashCode() }
                //new AssemblyType { Id = new Guid("a4a02c26-0fda-4c42-9f08-35b30615ccca"), TenantId = 3, TypeName = "~/Views/System/Status500.cshtml", TypeNameHashCode = "~/Views/System/Status500.cshtml".GetHashCode() },

                //new AssemblyType { Id = new Guid("c9c31471-0ea7-4c19-a2a6-086cefab21a6"), TenantId = 3, TypeName = "~/Views/Shared/_Layout.cshtml", TypeNameHashCode = "~/Views/Shared/_Layout.cshtml".GetHashCode() },
                //new AssemblyType { Id = new Guid("46ea2251-591a-4d19-9a35-4ba9a2dc9b3a"), TenantId = 3, TypeName = "~/Views/Shared/_LoginPartial.cshtml", TypeNameHashCode = "~/Views/Shared/_LoginPartial.cshtml".GetHashCode() },
                //new AssemblyType { Id = new Guid("a793c1fe-a610-40a6-9c90-7425dab1e237"), TenantId = 3, TypeName = "~/Views/Home/Index.cshtml", TypeNameHashCode = "~/Views/Home/Index.cshtml".GetHashCode() }
                
            };
            table.ForEach(x => context.AssemblyType.Add(x));
            context.SaveChanges();
        }

        private void SeedAssemblyTypeText(ComplexCommerce context)
        {
            var table = new List<AssemblyTypeText>
            {
                new AssemblyTypeText { Id = new Guid("4091858b-633f-4bcb-9a97-3fc3ef744e9b"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Name" },
                new AssemblyTypeText { Id = new Guid("d7c0d308-d5a1-4847-8268-5b66fd72af0d"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Name_Watermark" },

                new AssemblyTypeText { Id = new Guid("2aa17195-e151-448a-998e-f45dc6094a81"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Name_Description" },
                new AssemblyTypeText { Id = new Guid("4d0f8aa5-82fc-4071-a71e-cf26a982e53f"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Name_NullDisplayText" },
                new AssemblyTypeText { Id = new Guid("ae3fcdda-b629-43c2-9e9e-57f817ff83c2"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Name_StringLengthAttribute" },
                new AssemblyTypeText { Id = new Guid("0868620a-773b-45ba-9bc8-990df340c61a"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Name_RequiredAttribute" },

                





                new AssemblyTypeText { Id = new Guid("78fd3ec0-ce92-4702-8375-d4b18ce41731"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Age" },
                new AssemblyTypeText { Id = new Guid("b7dae7b9-49d5-4918-a712-55364fc7203c"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Age_RequiredAttribute" },


                new AssemblyTypeText { Id = new Guid("c358e463-8a89-434c-90cc-9578732a19c0"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Password1" },

                new AssemblyTypeText { Id = new Guid("efd36a89-e9bd-4e40-85b6-bde323622f76"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Password1_RequiredAttribute" },
                new AssemblyTypeText { Id = new Guid("635fb9d0-e03f-456c-9df6-d7c97519f113"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Password2" },
                //new AssemblyTypeText { Id = new Guid("90eb018e-30fa-465f-b92f-c431c77d8127"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Log_In" },
                //new AssemblyTypeText { Id = new Guid("96a17e9e-456f-44bd-9162-fe6c005f8ef9"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Register" },
                //new AssemblyTypeText { Id = new Guid("4eb0e460-d7da-4019-a76c-f864d0b6e7b5"), AssemblyTypeId = new Guid("f9d4bc14-3db0-46f7-97d7-441b8c9cd967"), TextName = "Log_Off" },

                new AssemblyTypeText { Id = new Guid("cb9772ec-3a5b-4b9a-ad76-79e4eca6cb2a"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "SKU" },
                new AssemblyTypeText { Id = new Guid("79723003-8062-489b-9e69-64032b2b3299"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "ImageUrl" },
                new AssemblyTypeText { Id = new Guid("388265e4-fb45-4701-a478-19d312b9910e"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "ImageUrl_NullDisplayText" },
                new AssemblyTypeText { Id = new Guid("af09bc00-5011-41b7-92f3-ecb12f1e3562"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Price" }
                //new AssemblyTypeText { Id = new Guid("091857b8-b22c-4139-a0e1-f7276c27e5e3"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "We_Suggest" },
                //new AssemblyTypeText { Id = new Guid("41ac2b17-1546-4a0d-af5c-f12c8254b8ae"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Learn_More" },
                //new AssemblyTypeText { Id = new Guid("f248a1ac-016d-4dbb-a923-eda07d175834"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Getting_Started_Header" },
                //new AssemblyTypeText { Id = new Guid("14e3c2b3-66f6-4306-b3f9-627643ec3cec"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Getting_Started" },
                //new AssemblyTypeText { Id = new Guid("f636da7a-ee0d-44be-8373-c4148826f1ff"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Add_Nuget_Header" },
                //new AssemblyTypeText { Id = new Guid("346c865b-506d-460c-ba74-ce6b70c2250d"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Add_Nuget" },
                //new AssemblyTypeText { Id = new Guid("7f1600db-da73-4784-bff3-b648d5a95fd3"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Find_Hosting_Header" },
                //new AssemblyTypeText { Id = new Guid("fa5df3ad-7881-4dec-bd00-ae6fc702bc33"), AssemblyTypeId = new Guid("57a8c17c-d171-41f7-b4e2-535325a381f8"), TextName = "Find_Hosting" }
                
                };
            table.ForEach(x => context.AssemblyTypeText.Add(x));
            context.SaveChanges();
        }

        private void SeedAssemblyTypeTextLocale(ComplexCommerce context)
        {
            var table = new List<AssemblyTypeTextLocale>
            {
                new AssemblyTypeTextLocale { Id = new Guid("2bf42417-7530-4a4a-8afc-20139bc47c10"), AssemblyTypeTextId = new Guid("4091858b-633f-4bcb-9a97-3fc3ef744e9b"), LocaleId = 1033, Value = @"Name" },
                new AssemblyTypeTextLocale { Id = new Guid("01780846-5f34-4fb6-8dfb-c4536ea05046"), AssemblyTypeTextId = new Guid("4091858b-633f-4bcb-9a97-3fc3ef744e9b"), LocaleId = 2058, Value = @"Nombre" },
                new AssemblyTypeTextLocale { Id = new Guid("6d4b2397-3615-4c0f-a359-45b8ea8eb374"), AssemblyTypeTextId = new Guid("d7c0d308-d5a1-4847-8268-5b66fd72af0d"), LocaleId = 1033, Value = @"Water mark??" },
                new AssemblyTypeTextLocale { Id = new Guid("6d980d8c-2c9a-44b0-9e7e-6c722e9fa217"), AssemblyTypeTextId = new Guid("d7c0d308-d5a1-4847-8268-5b66fd72af0d"), LocaleId = 2058, Value = @"Marca de agua" },

                new AssemblyTypeTextLocale { Id = new Guid("79bf3c3a-b3a4-4a97-b353-3e38cfcd3196"), AssemblyTypeTextId = new Guid("2aa17195-e151-448a-998e-f45dc6094a81"), LocaleId = 1033, Value = @"Enter your name, dude" },
                new AssemblyTypeTextLocale { Id = new Guid("61bac5ee-51cb-4824-9d68-ab8cf41b3ee2"), AssemblyTypeTextId = new Guid("2aa17195-e151-448a-998e-f45dc6094a81"), LocaleId = 2058, Value = @"Escriba su nombre, amigo" },
                new AssemblyTypeTextLocale { Id = new Guid("ba1ab181-66e0-4c5f-8d55-3bae1b23b1fc"), AssemblyTypeTextId = new Guid("4d0f8aa5-82fc-4071-a71e-cf26a982e53f"), LocaleId = 1033, Value = @"Enter something here" },
                new AssemblyTypeTextLocale { Id = new Guid("5817c9a6-b902-428e-8aa3-9c99c53823bf"), AssemblyTypeTextId = new Guid("4d0f8aa5-82fc-4071-a71e-cf26a982e53f"), LocaleId = 2058, Value = @"Oops, wrong language" },
                new AssemblyTypeTextLocale { Id = new Guid("1cbbef8f-3dc8-455d-be25-6e3be0ea9913"), AssemblyTypeTextId = new Guid("ae3fcdda-b629-43c2-9e9e-57f817ff83c2"), LocaleId = 1033, Value = @"Name cannot be longer than 40 characters" },
                new AssemblyTypeTextLocale { Id = new Guid("9bab4706-cc40-47b2-987f-b7974fe55b2a"), AssemblyTypeTextId = new Guid("ae3fcdda-b629-43c2-9e9e-57f817ff83c2"), LocaleId = 2058, Value = @"El nombre no puede tener más de 40 caracteres" },
                new AssemblyTypeTextLocale { Id = new Guid("96b3d49a-57b6-4eea-8f6c-0ad012249adb"), AssemblyTypeTextId = new Guid("0868620a-773b-45ba-9bc8-990df340c61a"), LocaleId = 1033, Value = @"Name is required" },
                new AssemblyTypeTextLocale { Id = new Guid("afd81c9c-d705-4bd8-ae7c-8bdfb7ef5fb6"), AssemblyTypeTextId = new Guid("0868620a-773b-45ba-9bc8-990df340c61a"), LocaleId = 2058, Value = @"El nombre es requerido" },


                new AssemblyTypeTextLocale { Id = new Guid("5c5f3b73-117d-47a0-84b2-49dda9756c79"), AssemblyTypeTextId = new Guid("78fd3ec0-ce92-4702-8375-d4b18ce41731"), LocaleId = 1033, Value = @"Age" },
                new AssemblyTypeTextLocale { Id = new Guid("3890ebb6-547a-44fc-bd1a-5861e5ae14a8"), AssemblyTypeTextId = new Guid("78fd3ec0-ce92-4702-8375-d4b18ce41731"), LocaleId = 2058, Value = @"Edad" },
                new AssemblyTypeTextLocale { Id = new Guid("38755a83-75df-4db9-a9ce-a0b66f4912d8"), AssemblyTypeTextId = new Guid("b7dae7b9-49d5-4918-a712-55364fc7203c"), LocaleId = 1033, Value = @"Age is required" },
                new AssemblyTypeTextLocale { Id = new Guid("d86bdc97-4d86-486e-86e9-e87f2c43fe9f"), AssemblyTypeTextId = new Guid("b7dae7b9-49d5-4918-a712-55364fc7203c"), LocaleId = 2058, Value = @"Se requiere edad" },


                new AssemblyTypeTextLocale { Id = new Guid("201e0c87-1ceb-4ece-bb6d-4882badb5848"), AssemblyTypeTextId = new Guid("c358e463-8a89-434c-90cc-9578732a19c0"), LocaleId = 1033, Value = @"Password" },
                new AssemblyTypeTextLocale { Id = new Guid("4e010569-0c94-42a0-b2cb-d255b36bb967"), AssemblyTypeTextId = new Guid("c358e463-8a89-434c-90cc-9578732a19c0"), LocaleId = 2058, Value = @"Contraseña" },

                new AssemblyTypeTextLocale { Id = new Guid("028ee1ed-362c-43af-a5ec-eb08c397b4af"), AssemblyTypeTextId = new Guid("efd36a89-e9bd-4e40-85b6-bde323622f76"), LocaleId = 1033, Value = @"Password is required" },
                new AssemblyTypeTextLocale { Id = new Guid("79a00cbe-96b1-40b8-8001-3686484304b3"), AssemblyTypeTextId = new Guid("efd36a89-e9bd-4e40-85b6-bde323622f76"), LocaleId = 2058, Value = @"Se requiere contraseña" },
                new AssemblyTypeTextLocale { Id = new Guid("a2cffe63-0d0f-46b4-a219-d861a6c85503"), AssemblyTypeTextId = new Guid("635fb9d0-e03f-456c-9df6-d7c97519f113"), LocaleId = 1033, Value = @"Confirm Password" },
                new AssemblyTypeTextLocale { Id = new Guid("d67fc117-64b0-4f3b-aa88-5ba6c86289ab"), AssemblyTypeTextId = new Guid("635fb9d0-e03f-456c-9df6-d7c97519f113"), LocaleId = 2058, Value = @"Confirmar Contraseña" },
                //new AssemblyTypeTextLocale { Id = new Guid("b2da7909-60c4-4a13-b259-49a84f380c59"), AssemblyTypeTextId = new Guid("8e32a053-44fc-4493-b3f2-783dd88d51cf"), LocaleId = 1033, Value = @"Log In" },
                //new AssemblyTypeTextLocale { Id = new Guid("91a7cda1-685a-43ef-9c3d-a4f1e4704f80"), AssemblyTypeTextId = new Guid("8e32a053-44fc-4493-b3f2-783dd88d51cf"), LocaleId = 2058, Value = @"Iniciar La Sesión" },
                //new AssemblyTypeTextLocale { Id = new Guid("4ba1e9df-d064-4823-9d8d-bba8cd8e12ed"), AssemblyTypeTextId = new Guid("ca4b9594-d961-47d4-9b9f-16c116ac18f2"), LocaleId = 1033, Value = @"Register" },
                //new AssemblyTypeTextLocale { Id = new Guid("0944bdcb-8f93-4644-a2c9-20d7dbf6a435"), AssemblyTypeTextId = new Guid("ca4b9594-d961-47d4-9b9f-16c116ac18f2"), LocaleId = 2058, Value = @"Registro" },
                //new AssemblyTypeTextLocale { Id = new Guid("26d8c4f5-4ea8-47af-ad4a-beca0126313a"), AssemblyTypeTextId = new Guid("9ec6c5ba-ffc1-42a8-a82e-e1d11c88c8a0"), LocaleId = 1033, Value = @"Log Off" },
                //new AssemblyTypeTextLocale { Id = new Guid("58067397-d113-4396-a165-782980882905"), AssemblyTypeTextId = new Guid("9ec6c5ba-ffc1-42a8-a82e-e1d11c88c8a0"), LocaleId = 2058, Value = @"Salir Del Sistema" },


                new AssemblyTypeTextLocale { Id = new Guid("6e4b881b-d0fa-46b9-af38-3ea886ed5002"), AssemblyTypeTextId = new Guid("cb9772ec-3a5b-4b9a-ad76-79e4eca6cb2a"), LocaleId = 1033, Value = @"SKU" },
                new AssemblyTypeTextLocale { Id = new Guid("42121edc-089b-447e-8c03-5a361ee3a467"), AssemblyTypeTextId = new Guid("cb9772ec-3a5b-4b9a-ad76-79e4eca6cb2a"), LocaleId = 2058, Value = @"SKU" },
                new AssemblyTypeTextLocale { Id = new Guid("1bff9c9b-f563-4512-8787-7ad00521f73e"), AssemblyTypeTextId = new Guid("79723003-8062-489b-9e69-64032b2b3299"), LocaleId = 1033, Value = @"Image URL" },
                new AssemblyTypeTextLocale { Id = new Guid("c01d4743-5ce2-4f1a-b2ae-516a21b6ae38"), AssemblyTypeTextId = new Guid("79723003-8062-489b-9e69-64032b2b3299"), LocaleId = 2058, Value = @"URL de la imagen" },
                new AssemblyTypeTextLocale { Id = new Guid("58fdc243-0d4e-4f0c-9d0e-ac878dd1ab6c"), AssemblyTypeTextId = new Guid("388265e4-fb45-4701-a478-19d312b9910e"), LocaleId = 1033, Value = @"Nothing" },
                new AssemblyTypeTextLocale { Id = new Guid("7547cc5c-80e3-469c-bc1b-01e9de8643ea"), AssemblyTypeTextId = new Guid("388265e4-fb45-4701-a478-19d312b9910e"), LocaleId = 2058, Value = @"Nada" },
                new AssemblyTypeTextLocale { Id = new Guid("f2cab486-b4f1-4dd4-9a99-eed2cd5dcf84"), AssemblyTypeTextId = new Guid("af09bc00-5011-41b7-92f3-ecb12f1e3562"), LocaleId = 1033, Value = @"Price" },
                new AssemblyTypeTextLocale { Id = new Guid("25117cac-ed87-4a82-8e0f-d105a83b7e69"), AssemblyTypeTextId = new Guid("af09bc00-5011-41b7-92f3-ecb12f1e3562"), LocaleId = 2058, Value = @"Precio" }
                //new AssemblyTypeTextLocale { Id = new Guid("1061be78-a524-48a3-914a-b79045b605a4"), AssemblyTypeTextId = new Guid("091857b8-b22c-4139-a0e1-f7276c27e5e3"), LocaleId = 1033, Value = @"We suggest the following:" },
                //new AssemblyTypeTextLocale { Id = new Guid("49dcbbec-79af-43f1-8be7-b4dbcbb6f77a"), AssemblyTypeTextId = new Guid("091857b8-b22c-4139-a0e1-f7276c27e5e3"), LocaleId = 2058, Value = @"Le sugerimos lo siguiente:" },
                //new AssemblyTypeTextLocale { Id = new Guid("225444b3-9d44-49ac-987f-b5edcb63a4bd"), AssemblyTypeTextId = new Guid("f248a1ac-016d-4dbb-a923-eda07d175834"), LocaleId = 1033, Value = @"Getting Started" },
                //new AssemblyTypeTextLocale { Id = new Guid("b017405b-ee84-42c7-800d-714451c0f66a"), AssemblyTypeTextId = new Guid("f248a1ac-016d-4dbb-a923-eda07d175834"), LocaleId = 2058, Value = @"Introducción" },
                //new AssemblyTypeTextLocale { Id = new Guid("31d14f9d-1313-4a1e-aba2-2c9485b6aca1"), AssemblyTypeTextId = new Guid("14e3c2b3-66f6-4306-b3f9-627643ec3cec"), LocaleId = 1033, Value = @"ASP.NET MVC gives you a powerful, patterns-based way to build dynamic websites that enables a clean separation of concerns and that gives you full control over markup for enjoyable, agile development. ASP.NET MVC includes many features that enable fast, TDD-friendly development for creating sophisticated applications that use the latest web standards." },
                //new AssemblyTypeTextLocale { Id = new Guid("e686afbc-c3a2-4419-9d79-cf4b5f56fd29"), AssemblyTypeTextId = new Guid("14e3c2b3-66f6-4306-b3f9-627643ec3cec"), LocaleId = 2058, Value = @"ASP.NET MVC le da una manera poderosa, basada en patrones para construir sitios web dinámicos que permiten una separación clara de las preocupaciones y que le da un control total sobre el marcado de agradable, desarrollo ágil. ASP.NET MVC incluye muchas características que permiten el desarrollo rápido, TDD de usar para la creación de aplicaciones sofisticadas que utilizan los últimos estándares web." },
                //new AssemblyTypeTextLocale { Id = new Guid("dd1c07ad-1f48-4f70-8ea9-90b982b02cba"), AssemblyTypeTextId = new Guid("f636da7a-ee0d-44be-8373-c4148826f1ff"), LocaleId = 1033, Value = @"Add NuGet packages and jump-start your coding" },
                //new AssemblyTypeTextLocale { Id = new Guid("7399ce37-3bf1-4d8e-922b-9ede85d2abc6"), AssemblyTypeTextId = new Guid("f636da7a-ee0d-44be-8373-c4148826f1ff"), LocaleId = 2058, Value = @"Agregar paquetes NuGet y poner en marcha su codificación" },
                //new AssemblyTypeTextLocale { Id = new Guid("e4d67bab-8cd2-4ef1-9df5-31d50d029814"), AssemblyTypeTextId = new Guid("346c865b-506d-460c-ba74-ce6b70c2250d"), LocaleId = 1033, Value = @"NuGet makes it easy to install and update free libraries and tools." },
                //new AssemblyTypeTextLocale { Id = new Guid("c6675885-d99e-4b08-8446-366610b86605"), AssemblyTypeTextId = new Guid("346c865b-506d-460c-ba74-ce6b70c2250d"), LocaleId = 2058, Value = @"NuGet hace que sea fácil de instalar y actualizar las bibliotecas y herramientas libres." },
                //new AssemblyTypeTextLocale { Id = new Guid("c8508692-2472-4c50-95b7-280bd8e042d8"), AssemblyTypeTextId = new Guid("7f1600db-da73-4784-bff3-b648d5a95fd3"), LocaleId = 1033, Value = @"Find Web Hosting" },
                //new AssemblyTypeTextLocale { Id = new Guid("6c01f160-bae2-4bf4-bf6b-a2e5c78ad481"), AssemblyTypeTextId = new Guid("7f1600db-da73-4784-bff3-b648d5a95fd3"), LocaleId = 2058, Value = @"Encontrar Web Hosting" },
                //new AssemblyTypeTextLocale { Id = new Guid("f5443770-d926-4627-97f0-0af0bb711898"), AssemblyTypeTextId = new Guid("fa5df3ad-7881-4dec-bd00-ae6fc702bc33"), LocaleId = 1033, Value = @"You can easily find a web hosting company that offers the right mix of features and price for your applications." },
                //new AssemblyTypeTextLocale { Id = new Guid("77c32c2f-624b-46dc-adaf-010ce49c699b"), AssemblyTypeTextId = new Guid("fa5df3ad-7881-4dec-bd00-ae6fc702bc33"), LocaleId = 2058, Value = @"Usted puede encontrar fácilmente una empresa de alojamiento web que ofrece la combinación perfecta de características y precios para sus aplicaciones." }

            };
            table.ForEach(x => context.AssemblyTypeTextLocale.Add(x));
            context.SaveChanges();
        }
    }
}
