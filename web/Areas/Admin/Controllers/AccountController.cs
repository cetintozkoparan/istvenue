using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Models;
using DAL.Entities;
using BLL.SecurityBL;
using BLL.AccountBL;
using System.Web.Security;

namespace web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login_v2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login_v2(LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                //string password = SecurityManager.EncodeMD5(model.Password);
                if (AccountManager.Login(loginmodel.Email, loginmodel.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ProcessMessage = "false";
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı!");
                }

                return View(loginmodel);

            }
            else
            {
                return View();
            }
        }

       
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login_v2", "Account");

        }

    }
}
