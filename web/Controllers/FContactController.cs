using BLL.ContactBL;
using BLL.BankBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using BLL.MailBL;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using DAL.Entities;
using web.Models;
using DAL.Context;

namespace web.Controllers
{
    public class FContactController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Iletisim/
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);
                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        public ActionResult Index()
        {
            MainContext db = new MainContext();
            Tags stag = db.Tags.Where(x => x.PageId == 12 && x.Lang == lang).FirstOrDefault();

            if (stag != null)
            {
                ViewBag.Title = stag.Title;
                ViewBag.Description = stag.Description;
                ViewBag.Keywords = stag.Keyword;
            }

            var contact = ContactManager.GetContact(lang);
            SubscribeModel obj = new SubscribeModel();
            ContactWrapperModel m = new ContactWrapperModel(contact, obj);
            return View(m);
        }

        public ActionResult Bank()
        {
            var banks = BankManager.GetBankInfoListForFront(lang);
            return View(banks);
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(string kepce,string namesurname, string email, string subject, string body)
        {
            try
            {
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != kepce)
                {
                    TempData["captchaError"] = "Yanlış değer girdiniz, lütfen tekrar deneyiniz.";
                    return RedirectToAction("Index");
                }
                if (namesurname == String.Empty || email == String.Empty || subject == String.Empty || body == String.Empty)
                {
                    TempData["required"] = "true";
                    return RedirectToAction("Index");
                }

                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(0);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = true;//burası düzeltilecek
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    
                    var mail = new MailMessage();
                    mail.From = new MailAddress(mset.ServerMail);
                    foreach (var item in msend)
                        mail.To.Add(item.MailAddress);
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    mail.Body = "<h5><b>" + namesurname + " - " + email + "</b></h5>" + "<p>" + body + "</p>";

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
