using System;

namespace ComplexCommerce.Web.Mvc.ErrorHandling
{
    public interface ISystemErrorHandler
    {
        void ProcessUnhandledError();
    }
}
