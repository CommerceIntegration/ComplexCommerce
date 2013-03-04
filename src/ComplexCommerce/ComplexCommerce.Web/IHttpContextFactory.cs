using System;
using System.Web;

namespace ComplexCommerce.Web
{
    public interface IHttpContextFactory
    {
        HttpContextBase GetHttpContext();
    }
}
