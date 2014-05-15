using web.Models;
using BLL.ProductBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.ServiceBL;
using BLL.ServiceGroupBL;
using DAL.Entities;

namespace web.Controllers
{
    public class FServiceController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Service/

        public ActionResult Index()
        {
            List<Service> services = new List<Service>();
            services = ServiceManager.GetServiceListForFront(lang);
            var ourservices = ServiceManager.GetOurServices(lang);

            ServiceWrapperModel swm = new ServiceWrapperModel(null, services, null, null, ourservices);
            return View(swm);
        }

        public ActionResult Hizmetlerimiz()
        {
            OurServices services = new OurServices();
            services = ServiceManager.GetOurServices(lang);
            ServiceWrapperModel swm = new ServiceWrapperModel(null, null, null, null, services);
            return View(swm);
        }
    }
}
