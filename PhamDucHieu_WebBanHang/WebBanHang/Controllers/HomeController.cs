using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        WebBanHangEntities objWebBanHangEntities = new WebBanHangEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            //Lấy danh sách sản phẩm 
            objHomeModel.ListCategory = objWebBanHangEntities.Categories.ToList();
            objHomeModel.ListProduct = objWebBanHangEntities.Products.ToList();
            return View(objHomeModel);
        }

        //GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebBanHangEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.PasswordUser = GetMD5(_user.PasswordUser);
                    objWebBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWebBanHangEntities.Users.Add(_user);
                    objWebBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User  objUser)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(objUser.PasswordUser);
                var data = objWebBanHangEntities.Users.Where(s => s.Email.Equals(objUser.Email) && s.PasswordUser.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}