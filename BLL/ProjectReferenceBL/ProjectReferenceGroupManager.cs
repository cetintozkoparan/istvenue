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

namespace BLL.ProjectReferenceBL
{
    public class ProjectReferenceGroupManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region ProjectReferenceGroup
        public static List<ProjectReferenceGroup> GetProjectReferenceGroupList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProjectReferenceGroup.Where(d => d.Deleted == false && d.Language == language).OrderBy(d=>d.SortNumber).ToList();
                return list;
            }
        }

        public static List<ProjectReferenceGroup> GetProjectReferenceGroupListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProjectReferenceGroup.Where(d => d.Deleted == false && d.Language == language && d.Online==true).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }


        public static bool AddProjectReferenceGroup(ProjectReferenceGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.ProjectReferenceGroup.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    logkeeper.Message = LogMessages.ProjectReferenceGroupAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.GroupName;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public static bool UpdateGroupStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProjectReferenceGroup.SingleOrDefault(d => d.ProjectReferenceGroupId == id);
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


        public static bool DeleteGroup(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ProjectReferenceGroup.FirstOrDefault(d => d.ProjectReferenceGroupId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    logkeeper.Message = LogMessages.ProjectReferenceGroupDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.GroupName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ProjectReferenceGroup GetProjectReferenceGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProjectReferenceGroup record = db.ProjectReferenceGroup.Where(d => d.ProjectReferenceGroupId == nid).SingleOrDefault();
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

        public static bool EditProjectReferenceGroup(int id, string name,string pageslug)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProjectReferenceGroup record = db.ProjectReferenceGroup.Where(d => d.ProjectReferenceGroupId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {

                        record.GroupName = name;
                        record.PageSlug = pageslug;
                        record.TimeUpdated = DateTime.Now;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        logkeeper.Message = LogMessages.ProjectReferenceGroupEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.GroupName;
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
                        ProjectReferenceGroup sortingrecord = db.ProjectReferenceGroup.SingleOrDefault(d => d.ProjectReferenceGroupId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
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

        #endregion ProjectReferenceGroup

        public static object UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Document.SingleOrDefault(d => d.DocumentId == id);
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

        public static object DeleteDocument(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Document.FirstOrDefault(d => d.DocumentId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                    logkeeper.Message = LogMessages.DocumentDeleted;
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

        public static Document GetDocumentById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Document record = db.Document.Where(d => d.DocumentId == nid && d.Deleted==false).SingleOrDefault();
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



        public static bool SortDocuments(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Document sortingrecord = db.Document.SingleOrDefault(d => d.DocumentId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
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

