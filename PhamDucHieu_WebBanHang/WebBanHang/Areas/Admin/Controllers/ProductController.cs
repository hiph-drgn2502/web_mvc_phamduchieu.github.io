using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using static WebBanHang.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebBanHangEntities objWebBanHangEntities = new WebBanHangEntities();
        // Danh sách sản phẩm
        public ActionResult Index()
        {
            var listProduct = objWebBanHangEntities.Products.ToList();
            return View(listProduct);
        }
        //Chi tiết sản phẩm
        public ActionResult Details(int Id)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        //Thêm sản phẩm
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageFile != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageFile.FileName);
                        //ten hinh
                        string extension = Path.GetExtension(objProduct.ImageFile.FileName);
                        //png
                        fileName = fileName + extension;
                        //fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        //tenhinh.png
                        objProduct.Avartar = fileName;
                        objProduct.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images/all_product"), fileName));
                    }
                    objProduct.CreateOnUtc = DateTime.Now;
                    objWebBanHangEntities.Products.Add(objProduct);
                    objWebBanHangEntities.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }

            }
            return View();
        }
        //Tách hàm thêm sản phẩm ra và gọi nó vào trong hàm "public ActionResult Create(Product objProduct)"
        [ValidateInput(false)]
        void LoadData()
        {
            Common objCommon = new Common();
            //Lấy dữ liệu danh mục dưới DB lên
            var listCat = objWebBanHangEntities.Categories.ToList();
            //Convert  sang select list dạng value, text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(listCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "CategoryName");

            //lấy dữ liệu thương hiệu dưới DB
            var listBrand = objWebBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(listBrand);
            //Convert  sang select list dạng value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "BrandName");

            //Loại sản phẩm
            List<ProductType> listProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Giảm giá sốc";
            listProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 03;
            objProductType.Name = "Đề xuất";
            listProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(listProductType);
            //Convert  sang select list dạng value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }

        //Xóa
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();

            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();

            objWebBanHangEntities.Products.Remove(objProduct);

            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        //Sửa
        [HttpGet]
        public ActionResult Edit(int Id, Product obj_Product)
        {
            var objProduct = objWebBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();

            return View(obj_Product);
        }

        [HttpPost]
        public ActionResult Edit(Product obj_Product)
        {
            if (obj_Product.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(obj_Product.ImageFile.FileName);
                //ten hinh
                string extension = Path.GetExtension(obj_Product.ImageFile.FileName);
                //png
                fileName = fileName + extension;
                //fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                //tenhinh.png
                obj_Product.Avartar = fileName;
                obj_Product.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images/all_product"), fileName));
            }

            objWebBanHangEntities.Entry(obj_Product).State = EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}