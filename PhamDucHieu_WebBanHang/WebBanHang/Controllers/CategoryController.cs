using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WebBanHangEntities objWebBanHangEntities = new WebBanHangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var listCategory = objWebBanHangEntities.Categories.ToList();
            return View(listCategory);
        }

        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objWebBanHangEntities.Products.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}