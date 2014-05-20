using DAL.Context;
using DAL.Entities;
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

        public ActionResult DetailInfo()
        {
            if (RouteData.Values["id"] != null)
            {
                int id = Convert.ToInt32(RouteData.Values["id"]);
                MainContext db = new MainContext();
                var model = db.Brifing.Where(x => x.Id == id).FirstOrDefault();
                return View(model);
            }
            else
            {
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult DetailInfo(Brifing brifing)
        {
            if (RouteData.Values["id"] != null)
            {
                int id = Convert.ToInt32(RouteData.Values["id"]);
                MainContext db = new MainContext();
                var model = db.Brifing.Where(x => x.Id == id).FirstOrDefault();
                model.Il = brifing.Il;
                model.Ilce = brifing.Ilce;

                model.IsinmaTipi = brifing.IsinmaTipi;
                model.Islem = brifing.Islem;
                model.IsTel = brifing.IsTel;
                model.Kati = brifing.Kati;
                model.KatSayisi = brifing.KatSayisi;
                model.Konut = brifing.Konut;
                model.KrediyeUygunluk = brifing.KrediyeUygunluk;
                model.KullanimDurumu = brifing.KullanimDurumu;
                model.Mahalle = brifing.Mahalle;
                model.Mesaj = brifing.Mesaj;
                model.MulkTipi = brifing.MulkTipi;
                model.OdaSayisi = brifing.OdaSayisi;
                model.TapDurumu = brifing.TapDurumu;
                model.Tarih = brifing.Tarih;
                model.Tel = brifing.Tel;
                model.Ticari = brifing.Ticari;
                model.YakiTipi = brifing.YakiTipi;
                model.YapiDurumu = brifing.YapiDurumu;
                model.Yas = brifing.Yas;
                model.Ad = brifing.Ad;
                model.Baska = brifing.Baska;
                model.Boyut = brifing.Boyut;

                model.CepTel = brifing.CepTel;
                model.Depozito = brifing.Depozito;
                model.Detay = brifing.Detay;
                model.Email = brifing.Email;
                model.EvTel = brifing.EvTel;
                model.FiyatAraligi = brifing.FiyatAraligi;
                model.FiyatAraligi2 = brifing.FiyatAraligi2;
                model.GetiriBeklentisi = brifing.GetiriBeklentisi;

                try
                {
                    db.SaveChanges();
                    ViewBag.ProcessMessage = true;
                    return View(model);
                }
                catch (Exception ex)
                {
                    ViewBag.ProcessMessage = false;
                }
            }
            else
            {
                ViewBag.ProcessMessage = false;
                return View();
            }
            return View();

        }



        [AllowAnonymous]
        public JsonResult Delete(int id)
        {
            MainContext db = new MainContext();
            var model = db.Brifing.Where(x => x.Id == id).FirstOrDefault();
            if (model != null)
            {
                db.Brifing.Remove(model);
                db.SaveChanges();
                return Json(true);
                
            }
            else
            {
                return Json(false);
            }
            //if (isdelete)
           
            //  else return false;
        }

    }
}
