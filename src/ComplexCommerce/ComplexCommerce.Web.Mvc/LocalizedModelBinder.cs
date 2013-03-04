//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Web.Mvc;
//using Csla.Web.Mvc;
//using ComplexCommerce.Business.Context;

//namespace ComplexCommerce.Web.Mvc
//{
//    public class LocalizedModelBinder
//        : CslaModelBinder
//    {
//        public LocalizedModelBinder(
//            IContextUtilities contextUtilities
//            )
//        {
//            if (contextUtilities == null)
//                throw new ArgumentNullException("contextUtilities");

//            this.contextUtilities = contextUtilities;
//        }

//        private readonly IContextUtilities contextUtilities;

//        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
//        {
//            // Get current tenant
//            var tenant = contextUtilities.GetTenantFromContext(controllerContext.HttpContext);
//            ApplicationContext.CurrentTenant = tenant;

//            // Get current culture and set the current thread.
//            var culture = contextUtilities.GetLocaleFromContext(controllerContext.HttpContext, tenant.DefaultLocale);
//            Thread.CurrentThread.CurrentUICulture = culture;
//            Thread.CurrentThread.CurrentCulture = culture;

//            return base.BindModel(controllerContext, bindingContext);
//        }
//    }
//}
