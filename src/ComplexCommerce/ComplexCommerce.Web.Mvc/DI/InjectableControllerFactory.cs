using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Threading.Tasks;
using ComplexCommerce.Shared.DI;

namespace ComplexCommerce.Web.Mvc.DI
{
    public class InjectableControllerFactory 
        : DefaultControllerFactory
    {
        private readonly IDependencyInjectionContainer container;

        public InjectableControllerFactory(IDependencyInjectionContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                try
                {
                    return base.GetControllerInstance(requestContext, controllerType);
                }
                catch (HttpException ex)
                {
                    if (ex.GetHttpCode() == 404)
                    {
                        var routeValues = requestContext.RouteData.Values;

                        routeValues.Clear();
                        routeValues["controller"] = "System";
                        routeValues["action"] = "Status404";

                        return container.Resolve<Controllers.SystemController>() as IController;
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            return container.Resolve(controllerType) as IController;
        }
    }
}
