using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FBrifingController : Controller
    {
        //
        // GET: /FBrifing/
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        public ActionResult New()
        {
            ViewBag.Lang = lang;
            return View();
        }

        [HttpPost]
        public ActionResult New(Brifing model)
        {
            try
            {
                MainContext db = new MainContext();
                model.BrifingTip = 0;
                db.Brifing.Add(model);
                db.SaveChanges();

                ViewBag.Result = true;
            }
            catch (Exception ex)
            {
                ViewBag.Result = false;
            }

            ViewBag.Lang = lang;
            return View(new Brifing());
        }

        public ActionResult NewPartial()
        {
            ViewBag.Lang = lang;
            ViewBag.BriefResult = "";
            return PartialView();
        }

        [HttpPost]
        public ActionResult NewPartial(Brifing model)
        {
            try
            {
                MainContext db = new MainContext();
                model.BrifingTip = 0;
                db.Brifing.Add(model);
                db.SaveChanges();

                ViewBag.BriefResult = true;
            }
            catch (Exception ex)
            {
                ViewBag.BriefResult = false;
            }

            ViewBag.Lang = lang;
            return PartialView();
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Detail(Brifing model, FormCollection collection)
        {
            try
            {
               
               
              

                MainContext db = new MainContext();
                var strArray = collection["gayrimenkul"];
                model.Islem = strArray;
                model.BrifingTip = 1;
                //model.
                db.Brifing.Add(model);
                db.SaveChanges();

                ViewBag.Result = true;
            }
            catch (Exception ex)
            {
                ViewBag.Result = false;
            }

            ViewBag.Lang = lang;
            return View();
        }

    }
}
