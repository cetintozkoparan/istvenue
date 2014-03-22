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
        public ActionResult SendCV(HttpPostedFileBase attachedfile, string Pozisyon)
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
                    mail.Body = "<h5><b>Başvurulan Pozisyon: </b>" + Pozisyon ;
                    if (attachedfile != null && attachedfile.ContentLength > 0)
                    {
                        var attachment = new Attachment(attachedfile.InputStream, attachedfile.FileName);
                        mail.Attachments.Add(attachment);
                    }
                    if (mail.To.Count > 0) client.Send(mail);
                }
                TempData["sent"] = "true";
                return RedirectToAction("Positions");
            }
            catch (Exception ex)
            {
                TempData["sent"] = "false";
            }

            return RedirectToAction("Positions");
        }

        [HttpPost]
        public ActionResult ApplicationForm(string namesurname, string departman, string notlar, HttpPostedFileBase attachedfile)
        {
            try
            {
                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(0);

                using (var client = new SmtpClient("mail.web.com.tr", 587))
                {
                    client.EnableSsl = mset.Security;
                    client.Credentials = new NetworkCredential("info@web.com.tr", "Deneysan2013");
                    var mail = new MailMessage();
                    mail.From = new MailAddress("info@web.com.tr");
                    mail.To.Add("info@web.com.tr");
                    mail.IsBodyHtml = true;
                    mail.Subject = "İş Başvurusu";

                    mail.Body = "<center><table><tr><td colspan='2'><h2><center>İŞ BAŞVURUSU</center></h2></td></tr>";
                    mail.Body += "<tr><td style='border: 1px solid #ddd; padding:3px;' width='300'><center><b>Adı Soyadı</b></center></td>";
                    mail.Body += " <td style='border: 1px solid #ddd; padding:3px;' width='300'><center>";
                    mail.Body += namesurname + "</center></td></tr><tr><td style='border: 1px solid #ddd; padding:3px;' width='300'><center><b>Başvurduğu Pozisyon</b></center></td>";
                    mail.Body += "<td style='border: 1px solid #ddd; padding:3px;' width='300'><center>" + departman + "</center></td></tr>";
                    mail.Body += "<tr><td style='border: 1px solid #ddd; padding:3px;' width='300'><center><b>Not</b></center></td>";
                    mail.Body += "<td style='border: 1px solid #ddd; padding:3px;' width='300'><center>";
                    mail.Body += notlar + "</center></td></tr></table></center>";
              
                    if (attachedfile != null && attachedfile.ContentLength > 0)
                    {
                        var attachment = new Attachment(attachedfile.InputStream, attachedfile.FileName);
                        mail.Attachments.Add(attachment);
                    }
                    client.Send(mail);
                }
                TempData["sent"] = "true";
                return RedirectToAction("ApplicationForm");
            }
            catch (Exception ex)
            {
                TempData["sent"] = "false";
            }

            return View();
        }

    }
}
