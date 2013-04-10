using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Entity.Context;

namespace ComplexCommerce.Data.Entity.Repositories
{
    public class AssemblyTypeRepository
        : IAssemblyTypeRepository
    {
        public AssemblyTypeRepository(IPersistenceContextFactory contextFactory)
        {
            //Contract.Requires<ArgumentNullException>(contextFactory == null);
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");

            this.contextFactory = contextFactory;
        }

        private readonly IPersistenceContextFactory contextFactory;

        #region IAssemblyTypeRepository Members

        public IEnumerable<AssemblyTypeLocaleDto> List(int tenantId, int localeId, int hashCode, string typeName)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from assemblyTypeText in ctx.ObjectContext.AssemblyTypeText
                              join assemblyType in ctx.ObjectContext.AssemblyType
                                  on assemblyTypeText.AssemblyTypeId equals assemblyType.Id
                              join assemblyTypeTextLocale in ctx.ObjectContext.AssemblyTypeTextLocale
                                  on assemblyTypeText.Id equals assemblyTypeTextLocale.AssemblyTypeTextId
                              join tenant in ctx.ObjectContext.Tenant
                                  on assemblyType.TenantId equals tenant.Id

                              where assemblyType.TenantId == tenantId
                              where assemblyType.TypeNameHashCode == hashCode && assemblyType.TypeName == typeName
                              where assemblyTypeTextLocale.LocaleId == localeId || assemblyTypeTextLocale.LocaleId == tenant.DefaultLocaleId
                              select new AssemblyTypeLocaleDto
                              {
                                  TextName = assemblyTypeText.TextName,
                                  Value = assemblyTypeTextLocale.Value,
                                  LocaleId = assemblyTypeTextLocale.LocaleId
                              });

                return result.ToList();
            }
        }

        #endregion
    }
}
