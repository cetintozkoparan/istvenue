using System.Linq;
using BLL.Project;
using System.Web.Mvc;
using web.Models;
using BLL.PhotoBL;
using DAL.Entities;
using System.Collections.Generic;

namespace web.Controllers
{
    public class FProjectsController : Controller
    {
        //
        // GET: /FProjects/

        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectContent(int id)
        {
            var plist = ProjectManager.GetProjectListForFront(lang);
            var p = ProjectManager.GetProjectById(id);
            var photos = PhotoManager.GetList(2, id);
            ProjectWrapperModel m = new ProjectWrapperModel(photos,plist, p);
            return View(m);
        }

       
    }
}
