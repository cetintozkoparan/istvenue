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
            Service service = new Service();
            OurServices ourservices = new OurServices();
            ServiceGroup servicegrp = new ServiceGroup();
            int grupID = 0;
            if (RouteData.Values["sid"] != null)
            {
                service = ServiceManager.GetServiceById(Convert.ToInt32(RouteData.Values["sid"].ToString()));
                ViewBag.grpname = ServiceGroupManager.GetServiceGroupById(service.ServiceGroupId).GroupName;
                grupID = service.ServiceGroupId;
            }else if (RouteData.Values["gid"] != null)
            {
                servicegrp = ServiceGroupManager.GetServiceGroupById(Convert.ToInt32(RouteData.Values["gid"].ToString()));
                grupID = Convert.ToInt32(RouteData.Values["gid"].ToString());
            }
            else
            {
                ourservices = ServiceManager.GetOurServices(lang);
            }
            
            var service_group_list = ServiceGroupManager.GetServiceGroupListForFront(grupID, lang);
            
            ServiceWrapperModel swm = new ServiceWrapperModel(servicegrp ,new List<Service>(), service_group_list,service,ourservices);
            return View(swm);
        }

        public ActionResult subservices(int id)
        {
            var sl = ServiceManager.GetServiceList(id);
            var sg = ServiceGroupManager.GetServiceGroupById(id);
            ServiceSubWrapperModel pswm = new ServiceSubWrapperModel(sl, sg);
            return PartialView("_services", pswm);
        }

        //public ActionResult ProductDetail(int pid)
        //{
        //    var product_group_list = ProductManager.GetProductGroupListForFront(lang);
            
        //    var product = ProductManager.GetProductById(pid);
        //    var psg = ProductManager.GetProductSubGroupById(product.ProductSubGroupId);
        //    var pg = ProductManager.GetProductGroupById(product.ProductGroupId);

        //    ViewBag.grpid = pg.ProductGroupId;
        //    ViewBag.sgid = psg.ProductSubGroupId;
        //    ViewBag.subgrpname = psg.GroupName;
        //    ViewBag.grpname = pg.GroupName;

        //    ProductDetailWrapperModel model = new ProductDetailWrapperModel(product, product_group_list);
            
        //    return View(model);
        //}
    }
}
