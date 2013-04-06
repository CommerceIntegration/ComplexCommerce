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
    public class ViewRepository
        : IViewRepository
    {
        public ViewRepository(IPersistenceContextFactory contextFactory)
        {
            //Contract.Requires<ArgumentNullException>(contextFactory == null);
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");

            this.contextFactory = contextFactory;
        }

        private readonly IPersistenceContextFactory contextFactory;



        #region IViewRepository Members

        public IEnumerable<ViewLocaleDto> List(int tenantId, int localeId, int hashCode, string virtualPath)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                //var result = (from view in ctx.ObjectContext.View
                                  
                //              join tenant in ctx.ObjectContext.Tenant
                //                  on view.TenantId equals tenant.Id
                              
                //              // Left Join
                //              join viewLocale in ctx.ObjectContext.ViewLocale
                //                on view.Id equals viewLocale.ViewId into x
                //              from locale in x.DefaultIfEmpty()

                //              // Left Join
                //              join defaultViewLocale in ctx.ObjectContext.ViewLocale
                //                  on view.Id equals defaultViewLocale.ViewId into y
                //              from defaultLocale in y.DefaultIfEmpty()

                //              where view.TenantId == tenantId
                //              where locale.LocaleId == localeId
                //              where defaultLocale != null ? defaultLocale.LocaleId == tenant.DefaultLocaleId : true
                //              select new ViewLocaleDto
                //              {
                //                  TextName = view.TextName,
                //                  Value = locale == null ? viewLocale.Value : 
                //              });

                //var result = (from viewLocale in ctx.ObjectContext.ViewLocale
                //                 join view in ctx.ObjectContext.View
                //                    on viewLocale.ViewId equals view.Id
                //              join tenant in ctx.ObjectContext.Tenant
                //                  on view.TenantId equals tenant.Id

                //              where view.TenantId == tenantId
                //              where view.HashCode == hashCode && view.VirtualPath == virtualPath
                //              where viewLocale.LocaleId == localeId || viewLocale.LocaleId == tenant.DefaultLocaleId
                //              select new ViewLocaleDto
                //              {
                //                  TextName = view.TextName,
                //                  Value = viewLocale.Value,
                //                  LocaleId = viewLocale.LocaleId
                //              });

                //return result.ToList();


                var result = (from viewText in ctx.ObjectContext.ViewText
                              join view in ctx.ObjectContext.View
                                 on viewText.ViewId equals view.Id
                              join viewTextLocale in ctx.ObjectContext.ViewTextLocale
                                  on viewText.Id equals viewTextLocale.ViewTextId
                              join tenant in ctx.ObjectContext.Tenant
                                  on view.TenantId equals tenant.Id

                              where view.TenantId == tenantId
                              where view.VirtualPathHashCode == hashCode && view.VirtualPath == virtualPath
                              where viewTextLocale.LocaleId == localeId || viewTextLocale.LocaleId == tenant.DefaultLocaleId
                              select new ViewLocaleDto
                              {
                                  TextName = viewText.TextName,
                                  Value = viewTextLocale.Value,
                                  LocaleId = viewTextLocale.LocaleId
                              });

                return result.ToList();
            }
        }

        #endregion
    }
}
