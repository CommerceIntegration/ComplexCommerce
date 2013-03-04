using System;
using System.Web;

namespace ComplexCommerce.Web
{
    public class HttpContextFactory
        : IHttpContextFactory
    {
        #region IHttpContextFactory Members

        public HttpContextBase GetHttpContext()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }

        #endregion
    }
}
