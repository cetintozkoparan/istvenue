using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FEstatesController : Controller
    {
        //
        // GET: /FEstates/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopularEstates()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Estate.Where(d =>  d.Popular == true).ToList();
                return View(list);
            }
        }

        public ActionResult SearchEstates()
        {
            return View();
        }
    }
}
