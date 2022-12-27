using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class PaymentController : Controller
    {
        WebBanHangEntities objWebBanHangEntities = new WebBanHangEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //lấy thông tin giỏ hàng từ biến section 
                var listCart = (List<ShoppingCartModel>)Session["cart"];
                //gán dữ liệu cho order
                Order objOrder = new Order();
                objOrder.OrderName = "Don Hang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.Id = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.OrderStatus = 1;
                objWebBanHangEntities.Orders.Add(objOrder);
                //lưu thông  tin dữ liệu vào bảng order
                objWebBanHangEntities.SaveChanges();

                //lấy OrderId vừa mới tạo để lưu vào bảng OrderDetail.
                int intOrderId = objOrder.Id;

                List<OrderDetail> listOrderDetail = new List<OrderDetail>();
                foreach (var item in listCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    listOrderDetail.Add(obj);
                }
                objWebBanHangEntities.OrderDetails.AddRange(listOrderDetail);
                objWebBanHangEntities.SaveChanges();
            }    
            return View();
        }
    }
}