using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplexCommerce.Controllers
{
    public class SystemController : Controller
    {
        //
        // GET: /Status301/

        public ActionResult Status301(string url)
        {
            // TODO: Add javascript and meta redirects to the view
            // TODO: Localize the view

            ViewBag.DestinationUrl = url;
            return View();
        }
    }
}
