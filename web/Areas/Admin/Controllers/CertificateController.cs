using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Filters;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using DAL.Entities;
using BLL.CertificateBL;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class CertificateController : Controller
    {
        //
        // GET: /Admin/Certificate/

        public ActionResult Index()
        {
            string sellang=FillLanguagesList();

            var referncelist = CertificateManager.GetCertificateList(sellang);
            return View(referncelist);
        }

        public ActionResult AddCertificate()
        {
            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            ViewBag.LanguageList = list;
           
            return View();
        }

        [HttpPost]
        public ActionResult AddCertificate(Certificate newmodel, HttpPostedFileBase uploadfile)
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
                    new ImageHelper(240, 240).SaveThumbnail(uploadfile, "/Content/images/Certificate/", Utility.SetPagePlug(newmodel.CertificateName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    newmodel.Logo = "/Content/images/Certificate/" + Utility.SetPagePlug(newmodel.CertificateName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }
                else
                {
                    newmodel.Logo = "/Content/images/front/noimage.jpeg";
                }
                newmodel.SortOrder = 9999;
                newmodel.TimeCreated = DateTime.Now;
                ViewBag.ProcessMessage = CertificateManager.AddCertificate(newmodel);
                ModelState.Clear();
                // Response.Redirect("/yonetim/haberduzenle/" + newsmodel.NewsId);
                return View();
            }
            else
                return View();
        }

        public ActionResult EditCertificate()
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
                    Certificate editCertificate = CertificateManager.GetCertificateById(nid);
                    return View(editCertificate);
                }
                else
                    return View();
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult EditCertificate(Certificate Certificatemodel, HttpPostedFileBase uploadfile)
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
                    new ImageHelper(240, 240).SaveThumbnail(uploadfile, "/Content/images/Certificate/", Utility.SetPagePlug(Certificatemodel.CertificateName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
                    Certificatemodel.Logo = "/Content/images/Certificate/" + Utility.SetPagePlug(Certificatemodel.CertificateName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
                }


                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        Certificatemodel.CertificateId = nid;
                        ViewBag.ProcessMessage = CertificateManager.EditCertificate(Certificatemodel);
                        return View(Certificatemodel);
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(Certificatemodel);
                    }
                }
                else  return View();
            }
            else
                return View();

            return View();
        }


        public JsonResult CertificateEditStatus(int id)
        {
            string NowState;
            bool isupdate = CertificateManager.UpdateStatus(id);
            return Json(isupdate);
        }

        
        public JsonResult DeleteCertificate(int id)
        {
            bool isdelete = CertificateManager.Delete(id);
            //if (isdelete)
            return Json(isdelete);
          //  else return false;
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

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = CertificateManager.SortRecords(idsList);
            return Json(issorted);


        }

        public class JsonList
        {
            public string[] list { get; set; }
        }
       

    }
}
