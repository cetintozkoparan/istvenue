using BLL.MailBL;
using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FBrifingController : Controller
    {
        //
        // GET: /FBrifing/
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        public ActionResult New()
        {
            MainContext db = new MainContext();

            Tags stag = db.Tags.Where(x => x.PageId == 16 && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }

            ViewBag.Lang = lang;
            return View();
        }

        [HttpPost]
        public ActionResult New(Brifing model)
        {
            try
            {
                MainContext db = new MainContext();

                Tags stag = db.Tags.Where(x => x.PageId == 16 && x.Lang == lang).FirstOrDefault();

                if (stag != null)
                {
                    ViewBag.Title = stag.Title;
                    ViewBag.Description = stag.Description;
                    ViewBag.Keywords = stag.Keyword;
                }

                model.BrifingTip = 0;
                db.Brifing.Add(model);
                db.SaveChanges();

                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(2);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = mset.Security;//burası düzeltilecek
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    var mail = new MailMessage();
                    mail.From = new MailAddress(mset.ServerMail);
                    foreach (var item in msend)
                        mail.To.Add(item.MailAddress);
                    if (lang == "tr")
                    {

                        mail.Subject = "Mesaj Bırakın, Geri Dönelim";
                        mail.IsBodyHtml = true;
                        mail.Body = "<p>Mesaj No:" + model.Id + "</p>" +
                            "<p>Adı:" + model.Ad + "</p>" +
                            "<p>E-Mail:" + model.Email + "</p>" +
                            "<p>Mesaj:" + model.Mesaj + "</p>";
                    }
                    else
                    {
                        mail.Subject = "Leave your Brief, We Will Call You";
                        mail.IsBodyHtml = true;
                        mail.Body = "<p>Message Number:" + model.Id + "</p>" +
                            "<p>Name:" + model.Ad + "</p>" +
                            "<p>E-Mail:" + model.Email + "</p>" +
                            "<p>Message:" + model.Mesaj + "</p>";
                    }
                  
                    if (mail.To.Count > 0) client.Send(mail);
                }
                TempData["sent"] = "true";



                ViewBag.Result = true;
            }
            catch (Exception ex)
            {
                ViewBag.Result = false;
            }

            ViewBag.Lang = lang;
            return View(new Brifing());
        }

        public ActionResult NewPartial()
        {
            ViewBag.Lang = lang;
            ViewBag.BriefResult = "";
            return PartialView();
        }

        [HttpPost]
        public ActionResult NewPartial(Brifing model)
        {
            try
            {
                MainContext db = new MainContext();
                model.BrifingTip = 0;
                db.Brifing.Add(model);
                db.SaveChanges();

                ViewBag.BriefResult = true;
            }
            catch (Exception ex)
            {
                ViewBag.BriefResult = false;
            }

            ViewBag.Lang = lang;
            return PartialView();
        }

        public ActionResult Detail()
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == 17 && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Detail(Brifing model, FormCollection collection)
        {
            try
            {
               
                MainContext db = new MainContext();
                Tags stag = db.Tags.Where(x => x.PageId == 17 && x.Lang == lang).FirstOrDefault();

                if (stag != null)
                {
                    ViewBag.Title = stag.Title;
                    ViewBag.Description = stag.Description;
                    ViewBag.Keywords = stag.Keyword;
                }
                var strArray = collection["gayrimenkul"];
                model.Islem = strArray;
                model.BrifingTip = 1;
                //model.
                db.Brifing.Add(model);
                db.SaveChanges();



                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(2);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = mset.Security;//burası düzeltilecek
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    var mail = new MailMessage();
                    mail.From = new MailAddress(mset.ServerMail);
                    foreach (var item in msend)
                        mail.To.Add(item.MailAddress);
                    if (lang == "tr")
                    {

                        mail.Subject = "Mesaj Bırakın, Geri Dönelim:Detaylı";
                        mail.IsBodyHtml = true;
                        mail.Body = "<p>Mesaj No:" + model.Id + "</p>" +
                            "<p>Adı:" + model.Ad + "</p>" +
                            "<p>E-Mail:" + model.Email + "</p>" +
                            "<p>Mesaj:" + model.Mesaj + "</p>";
                    }
                    else
                    {
                        mail.Subject = "Leave your Brief, We Will Call You: Detailed";
                        mail.IsBodyHtml = true;
                        mail.Body = "<p>Message Number:" + model.Id + "</p>" +
                            "<p>Name:" + model.Ad + "</p>" +
                            "<p>E-Mail:" + model.Email + "</p>" +
                            "<p>Message:" + model.Mesaj + "</p>";
                    }

                    if (mail.To.Count > 0) client.Send(mail);
                }


                ModelState.Clear();

                ViewBag.Result = true;
            }
            catch (Exception ex)
            {
                ViewBag.Result = false;
            }

            ViewBag.Lang = lang;
            return View();
        }

    }
}
