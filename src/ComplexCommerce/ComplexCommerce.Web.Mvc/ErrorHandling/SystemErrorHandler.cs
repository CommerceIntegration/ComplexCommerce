using System;
using System.Net;
using System.Web;
using System.Web.Routing;
using ComplexCommerce.Web.Mvc.Controllers;

namespace ComplexCommerce.Web.Mvc.ErrorHandling
{
    public class SystemErrorHandler 
        : ISystemErrorHandler
    {
        public SystemErrorHandler(
            IHttpContextFactory httpContextFactory,
            ISystemController systemController
            )
        {
            if (httpContextFactory == null)
                throw new ArgumentNullException("httpContextFactory");
            if (systemController == null)
                throw new ArgumentNullException("systemController");

            this.httpContextFactory = httpContextFactory;
            this.systemController = systemController;
        }

        private readonly IHttpContextFactory httpContextFactory;
        private readonly ISystemController systemController;

        public void ProcessUnhandledError()
        {
            var context = httpContextFactory.GetHttpContext();

            // Avoid IIS7 getting in the middle
            context.Response.TrySkipIisCustomErrors = true;

            if (!context.IsDebuggingEnabled)
            {
                var exception = context.Server.GetLastError();
                var httpException = exception as HttpException;

                context.Response.Clear();
                context.Server.ClearError();
                var routeData = new RouteData();
                routeData.Values["controller"] = "System";
                routeData.Values["action"] = "Status500";
                routeData.Values["exception"] = exception;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                if (httpException != null)
                {
                    context.Response.StatusCode = httpException.GetHttpCode();
                    switch (context.Response.StatusCode)
                    {
                        //case 403:
                        //    routeData.Values["action"] = "Status403";
                        //    break;
                        case 404:
                            routeData.Values["action"] = "Status404";
                            break;
                    }
                }
                var requestContext = new RequestContext(context, routeData);
                systemController.Execute(requestContext);
            }
        }
    }
}
