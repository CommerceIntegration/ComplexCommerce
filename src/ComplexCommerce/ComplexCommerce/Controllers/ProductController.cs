using System;
using System.Web.Mvc;
using ComplexCommerce.Business.Catalog;

namespace ComplexCommerce.Controllers
{
    public class ProductController 
        : Controller
    {
        public ProductController(
            IProductFactory productFactory
            )
        {
            if (productFactory == null)
                throw new ArgumentNullException("productFactory");
            this.productFactory = productFactory;
        }

        private readonly IProductFactory productFactory;

        //
        // GET: /Product/
        public ActionResult Details(Guid id)
        {
            ViewData.Model = productFactory.GetProduct(id);
            return View();
        }
    }
}
