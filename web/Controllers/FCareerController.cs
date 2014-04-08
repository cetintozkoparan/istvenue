using BLL.HRBL;
using BLL.MailBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace web.Controllers
{
    public class Formvalues
    {
        public string pozisyon { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }

        public string dogumyeri { get; set; }
        public string dogumgun { get; set; }
        public string dogumay { get; set; }
        public string dogumyil { get; set; }
        public string cinsiyet { get; set; }
        public string tcno { get; set; }
        public string askerlikdurumu { get; set; }
        public string medenidurumu { get; set; }
        public string ehliyet { get; set; }
        public string ehliyetyili { get; set; }
        public string ehliyettipi { get; set; }

        public string adres { get; set; }
        public string eposta { get; set; }
        public string evtel { get; set; }
        public string ceptel { get; set; }
        public string digertel { get; set; }
        public string egitimseviye { get; set; }
        public string okul { get; set; }
        public string bolum { get; set; }
        public string yil { get; set; }

        public string ingilizceseviye { get; set; }
        public string ilgilizceyer { get; set; }
        public string almancaseviye { get; set; }
        public string almancayer { get; set; }
        public string fransizcaseviye { get; set; }
        public string fransizcayer { get; set; }
        public string sirket { get; set; }
        public string gorev { get; set; }
        public string isbaslangictarihi { get; set; }
        public string isbitistarihi { get; set; }
        public string ayrilmanedeni { get; set; }
        public string referansadi { get; set; }
        public string referanskurum { get; set; }
        public string referanstel { get; set; }
        public string ilavebilgi { get; set; }
    }

    public class FCareerController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        
        //
        // GET: /Kariyer/

        public ActionResult Index()
        {
            var content = HumanResourceManager.GetHRByLanguage(lang);
            return View(content);
        }

        public ActionResult Positions()
        {
            var content = HumanResourceManager.GetHumanResourcePositionListForFront(lang);
            return View(content);
        }

        [HttpGet]
        public ActionResult ApplicationForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendCV(HttpPostedFileBase attachedfile)
        {
            try
            {
                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(0);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = mset.Security;//burası düzeltilecek
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    var mail = new MailMessage();
                    mail.From = new MailAddress(mset.ServerMail);
                    foreach (var item in msend)
                        mail.To.Add(item.MailAddress);
                    mail.Subject = "Yeni CV İletisi";
                    mail.IsBodyHtml = true;
                    mail.Body = "Yeni bir iş başvurusu bulunmaktadır. Gönderen kişinin cv'si ektedir.";
                    if (attachedfile != null && attachedfile.ContentLength > 0)
                    {
                        var attachment = new Attachment(attachedfile.InputStream, attachedfile.FileName);
                        mail.Attachments.Add(attachment);
                    }
                    if (mail.To.Count > 0) client.Send(mail);
                }
                TempData["sent"] = "true";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["sent"] = "false";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ApplicationForm(Formvalues form)
        {
            try
            {
                string html = System.IO.File.ReadAllText(Server.MapPath("~/HTMLTemplate/CareerTemplate.html"));
                html = html.Replace("[pozisyon]", form.pozisyon);
                html = html.Replace("[ad]", form.ad);
                html = html.Replace("[soyad]", form.soyad);
                html = html.Replace("[dogumyeri]", form.dogumyeri);
                html = html.Replace("[dogumgun]", form.dogumgun);
                html = html.Replace("[dogumay]", form.dogumay);
                html = html.Replace("[dogumyil]", form.dogumyil);
                
                html = html.Replace("[cinsiyet]", form.cinsiyet);
                html = html.Replace("[tcno]", form.tcno);

                html = html.Replace("[medenidurumu]", form.medenidurumu);
                html = html.Replace("[askerlikdurumu]", form.askerlikdurumu);
                html = html.Replace("[ehliyettipi]", form.ehliyettipi);
                html = html.Replace("[ehliyetyili]", form.ehliyetyili);
                html = html.Replace("[adres]", form.adres);
                html = html.Replace("[eposta]", form.eposta);
                html = html.Replace("[evtel]", form.evtel);
                html = html.Replace("[ceptel]", form.ceptel);
                html = html.Replace("[digertel]", form.digertel);
                html = html.Replace("[egitimseviye]", form.egitimseviye);
                html = html.Replace("[okul]", form.okul);
                html = html.Replace("[bolum]", form.bolum);

                html = html.Replace("[yil]", form.bolum);
                html = html.Replace("[ingilizceseviye]", form.ingilizceseviye);
                html = html.Replace("[ingilizceyer]", form.ilgilizceyer);
                html = html.Replace("[almancaseviye]", form.almancaseviye);
                html = html.Replace("[almancayer]", form.almancayer);
                html = html.Replace("[fransizcaseviye]", form.fransizcaseviye);
                html = html.Replace("[fransizcayer]", form.fransizcayer);
                html = html.Replace("[sirket]", form.sirket);

                html = html.Replace("[gorev]", form.gorev);
                html = html.Replace("[isbaslangictarihi]", form.isbaslangictarihi);
                html = html.Replace("[isbitistarihi]", form.isbitistarihi);
                html = html.Replace("[ayrilmanedeni]", form.ayrilmanedeni);
                html = html.Replace("[referansadi]", form.referansadi);
                html = html.Replace("[referanskurum]", form.referanskurum);
                html = html.Replace("[referanstel]", form.referanstel);
                html = html.Replace("[ilavebilgi]", form.ilavebilgi);

                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(0);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = mset.Security;
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    var mail = new MailMessage();
                    mail.From = new MailAddress(mset.ServerMail);
                    foreach (var item in msend)
                        mail.To.Add(item.MailAddress);
                    mail.Subject = "Yeni İş Başvurusu";
                    mail.IsBodyHtml = true;
                    mail.Body = html;

                    if (mail.To.Count > 0) client.Send(mail);
                }
                TempData["sent"] = "true";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["sent"] = "false";
            }

            return RedirectToAction("Index");
        }

    }
}
