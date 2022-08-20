using PagedList;
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
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var listProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //Lấy ds sản phẩm theo từ khóa tìm kiếm
                listProduct = objWebBanHangEntities.Products.Where(n => n.ProductName.Contains(SearchString)).ToList();
            }
            else
            {
                //Lấy tất cả sản phẩm trong bảng product
                listProduct = objWebBanHangEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //Số lượng item của 1 trang là 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
            //return View(listProduct);
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
        public ActionResult Edit(int Id)
        {
            var obj_Product = objWebBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();

            return View(obj_Product);
        }

        [HttpPost]
        public ActionResult Edit(Product obj_Product)
        {
            //this.LoadData();

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

            //Common objCommon = new Common();
            ////Lấy dữ liệu danh mục dưới DB lên
            //var listCat2 = objWebBanHangEntities.Categories.ToList();
            ////Convert  sang select list dạng value, text
            //ListtoDataTableConverter converter = new ListtoDataTableConverter();
            //DataTable dtCategory1 = converter.ToDataTable(listCat2);
            //ViewBag.ListCategory1 = objCommon.ToSelectList(dtCategory1, "Id", "CategoryName");


            objWebBanHangEntities.Entry(obj_Product).State = EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            //return View(objWebBanHangEntities);
            return RedirectToAction("Index");
        }
    }
}