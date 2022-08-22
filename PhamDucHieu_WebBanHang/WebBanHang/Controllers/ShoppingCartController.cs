using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ShoppingCartController : Controller
    {
        WebBanHangEntities objWebBanHangEntities = new WebBanHangEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View((List<ShoppingCartModel>)Session["cart"]);
        }

        public ActionResult AddToCart(int Id, int quantity)
        {

            if (Session["cart"] == null)
            {
                List<ShoppingCartModel> cart = new List<ShoppingCartModel>();
                cart.Add(new ShoppingCartModel { Product = objWebBanHangEntities.Products.Find(Id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<ShoppingCartModel> cart = (List<ShoppingCartModel>)Session["cart"];
                //Kiểm tra sản phẩm có tồn tại trong giỏ hàng hay chưa ?
                int index = isExist(Id);
                if (index != -1)
                {
                    //Nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //Nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new ShoppingCartModel { Product = objWebBanHangEntities.Products.Find(Id), Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new {Message = "Thành công", JsonRequestBehavior.AllowGet});
        }

        private int isExist(int Id)
        {
            List<ShoppingCartModel> cart = (List<ShoppingCartModel>)Session["cart"];
                for (int i = 0; i < cart.Count; i++)
                    if (cart[i].Product.Id.Equals(Id))
                    {
                        return i;
                    }
                return -1;
                
        }

        //Xóa sản phẩm khỏi giỏ hàng theo Id
        public ActionResult Remove(int Id)
        {
            List<ShoppingCartModel> li = (List<ShoppingCartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.Id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}