using BLL.InstituionalBL;
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

            var aboutus = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Hakkimizda));
            return View(aboutus);
        }

        public ActionResult VisionMision()
        {
            var visionmision = InstituionalManager.GetInstationalByLanguage(lang, Convert.ToInt32(EnumInstituionalTypes.Misyon));
            return View(visionmision);
        }

        public ActionResult Page(int pid)
        {
            var page = PageManager.Get(pid);
            return View(page);
        }
    }
}
