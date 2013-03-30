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
        // GET: /System/Status301/?url=(some url)

        public ActionResult Status301(string url)
        {
            // TODO: Add javascript and meta redirects to the view
            // TODO: Localize the view

            ViewBag.DestinationUrl = url;
            return View();
        }

        //
        // GET: /System/Status404

        public ActionResult Status404()
        {
            // TODO: Localize the view
            return View();
        }

        //
        // GET: /System/Status500

        public ActionResult Status500()
        {
            // TODO: Localize the view
            return View();
        }
    }
}
