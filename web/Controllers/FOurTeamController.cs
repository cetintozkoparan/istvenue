using BLL.InstituionalBL;
using BLL.OurTeamBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Helpers.Enums;
using web.Models;

namespace web.Controllers
{
    public class FOurTeamController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            var list = OurTeamManager.GetOurTeamListForFront(lang);
            var dan = InstituionalManager.GetInstationalByLanguage(lang, (int)EnumInstituionalTypes.Danismanlarimiz); 
            var uzm = InstituionalManager.GetInstationalByLanguage(lang, (int)EnumInstituionalTypes.UzmanlikAlanlarimiz); 
            OurTeamWrapperModel m = new OurTeamWrapperModel(list, dan, uzm);
            return View(m);
        }
    }
}
