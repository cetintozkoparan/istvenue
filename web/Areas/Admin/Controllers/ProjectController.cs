using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.Project;
using DAL.Entities;
using web.Areas.Admin.Filters;
using BLL.PhotoBL;

namespace web.Areas.Admin.Controllers
{
      [AuthenticateUser]
    public class ProjectController : Controller
    {
        //
        // GET: /Admin/Projects/

        public ActionResult Index()
        {
            string sellang = FillLanguagesList();

            var list = ProjectManager.GetProjectList(sellang);
            return View(list);
        }

        public ActionResult AddProject()
        {
            ImageHelperNew.DestroyImageCashAndSession(235, 200);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;

            return View();
        }
        
        public void UpdatePhotoPath(int photoId, string newpath)
        {
            PhotoManager.UpdatePhotoPath(photoId, newpath);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProject(IEnumerable<HttpPostedFileBase> attachments, Projects newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
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
                    uploadfile.SaveAs(Server.MapPath("/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName)));
                    newmodel.ProjectFile = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }

                //if (uploadimage != null && uploadimage.ContentLength > 0)
                //{
                //    using (System.Drawing.Image image = System.Drawing.Image.FromStream(uploadimage.InputStream, true, true))
                //    {
                //        if (image.Width == 235 && image.Height == 200)
                //        {
                //            Random random = new Random();
                //            int rand = random.Next(1000, 99999999);
                //            uploadimage.SaveAs(Server.MapPath("/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadimage.FileName)));
                //            newmodel.Logo = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadimage.FileName);
                //        }
                //        else
                //        {
                //            TempData["ImageSizeError"] = "Eklemiş olduğunuz resmin boyutları 235x200 olmalıdır...";
                //            return View();
                //        }
                //    }
                //}

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
                ViewBag.ProcessMessage = ProjectManager.AddProject(newmodel);
                Session.Remove("UploadType");
                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/projects/", Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName));
                        Photo p = new Photo();
                        p.CategoryId = 2;
                        p.ItemId = newmodel.ProjectId;
                        p.Path = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Thumbnail = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = lang;
                        p.TimeCreated = DateTime.Now;
                        p.Title = "Proje";
                        PhotoManager.Save(p);
                    }
                }
                ModelState.Clear();

                return View();
            }
            else
                return View();
        }


        public ActionResult EditProject()
        {
            ImageHelperNew.DestroyImageCashAndSession(235, 200);
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Projects editrecord = ProjectManager.GetProjectById(nid);
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
        public ActionResult EditProject(IEnumerable<HttpPostedFileBase> attachments, Projects newmodel, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadimage)
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();
            ViewBag.LanguageList = list;

            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadfile.SaveAs(Server.MapPath("/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName)));
                    newmodel.ProjectFile = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }

                //if (uploadimage != null && uploadimage.ContentLength > 0)
                //{
                //    using (System.Drawing.Image image = System.Drawing.Image.FromStream(uploadimage.InputStream, true, true))
                //    {
                //        if (image.Width == 235 && image.Height == 200)
                //        {
                //            Random random = new Random();
                //            int rand = random.Next(1000, 99999999);
                //            uploadimage.SaveAs(Server.MapPath("/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadimage.FileName)));
                //            newmodel.Logo = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(uploadimage.FileName);
                //        }
                //        else
                //        {
                //            TempData["ImageSizeError"] = "Eklemiş olduğunuz resmin boyutları 235x200 olmalıdır...";
                //            return RedirectToAction("EditProject", "Project", new { id = RouteData.Values["id"] });
                //        }
                //    }
                //}
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
                        newmodel.ProjectId = nid;
                        ViewBag.ProcessMessage = ProjectManager.EditProject(newmodel);
                        Session.Remove("UploadType");

                        foreach (var item in attachments)
                        {
                            if (item != null && item.ContentLength > 0)
                            {
                                Random random = new Random();
                                int rand = random.Next(1000, 99999999);
                                new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/projects/", Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName));
                                Photo p = new Photo();
                                p.CategoryId = 2;
                                p.ItemId = newmodel.ProjectId;
                                p.Path = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                                p.Thumbnail = "/Content/images/projects/" + Utility.SetPagePlug(newmodel.Name) + "_" + rand + Path.GetExtension(item.FileName);
                                p.Online = true;
                                p.SortOrder = 9999;
                                p.Language = lang;
                                p.TimeCreated = DateTime.Now;
                                p.Title = "Proje";
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
            return lang;
        }

        public PartialViewResult Photos(int categoryID, int itemId)
        {
            List<Photo> p = PhotoManager.GetList(categoryID, itemId);
            return PartialView("_photos", p);
        }

        public JsonResult EditStatus(int id)
        {
            string NowState;
            bool isupdate = ProjectManager.UpdateStatus(id);
            return Json(isupdate);
        }


        public JsonResult Delete(int id)
        {
            bool isdelete = ProjectManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
            //  else return false;
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ProjectManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
    }
}
