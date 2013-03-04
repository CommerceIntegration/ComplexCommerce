using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Csla.Web.Mvc;
using ComplexCommerce.Web.Mvc;
using ComplexCommerce.Shared.DI;

namespace ComplexCommerce
{
    public class ModelBinderConfig
    {
        public static void RegisterModelBinder()
        {
            //CSLA 4 Configuration
            ModelBinders.Binders.DefaultBinder = new CslaModelBinder();
            //ModelBinders.Binders.DefaultBinder = container.Resolve(typeof(IModelBinder)) as IModelBinder;
        }
    }
}