using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.ProjectReferenceBL;
using DAL.Entities;
using web.Areas.Admin.Filters;
using BLL.PhotoBL;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class ProjectReferenceController : Controller
    {
        //
        // GET: /Admin/ProjectReference/

        public ActionResult Index()
        {
            string id = FillLanguagesList();
            
            int groupid = Convert.ToInt32(id);

            var list = ProjectReferenceManager.GetProjectReferenceList(groupid);
            return View(list);
        }

        public ActionResult AddProjectReference()
        {
            ImageHelperNew.DestroyImageCashAndSession(166, 135);
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            var groups = ProjectReferenceGroupManager.GetProjectReferenceGroupList(lang);
            var grouplist = new SelectList(groups, "ProjectReferenceGroupId", "GroupName");
            ViewBag.GroupList = grouplist;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProjectReference(IEnumerable<HttpPostedFileBase> attachments,ProjectReferences newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            var groups = ProjectReferenceGroupManager.GetProjectReferenceGroupList(newmodel.Language);
            var grouplist = new SelectList(groups, "ProjectReferenceGroupId", "GroupName", newmodel.ProjectReferenceGroupId);
            ViewBag.GroupList = grouplist;
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadfile.SaveAs(Server.MapPath("/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName)));
                    newmodel.ProjectReferenceFile = "/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }

                if (Session["ModifiedImageId"] != null)
                {
                    newmodel.Logo = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
                else
                {
                    newmodel.Logo = "/Content/images/front/noimage.jpeg";
                }
                newmodel.PageSlug = Utility.SetPagePlug(newmodel.Name);
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = ProjectReferenceManager.AddProjectReference(newmodel);
                Session.Remove("UploadType");
                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/ProjectReferences/", Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName));
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.ProjectReference;
                        p.ItemId = newmodel.ProjectReferenceId;
                        p.Path = "/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Thumbnail = "/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = lang;
                        p.TimeCreated = DateTime.Now;
                        p.Title = "Referans";
                        PhotoManager.Save(p);
                    }
                }

                ModelState.Clear();
                
                return View();
            }
            else
                return View();
        }

        public PartialViewResult Photos(int categoryID, int itemId)
        {
            List<Photo> p = PhotoManager.GetList(categoryID, itemId);
            return PartialView("_photos", p);
        }

        public void DeletePhoto(int id)
        {
            PhotoManager.Delete(id);
        }

        public ActionResult EditProjectReference()
        {
            ImageHelperNew.DestroyImageCashAndSession(166, 135);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    ProjectReferences editrecord = ProjectReferenceManager.GetProjectReferenceById(nid);
                    
                    var pjgroups = ProjectReferenceGroupManager.GetProjectReferenceGroupList(editrecord.Language);
                    var grouplist = new SelectList(pjgroups, "ProjectReferenceGroupId", "GroupName", editrecord.ProjectReferenceGroupId);
                    ViewBag.GroupList = grouplist;
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
        public ActionResult EditProjectReference(IEnumerable<HttpPostedFileBase> attachments, ProjectReferences newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadfile.SaveAs(Server.MapPath("/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName)));
                    newmodel.ProjectReferenceFile = "/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }

              
                if (Session["ModifiedImageId"] != null)
                {
                    newmodel.Logo = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }

                newmodel.PageSlug = Utility.SetPagePlug(newmodel.Name);

                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        newmodel.ProjectReferenceId = nid;
                        ViewBag.ProcessMessage = ProjectReferenceManager.EditProjectReference(newmodel);
                        Session.Remove("UploadType");
                        var pjgroups = ProjectReferenceGroupManager.GetProjectReferenceGroupList(newmodel.Language);
                        var grouplist = new SelectList(pjgroups, "ProjectReferenceGroupId", "GroupName", newmodel.ProjectReferenceGroupId);
                        ViewBag.GroupList = grouplist;
                            foreach (var item in attachments)
                            {
                                if (item != null && item.ContentLength > 0)
                                {
                                    Random random = new Random();
                                    int rand = random.Next(1000, 99999999);
                                    new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/ProjectReferences/", Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName));
                                    Photo p = new Photo();
                                    p.CategoryId = (int)PhotoType.ProjectReference;
                                    p.ItemId = newmodel.ProjectReferenceId;
                                    p.Path = "/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                                    p.Thumbnail = "/Content/images/ProjectReferences/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                                    p.Online = true;
                                    p.SortOrder = 9999;
                                    p.Language = lang;
                                    p.TimeCreated = DateTime.Now;
                                    p.Title = "Referans";
                                    PhotoManager.Save(p);
                                }
                            }
                       
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
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            
            var prgroups = ProjectReferenceGroupManager.GetProjectReferenceGroupList(lang);
            var grouplist = new SelectList(prgroups, "ProjectReferenceGroupId", "GroupName");
            ViewBag.GroupList = grouplist;
            
            string id;

            if (RouteData.Values["id"] == null)
            {
                if (prgroups != null && prgroups.Count != 0)
                    id = prgroups.First().ProjectReferenceGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();
            return id;
        }


        public JsonResult EditStatus(int id)
        {
            bool isupdate = ProjectReferenceManager.UpdateStatus(id);
            return Json(isupdate);
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = ProjectReferenceGroupManager.GetProjectReferenceGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "ProjectReferenceGroupId", "GroupName"));
            return result;
        }

        public JsonResult Delete(int id)
        {
            bool isdelete = ProjectReferenceManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ProjectReferenceManager.SortRecords(idsList);
            return Json(issorted);
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
    }
}
