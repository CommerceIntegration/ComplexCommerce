using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Data.Repositories
{
    public interface IErrorRepository
    {
        IEnumerable<ErrorDto> ListForApplication(string application, int skip, int take, out int totalCount);
        ErrorDto Fetch(string application, Guid id);
        void Insert(ErrorDto item);
    }
}
