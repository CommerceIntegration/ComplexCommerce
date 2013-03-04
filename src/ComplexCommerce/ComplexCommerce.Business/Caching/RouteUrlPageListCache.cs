using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business.Caching
{
    public class RouteUrlPageListCache
        : SingletonObjectCache<RouteUrlPageList>
    {
        public RouteUrlPageListCache(ObjectCache cache, IApplicationContext appContext)
            : base(cache)
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");

            this.appContext = appContext;
        }

        private readonly IApplicationContext appContext;

        public override string Key
        {
            get 
            {
                var tenant = appContext.CurrentTenant;
                return "__CC_RouteUrlPageList_" + tenant.Id; 
            }
        }
    }
}
