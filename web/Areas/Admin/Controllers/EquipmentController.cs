using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Filters;
using BLL.LanguageBL;
using BLL.LinkBL;
using DAL.Entities;

namespace web.Areas.Admin.Controllers
{
     [AuthenticateUser]
    public class EquipmentController : Controller
    {
        //
        // GET: /Admin/Link/

        public ActionResult Index()
        {
            string sellang = FillLanguagesForList();

            var list = EquipmentManager.GetList(sellang);
            return View(list);
        }

        public ActionResult Add()
        {
            FillLanguagesList();
            return View();
        }


        [HttpPost]
        public ActionResult Add(Equipment model)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
                ModelState.Clear();
                ViewBag.ProcessMessage = EquipmentManager.Add(model);
            }
            return View();
        }


        public ActionResult Edit()
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
                    Equipment editrecord = EquipmentManager.GetById(nid);
                    return View(editrecord);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult Edit(Equipment model)
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
                        model.EquipmentId = nid;
                        ViewBag.ProcessMessage = EquipmentManager.Edit(model);
                        return View(model);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(model);
                    }
                }
                else return View();
            }
            else
                return View();
        }

        public JsonResult EditStatus(int id)
        {
            bool isupdate = EquipmentManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult DeleteRecord(int id)
        {
            bool isdelete = EquipmentManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = EquipmentManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
        string FillLanguagesForList()
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

        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            return lang;
        }


      


    }
}
