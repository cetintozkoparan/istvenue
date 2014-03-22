using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Filters;
using web.Areas.Admin.Helpers;
using BLL.ProjectReferenceBL;
using BLL.LanguageBL;
using DAL.Entities;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ProjectReferenceGroupController : Controller
    {
        //
        // GET: /Admin/ProjectReferenceGroup/

        public ActionResult Index()
        {
            string lang=FillLanguagesList();
            var grouplist = ProjectReferenceGroupManager.GetProjectReferenceGroupList(lang);
            return View(grouplist);
        }


        [HttpPost]
        public ActionResult Index(string drplanguage, string txtname)
        {
                  string lang = FillLanguagesList();
            if (ModelState.IsValid)
            {
                ProjectReferenceGroup model = new ProjectReferenceGroup();
                model.GroupName = txtname;
                model.Language = drplanguage;
                model.PageSlug = Utility.SetPagePlug(txtname);
                ViewBag.ProcessMessage = ProjectReferenceGroupManager.AddProjectReferenceGroup(model);
          
                var grouplist = ProjectReferenceGroupManager.GetProjectReferenceGroupList(lang);
               
                return View(grouplist);


            }
            return View();
        }

        public void UpdateRecord(int id, string name)
        {
            string clearname = name.Replace("%47", "\'");
            string pageslug = Utility.SetPagePlug(clearname);
            ProjectReferenceGroupManager.EditProjectReferenceGroup(id, clearname,pageslug);
        }


        public JsonResult GroupEditStatus(int id)
        {
            return Json(ProjectReferenceGroupManager.UpdateGroupStatus(id));
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(ProjectReferenceGroupManager.DeleteGroup(id));
        }


        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ProjectReferenceGroupManager.SortRecords(idsList);
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
