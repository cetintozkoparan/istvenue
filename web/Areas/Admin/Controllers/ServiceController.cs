using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.ServiceBL;
using DAL.Entities;
using web.Areas.Admin.Filters;
using BLL.ServiceGroupBL;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class ServiceController : Controller
    {
        //
        // GET: /Admin/Service/

        public ActionResult Index()
        {
            string id = FillLanguagesList();
            int groupid = Convert.ToInt32(id);

            var list = ServiceManager.GetServiceList(groupid);
            return View(list);
        }

        public ActionResult OurServices()
        {
            FillLanguagesList();

            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var servicepage = ServiceManager.GetOurServices(lang);
            return View(servicepage);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OurServices(OurServices record)
        {
            FillLanguagesList();
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            record.Language = lang;
            ViewBag.ProcessMessage = ServiceManager.EditOurServices(record);
            return View();
        }

        public ActionResult AddService()
        {
            FillLanguagesList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddService(Service newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
               
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.Name);
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = ServiceManager.AddService(newmodel);
                ModelState.Clear();

                return View();
            }
            else
                return View();
        }


        public ActionResult EditService()
        {
            FillLanguagesList();

            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Service editrecord = ServiceManager.GetServiceById(nid);
                    return View(editrecord);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditService(Service newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
               
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.Name);

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        newmodel.ServiceId = nid;
                        ViewBag.ProcessMessage = ServiceManager.EditService(newmodel);
                        return View(newmodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(newmodel);
                    }
                }
                else return View();
            }
            else
                return View();
        }

        string FillLanguagesList()
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = ServiceGroupManager.GetServiceGroupList(lang);

            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().ServiceGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();


            var grouplist = new SelectList(groups, "ServiceGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;

            return id;
        }


        public JsonResult EditStatus(int id)
        {
            string NowState;
            bool isupdate = ServiceManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult Delete(int id)
        {
            bool isdelete = ServiceManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = ServiceGroupManager.GetServiceGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "ServiceGroupId", "GroupName"));
            return result;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ServiceManager.SortRecords(idsList);
            return Json(issorted);
        }
        public class JsonList
        {
            public string[] list { get; set; }
        }
       
    }
}
