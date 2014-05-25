using System.Linq;
using System.Web.Mvc;
using web.Models;
using BLL.NewsBL;
using BLL.ReferenceBL;
using BLL.Project;
using BLL.DocumentsBL;
using BLL.PhotoBL;
using BLL.SectorGroupBL;
using BLL.ProductBL;
using BLL.ServiceGroupBL;
using BLL.ContactBL;
using System.Collections.Generic;
using DAL.Context;
using DAL.Entities;

namespace web.Controllers
{
    public class slider
    {
        public string image { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumb { get; set; }
    }

    public class FHomeController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        
        public ActionResult Index()
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == 1).FirstOrDefault();
           
            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }
            //var news = NewsManager.GetNewsListForFront(lang).Take(4);
            //var references = ReferenceManager.GetReferenceListForFront(lang);
            //var docgroup = DocumentManager.GetDocumentGroupListForFront(lang).First();
            //var docs = DocumentManager.GetDocumentListForFront(docgroup.DocumentGroupId).Take(5);
            //var projects = ProjectManager.GetProjectListForFront(lang).Take(4);
            //var photos = PhotoManager.GetListForFront(lang, 0);
            //var sectors = SectorGroupManager.GetSectorGroupListForFront(lang);
            //var services = ServiceGroupManager.GetServiceGroupListForFront(0, lang);
            //var prodgroups = ProductManager.GetProductGroupListForFront(lang);
            //var contact = ContactManager.GetContact(lang);
            //ViewBag.docgroupSlug = docgroup.PageSlug;
            //ViewBag.docgroupid = docgroup.DocumentGroupId;
            //ViewBag.docgroup = docgroup.GroupName;
            //HomePageWrapperModel modelbind = new HomePageWrapperModel(contact,services, prodgroups, sectors, news, references, projects, docs, photos);
            //return View(modelbind);
            return View();
        }

        public JsonResult GetImages()
        {
            var photos = PhotoManager.GetListForFront(lang, 0);
            
            var slider = new List<slider>();    

            foreach (var item in photos)
            {
                slider s = new slider();
                s.image = item.Path;
                s.title = item.Title;
                s.url = item.Link;
                s.thumb = "";
                slider.Add(s);
            }

            return Json(slider
                ,
                JsonRequestBehavior.AllowGet
                );
        }
        public ActionResult ChangeCulture(string lang,string returnUrl)
        {
            Session["culture"] = lang;
            if(lang=="en")
                return Redirect("/en/homepage");
            return Redirect("/tr/anasayfa");
        }
    }
}
