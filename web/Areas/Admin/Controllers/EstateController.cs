using BLL.EstateBL;
using BLL.LanguageBL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Helpers;

namespace web.Areas.Admin.Controllers
{
    public class EstateController : Controller
    {
        //
        // GET: /Admin/State/

        public ActionResult Index()
        {
            string lang = FillLanguagesList();
            var estates =EstateManager.GetEstateList(lang);
            

            return View(estates);
        }

        public ActionResult Add()
        {
            var languages = LanguageManager.GetLanguages();
            var countries = EstateManager.GetCountryList();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            var countrylist = new SelectList(countries, "Id", "Name");
            ViewBag.Country = countrylist;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Estate record, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var countries = EstateManager.GetCountryList();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            var countrylist = new SelectList(countries, "Id", "Name");
            ViewBag.Country = countrylist;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 80).SaveThumbnail(uploadfile, "/Content/images/Photos/", Utility.SetPagePlug(record.ReferenceNo.ToString()) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    record.Photo = "/Content/images/Photos/" + Utility.SetPagePlug(record.ReferenceNo.ToString()) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    record.Photo = "/Content/images/front/noimage.jpeg";
                }


                ViewBag.ProcessMessage = EstateManager.AddEstate(record);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }

        public JsonResult GetTowns(int id)
        {
            //return Json(EstateManager.GetTownList(id));
            var towns = EstateManager.GetTownList(id);
            return Json(new SelectList(towns, "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistricts(int id)
        {
            //return Json(EstateManager.GetTownList(id));
            var towns = EstateManager.GetDistrictList(id);
            return Json(new SelectList(towns, "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult DeleteEstate(int id)
        {
            bool isdelete = EstateManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }


    }
}
