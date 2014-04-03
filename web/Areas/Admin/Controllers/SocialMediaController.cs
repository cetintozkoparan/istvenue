using BLL.SocialMediaBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Areas.Admin.Controllers
{
    public class SocialMediaController : Controller
    {
        //
        // GET: /Admin/SocialMedia/

        public ActionResult Index()
        {
            var list = SocialMediaManager.GetList();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }


        public ActionResult Edit()
        {
            return View();
        }

    }
}
