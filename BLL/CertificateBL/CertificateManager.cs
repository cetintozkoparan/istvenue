using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.LogBL;
using DAL.Context;
using DAL.Entities;
using log4net;

namespace BLL.CertificateBL
{
    public class CertificateManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<Certificate> GetCertificateList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Certificate.Where(d => d.Deleted == false && d.Language == language).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static List<Certificate> GetCertificateListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Certificate.Where(d => d.Deleted == false && d.Language == language && d.Online==true).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddCertificate(Certificate record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    if (!record.TimeCreated.HasValue)
                        record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    db.Certificate.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.CertificateAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.CertificateName;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public static bool UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Certificate.SingleOrDefault(d => d.CertificateId == id);
                try
                {
                    
                    if (list != null)
                    {
                        list.Online = list.Online == true ? false : true;
                        db.SaveChanges();
                                           
                    }
                     return list.Online;
            
                }
                catch (Exception)
                {
                    return list.Online;
                }
            }
        }


        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Certificate.FirstOrDefault(d => d.CertificateId == id);
                    record.Deleted = true;
                    
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.CertificateDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.CertificateName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Certificate GetCertificateById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Certificate record = db.Certificate.Where(d => d.CertificateId == nid).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool EditCertificate(Certificate Certificatemodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Certificate record = db.Certificate.Where(d => d.CertificateId == Certificatemodel.CertificateId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = Certificatemodel.Content;
                        record.Link = Certificatemodel.Link;
                        record.CertificateName = Certificatemodel.CertificateName;
                        record.Language = Certificatemodel.Language;
                        if (!string.IsNullOrEmpty(Certificatemodel.Logo))
                        {
                            record.Logo = Certificatemodel.Logo;
                        }
                        record.Content = Certificatemodel.Content;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Referans.ToString();
                        logkeeper.Message = LogMessages.CertificateEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.CertificateName;
                        logkeeper.AddInfoLog(logger);

                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }




        public static bool SortRecords(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Certificate sortingrecord = db.Certificate.SingleOrDefault(d => d.CertificateId == mid);
                        sortingrecord.SortOrder = Convert.ToInt32(row);
                        db.SaveChanges();
                        row++;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
