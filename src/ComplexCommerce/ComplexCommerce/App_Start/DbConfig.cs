using System;
using System.Collections.Generic;
using System.Data.Entity;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Data;
using ComplexCommerce.Data.Entity.Model;

namespace ComplexCommerce
{
    public class DbConfig
    {
        public static void Register(IDependencyInjectionContainer container)
        {
            // Configure AutoMapper - all work done in constructor
            container.Resolve<IDataInitializer>();

            // TODO: Remove the initializer before deploying to production
            Database.SetInitializer(new ComplexCommerceSeeder());
        }
    }
}