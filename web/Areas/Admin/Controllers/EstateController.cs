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
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            var countries = EstateManager.GetCountryList();
 
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Estate record = EstateManager.GetEstateById(nid);

                    var countrylist = new SelectList(countries, "Id", "Name",record.CountryId);
                    ViewBag.Country = countrylist;

                    var towns = EstateManager.GetTownList(record.CountryId);
                    var townList = new SelectList(towns, "Id", "Name",record.TownId);
                    ViewBag.Town = townList;

                    var districts = EstateManager.GetDistrictList(record.TownId);
                    var districtsList = new SelectList(districts, "Id", "Name",record.DistrictId);
                    ViewBag.District = districtsList;

                    return View(record);
                }
                else
                    return View();
            }
            else
                return View();
 
        }

        [HttpPost]
        public ActionResult Edit(Estate record, HttpPostedFileBase uploadfile)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/Photos/", Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    record.Photo = "/Content/images/Photos/" + Utility.SetPagePlug(record.Header) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        record.Id = nid;
                        ViewBag.ProcessMessage = EstateManager.EditEstate(record);
                        //return View(record);
                        return RedirectToAction("Index", "Estate");
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(record);
                    }
                }
                else
                    return View();
            }
            else
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
