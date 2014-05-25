using BLL.InstituionalBL;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Helpers.Enums;

namespace web.Controllers
{
    public class FInstitutionalController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Kurumsal/

        public ActionResult Index()
        {
            SetPageSetting(2);
            var aboutus = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Hakkimizda));
            return View(aboutus);
        }

        public ActionResult WhyUs()
        {
            SetPageSetting(9);
            var aboutus = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.NedenBiz));
            return View(aboutus);
        }

        public ActionResult VisionMision()
        {
            SetPageSetting(3);

            var misyon = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Misyon));
            var vizyon = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Vizyon));
            ViewBag.MisyonContent = misyon.Content;
            ViewBag.VizyonContent = vizyon.Content;
            return View();
        }

        public ActionResult SiteHarita()
        {
            return View();
        }

        public ActionResult Page(int pid)
        {
            var page = PageManager.Get(pid);
            return View(page);
        }

        public void SetPageSetting(int id)
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == id && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }
        }
    }
}
