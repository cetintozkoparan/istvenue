using BLL.CertificateBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FCertificateController : Controller
    {
        //
        // GET: /FSolutionPartner/

        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            var list = CertificateManager.GetCertificateListForFront(lang);
            return View(list);
        }
    }
}
