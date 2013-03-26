using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Business;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Context
{
    public interface IApplicationContext
    {
        ITenant CurrentTenant { get; set; }
        int CurrentLocaleId { get; }
        T GetOrAdd<T>(string key, Func<T> loadFunction);
        T GetOrAdd<T>(string key, ApplicationContextSourceEnum source, Func<T> loadFunction);
    }
}
