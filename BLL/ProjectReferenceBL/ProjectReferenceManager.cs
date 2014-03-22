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

namespace BLL.ProjectReferenceBL
{
    public class ProjectReferenceManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<ProjectReferences> GetProjectReferenceList(int gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProjectReferences.Where(d =>  d.ProjectReferenceGroupId == gid).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static List<ProjectReferences> GetProjectReferenceListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProjectReferences.Where(d => d.Language == language && d.Online==true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static List<ProjectReferences> GetProjectReferenceListForFront(int gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProjectReferences.Where(d => d.ProjectReferenceGroupId == gid && d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static List<ProjectReferences> GetCompletedProjectsForFront(bool status)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProjectReferences.Where(d => d.Devam == status && d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddProjectReference(ProjectReferences record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.SortOrder = 9999;
                    record.Online = true;
                    db.ProjectReferences.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.ReferenceAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Name;
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
                var list = db.ProjectReferences.SingleOrDefault(d => d.ProjectReferenceId == id);
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
                    var record = db.ProjectReferences.FirstOrDefault(d => d.ProjectReferenceId == id);
                    db.ProjectReferences.Remove(record);
                    db.SaveChanges();
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.ReferenceDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Name;
                    logkeeper.AddInfoLog(logger);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ProjectReferences GetProjectReferenceById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProjectReferences record = db.ProjectReferences.Where(d => d.ProjectReferenceId == nid).SingleOrDefault();
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

        public static bool EditProjectReference(ProjectReferences ProjectReferencemodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProjectReferences record = db.ProjectReferences.Where(d => d.ProjectReferenceId == ProjectReferencemodel.ProjectReferenceId).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = ProjectReferencemodel.Content;
                        record.Name = ProjectReferencemodel.Name;
                        record.SubTitle = ProjectReferencemodel.SubTitle;
                        record.Devam = ProjectReferencemodel.Devam;
                        if (ProjectReferencemodel.Logo != null)
                        {
                            record.Logo = ProjectReferencemodel.Logo;
                        }
                        record.Language = ProjectReferencemodel.Language;
                        if (!string.IsNullOrEmpty(ProjectReferencemodel.ProjectReferenceFile))
                        {
                            record.ProjectReferenceFile = ProjectReferencemodel.ProjectReferenceFile;
                        }
                        record.Content = ProjectReferencemodel.Content;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Referans.ToString();
                        logkeeper.Message = LogMessages.ReferenceEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.Name;
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
                        ProjectReferences sortingrecord = db.ProjectReferences.SingleOrDefault(d => d.ProjectReferenceId == mid);
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
