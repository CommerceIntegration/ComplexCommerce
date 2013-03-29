using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using AutoMapper;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Entity.Context;
using ComplexCommerce.Data.Entity.Model;

namespace ComplexCommerce.Data.Entity.Repositories
{
    public class TenantRepository
        : ITenantRepository
    {
        public TenantRepository(IPersistenceContextFactory contextFactory)
        {
            //Contract.Requires<ArgumentNullException>(contextFactory == null);
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");

            this.contextFactory = contextFactory;
        }

        private readonly IPersistenceContextFactory contextFactory;

        #region ITenantRepository Members

        public TenantDto Fetch(string host)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                var tenant = (from t in ctx.ObjectContext.Tenant
                             where t.Host == host
                             select t).FirstOrDefault();

                var result = Mapper.Map<Tenant, TenantDto>(tenant);


                //if (result == null)
                //    throw new DataNotFoundException("Store");
                return result;



            }
        }

        #endregion
    }
}
