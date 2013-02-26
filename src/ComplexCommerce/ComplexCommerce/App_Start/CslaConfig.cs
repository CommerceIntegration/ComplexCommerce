using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Csla.Web.Mvc;

namespace ComplexCommerce
{
    public class CslaConfig
    {
        public static void RegisterCsla()
        {
            //CSLA 4 Configuration
            ModelBinders.Binders.DefaultBinder = new CslaModelBinder();
        }
    }
}