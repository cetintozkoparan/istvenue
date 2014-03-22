using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myBLOGData;
using System.Data;
using System.Web.Security;
using System.Web;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using log4net;
namespace BLL.AccountBL
{
    public class AccountManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static bool Login(string email, string password)
        {
            using(MainContext db=new MainContext())
            {
                AdminUser record = db.AdminUser.SingleOrDefault(d => d.Email == email && d.Password == password);
                if (record != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, record.FullName, DateTime.Now, DateTime.Now.AddMinutes(120), false, "Admin", FormsAuthentication.FormsCookiePath);
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                    HttpContext.Current.Response.Cookies.Add(cookie);
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Login.ToString();
                    logkeeper.Message = LogMessages.Logined;
                    logkeeper.User = record.FullName;
                    logkeeper.Data = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    logkeeper.AddInfoLog(logger);
                    return true;
                }
                else
                {
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Login.ToString();
                    logkeeper.Message = LogMessages.NotLogined;
                    logkeeper.User = email;
                    logkeeper.Data = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    logkeeper.AddInfoLog(logger);
                    return false;
                }
            }
        }
    }
}
