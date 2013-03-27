using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplexCommerce.Business;

namespace ComplexCommerce.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(
            ICategoryFactory categoryFactory
            )
        {
            if (categoryFactory == null)
                throw new ArgumentNullException("categoryFactory");
            this.categoryFactory = categoryFactory;
        }

        private readonly ICategoryFactory categoryFactory;

        //
        // GET: /Category/

        //public ActionResult Index(Guid id)
        //{
        //    ViewData.Model = categoryFactory.GetCategory(id);
        //    return View();
        //}

        public ActionResult Details(Guid id)
        {
            ViewData.Model = categoryFactory.GetCategory(id);
            return View();
        }
    }
}
