using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.HRBL;
using BLL.LanguageBL;
using DAL.Entities;
using web.Areas.Admin.Filters;
using System.IO;
using web.Areas.Admin.Helpers;
using System.Web.Script.Serialization;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class HumanResourceController : Controller
    {
        //
        // GET: /Admin/HumanResource/

        public ActionResult Index()
        {
            string lang = FillLanguagesList();

            var vision_info = HumanResourceManager.GetHRByLanguage(lang);
            return View(vision_info);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(HumanResource record)
        {
            string lang = FillLanguagesList();
            record.Language = lang;

            ViewBag.ProcessMessage = HumanResourceManager.EditHumanResource(record);


            return View();
        }

        public ActionResult HumanResourcePositions()
        {
            string sellang = FillLanguagesList();

            var referncelist = HumanResourceManager.GetHumanResourcePositionList(sellang);
            return View(referncelist);
        }

        public ActionResult AddHumanResourcePosition()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddHumanResourcePosition(HumanResourcePosition newmodel)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (ModelState.IsValid)
            {
                newmodel.SortOrder = 9999;
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = HumanResourceManager.AddHumanResourcePosition(newmodel);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult EditHumanResourcePosition()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    HumanResourcePosition editHumanResourcePosition = HumanResourceManager.GetHumanResourcePositionById(nid);
                    return View(editHumanResourcePosition);
                }
                else
                    return View();
            }
            else
                return View();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditHumanResourcePosition(HumanResourcePosition HumanResourcePositionmodel)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
                
                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        HumanResourcePositionmodel.HumanResourcePositionId = nid;
                        ViewBag.ProcessMessage = HumanResourceManager.EditHumanResourcePosition(HumanResourcePositionmodel);
                        return View(HumanResourcePositionmodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(HumanResourcePositionmodel);
                    }
                }
                else return View();
            }
            else
                return View();

            return View();
        }


        public JsonResult HumanResourcePositionEditStatus(int id)
        {
            string NowState;
            bool isupdate = HumanResourceManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult DeleteHumanResourcePosition(int id)
        {
            bool isdelete = HumanResourceManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = HumanResourceManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
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
    }
}
