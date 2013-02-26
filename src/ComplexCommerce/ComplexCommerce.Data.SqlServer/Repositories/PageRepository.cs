using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.SqlServer.Repositories
{
    public class PageRepository
        : IPageRepository
    {
        private readonly IPersistenceContextFactory contextFactory;

        public PageRepository(IPersistenceContextFactory contextFactory)
        {
            Contract.Requires<ArgumentNullException>(contextFactory == null);

            this.contextFactory = contextFactory;
        }

        #region IPageRepository Members

        public List<SiteMapPageDto> List(int storeId, int localeId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
