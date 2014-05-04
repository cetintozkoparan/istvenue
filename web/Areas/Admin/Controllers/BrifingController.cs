using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Areas.Admin.Controllers
{
    public class BrifingController : Controller
    {
        //
        // GET: /Admin/Brifing/

        public ActionResult Index()
        {
            MainContext db = new MainContext();
            var model = db.Brifing.Where(x=>x.BrifingTip == 0).ToList();
            return View(model);
        }

        public ActionResult Detail()
        {
            MainContext db = new MainContext();
            var model = db.Brifing.Where(x => x.BrifingTip == 1).ToList();
            return View(model);
        }

    }
}
