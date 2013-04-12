using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Entity.Context;
using ComplexCommerce.Data.Entity.Model;
using AutoMapper;

namespace ComplexCommerce.Data.Entity.Repositories
{
    public class ErrorRepository
        : IErrorRepository
    {
        public ErrorRepository(IPersistenceContextFactory contextFactory)
        {
            //Contract.Requires<ArgumentNullException>(contextFactory == null);
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");

            this.contextFactory = contextFactory;
        }

        private readonly IPersistenceContextFactory contextFactory;

        #region IErrorRepository Members

        //public IEnumerable<ErrorDto> ListForTenant(int tenantId, int skip, int take, out int totalCount)
        //{
        //    using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
        //    {
        //        totalCount = (from error in ctx.ObjectContext.Error
        //                    where error.TenantId == tenantId
        //                    select error).ToList().Count();


        //        var result = (from error in ctx.ObjectContext.Error
        //                      where error.TenantId == tenantId
        //                      orderby error.UtcTime, error.Sequence
        //                      select new ErrorDto
        //                      {
        //                          Id = error.Id,
        //                          Host = error.Host,
        //                          Type = error.Type,
        //                          Source = error.Source,
        //                          Message = error.Message,
        //                          User = error.User,
        //                          StatusCode = error.StatusCode,

        //                          //TODO: figure out how to convert the time to the client's local time
        //                          UtcTime = error.UtcTime
        //                      });

        //        return result.Skip(skip).Take(take).ToList();
        //    }
        //}

        public IEnumerable<ErrorDto> ListForApplication(string application, int skip, int take, out int totalCount)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                totalCount = (from error in ctx.ObjectContext.Error
                              where error.Application == application
                              select error).ToList().Count();


                var result = (from error in ctx.ObjectContext.Error
                              where error.Application == application
                              orderby error.UtcTime, error.Sequence
                              select new ErrorDto
                              {
                                  Id = error.Id,
                                  TenantId = error.TenantId,
                                  Application = error.Application,
                                  Host = error.Host,
                                  Type = error.Type,
                                  Source = error.Source,
                                  Message = error.Message,
                                  User = error.User,
                                  StatusCode = error.StatusCode,

                                  //TODO: figure out how to convert the time to the client's local time
                                  UtcTime = error.UtcTime
                              });

                return result.Skip(skip).Take(take).ToList();
            }
        }

        public ErrorDto Fetch(string application, Guid id)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from error in ctx.ObjectContext.Error
                              where error.Id == id
                              where error.Application == application
                              select new ErrorDto
                              {
                                  AllXml = error.AllXml
                              }).FirstOrDefault();

                if (result == null)
                    throw new DataNotFoundException("Error");
                return result;
            }
        }

        public void Insert(ErrorDto item)
        {

            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var newItem = Mapper.Map<ErrorDto, Error>(item);

                ctx.ObjectContext.Error.Add(newItem);
                ctx.ObjectContext.SaveChanges();
                item.Id = newItem.Id;

                // For concurrency tracking
                //item.LastChanged = newItem.LastChanged;
            }
        }

        #endregion
    }
}
