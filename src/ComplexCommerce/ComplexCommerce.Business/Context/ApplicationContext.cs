using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComplexCommerce.Business;
using ComplexCommerce.Data.Dto;
using CslaLibrary = Csla;


namespace ComplexCommerce.Business.Context
{
    public class ApplicationContext
        : IApplicationContext
    {

        const string CurrentTenantName = "complexcommerce.tenant";
        public ITenant CurrentTenant
        {
            get
            {
                var obj = (Tenant)CslaLibrary.ApplicationContext.ClientContext[CurrentTenantName];
                if (obj != null)
                    return obj;
                else
                    return Tenant.NewTenant();
            }
            set
            {
                CslaLibrary.ApplicationContext.ClientContext[CurrentTenantName] = value;
            }
        }

        public int CurrentLocaleId
        {
            get { return Thread.CurrentThread.CurrentUICulture.LCID; }
        }


        public T GetOrAdd<T>(string key, Func<T> loadFunction)
        {
            return GetOrAdd<T>(key, ApplicationContextSourceEnum.LocalContext, loadFunction);
        }

        public T GetOrAdd<T>(string key, ApplicationContextSourceEnum source, Func<T> loadFunction)
        {
            T result = default(T);
            if (!TryGetValue<T>(key, source, out result))
            {
                result = loadFunction();
                var cache = GetContextDictionary(source);
                cache.Add(key, result);
            }
            return result;
        }

        private T Get<T>(string key, ApplicationContextSourceEnum source)
        {
            var cache = GetContextDictionary(source);
            return (T)cache[key];
        }

        private CslaLibrary.Core.ContextDictionary GetContextDictionary(ApplicationContextSourceEnum source)
        {
            CslaLibrary.Core.ContextDictionary result;
            switch (source)
            {
                case ApplicationContextSourceEnum.LocalContext:
                    result = CslaLibrary.ApplicationContext.LocalContext;
                    break;
                case ApplicationContextSourceEnum.ClientContext:
                    result = CslaLibrary.ApplicationContext.ClientContext;
                    break;
                case ApplicationContextSourceEnum.GlobalContext:
                    result = CslaLibrary.ApplicationContext.GlobalContext;
                    break;
                default:
                    result = CslaLibrary.ApplicationContext.LocalContext;
                    break;
            }
            return result;
        }

        private bool TryGetValue<T>(string key, ApplicationContextSourceEnum source, out T value)
        {
            value = this.Get<T>(key, source);
            if (value != null)
            {
                return true;
            }
            return false;
        }
    }
}
