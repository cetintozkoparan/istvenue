using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using System.Web;
using log4net;

namespace BLL.HRBL
{
    public class HumanResourceManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static HumanResource GetHRByLanguage(string language)
        {
            using (MainContext db = new MainContext())
            {
                HumanResource instional_info = db.HumanResource.SingleOrDefault(d => d.Language == language);
                return instional_info;
            }
        }

        public static bool EditHumanResource(HumanResource record)
        {
            using (MainContext db = new MainContext())
            {
                HumanResource editrecord = db.HumanResource.SingleOrDefault(d =>  d.Language == record.Language);
        
                if (editrecord == null)
                {
                    editrecord = new HumanResource();
                    editrecord.Language = record.Language;
                    editrecord.Content = record.Content;
                    db.HumanResource.Add(editrecord);
                }
                else
                {
                    editrecord.Content = record.Content;
                }

                db.SaveChanges();

                return true;
            }
        }

        public static List<HumanResourcePosition> GetHumanResourcePositionList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.HumanResourcePosition.Where(d => d.Deleted == false && d.Language == language).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static List<HumanResourcePosition> GetHumanResourcePositionListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.HumanResourcePosition.Where(d => d.Deleted == false && d.Language == language && d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddHumanResourcePosition(HumanResourcePosition record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    if (!record.TimeCreated.HasValue)
                        record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    db.HumanResourcePosition.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.InsanKaynaklari.ToString();
                    logkeeper.Message = LogMessages.HumanResourcePositionAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.HumanResourcePositionName;
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
                var list = db.HumanResourcePosition.SingleOrDefault(d => d.HumanResourcePositionId == id);
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
                    var record = db.HumanResourcePosition.FirstOrDefault(d => d.HumanResourcePositionId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.InsanKaynaklari.ToString();
                    logkeeper.Message = LogMessages.HumanResourcePositionDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.HumanResourcePositionName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static HumanResourcePosition GetHumanResourcePositionById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    HumanResourcePosition record = db.HumanResourcePosition.Where(d => d.HumanResourcePositionId == nid).SingleOrDefault();
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

        public static bool EditHumanResourcePosition(HumanResourcePosition HumanResourcePositionmodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    HumanResourcePosition record = db.HumanResourcePosition.Where(d => d.HumanResourcePositionId == HumanResourcePositionmodel.HumanResourcePositionId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = HumanResourcePositionmodel.Content;
                        record.HumanResourcePositionName = HumanResourcePositionmodel.HumanResourcePositionName;
                        record.Language = HumanResourcePositionmodel.Language;
                        record.Workdef = HumanResourcePositionmodel.Workdef;
                       
                        record.Content = HumanResourcePositionmodel.Content;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.InsanKaynaklari.ToString();
                        logkeeper.Message = LogMessages.HumanResourcePositionEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.HumanResourcePositionName;
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
                        HumanResourcePosition sortingrecord = db.HumanResourcePosition.SingleOrDefault(d => d.HumanResourcePositionId == mid);
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
