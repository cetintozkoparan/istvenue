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
            using (MainContext db = new MainContext())
            {
                var list = db.Estate.ToList();
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult SearchEstates(SearchEstateModel model)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Estate.ToList();
                if (!string.IsNullOrEmpty(model.referansno))
                {
                    int refno = Convert.ToInt32(model.referansno);
                    list = list.Where(d=>d.ReferenceNo == refno).ToList();
                }
                if (!string.IsNullOrEmpty(model.keyword))
                {
                    list = list.Where(d => d.Header.Contains(model.keyword)).ToList();                    
                }
                if (model.emlaktipi != 0)
                {
                    list = list.Where(d => d.TypeId == model.emlaktipi).ToList();                    
                }

                if (model.sehir != 0)
                {
                    list = list.Where(d => d.CountryId == model.sehir).ToList();
                }

                if (model.ilce != 0)
                {
                    list = list.Where(d => d.TownId == model.ilce).ToList();
                }

                if (model.semt != 0)
                {
                    list = list.Where(d => d.DistrictId == model.semt).ToList();
                }

                //if (!string.IsNullOrEmpty(model.fiyataraligialt))
                //{
                //    list = list.Where(d => d.Price >= model.fiyataraligialt).ToList();
                //}

                if (!string.IsNullOrEmpty(model.metrekarealt))
                {
                    int metrekare = Convert.ToInt32(model.metrekarealt);
                    list = list.Where(d => d.RoomNumber > metrekare).ToList();
                }

                if (!string.IsNullOrEmpty(model.metrekareust))
                {
                    int metrekare = Convert.ToInt32(model.metrekareust);
                    list = list.Where(d => d.RoomNumber < metrekare).ToList();
                }

                if (!string.IsNullOrEmpty(model.odasayisialt))
                {
                    int roomcount = Convert.ToInt32(model.odasayisialt);
                    list = list.Where(d => d.RoomNumber > roomcount).ToList();
                }

                if (!string.IsNullOrEmpty(model.odasayisiust))
                {
                    int roomcount = Convert.ToInt32(model.odasayisiust);
                    list = list.Where(d => d.RoomNumber < roomcount).ToList();
                }

                if (!string.IsNullOrEmpty(model.binayasialt))
                {
                    int binayasialt = Convert.ToInt32(model.binayasialt);
                    list = list.Where(d => d.Age > binayasialt).ToList();
                }

                if (!string.IsNullOrEmpty(model.binayasiust))
                {
                    int binayasiust = Convert.ToInt32(model.binayasiust);
                    list = list.Where(d => d.Age < binayasiust).ToList();
                }

                return View(list);
            }
        }
    }

    public class SearchEstateModel
    {
        public string referansno { get; set; }
        public string keyword { get; set; }
        public int islemtipi { get; set; }
        public int emlaktipi { get; set; }
        public int sehir { get; set; }
        public int ilce { get; set; }
        public int semt { get; set; }
        public string fiyataraligialt { get; set; }
        public string fiyataraligiust { get; set; }
        public string parabirimi { get; set; }
        public string metrekarealt { get; set; }
        public string metrekareust { get; set; }
        public string odasayisialt { get; set; }
        public string odasayisiust { get; set; }
        public string binayasialt { get; set; }
        public string binayasiust { get; set; }
        public string emlakdanismani { get; set; }

    }

}
