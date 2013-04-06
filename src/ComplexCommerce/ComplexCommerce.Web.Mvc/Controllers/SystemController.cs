using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComplexCommerce.Web;

namespace ComplexCommerce.Web.Mvc.Controllers
{
    public class SystemController 
        : Controller
    {
        //
        // GET: /System/Status301/?url=(some url)

        public ActionResult Status301(string url)
        {
            Response.CacheControl = "no-cache";
            Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
            Response.RedirectLocation = url;

            ViewBag.DestinationUrl = url;
            return View();
        }

        //
        // GET: /not-found

        public ActionResult Status404()
        {
            Response.CacheControl = "no-cache";
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            return View();
        }

        //
        // GET: /server-error

        public ActionResult Status500()
        {
            Response.CacheControl = "no-cache";
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return View();
        }
    }
}
