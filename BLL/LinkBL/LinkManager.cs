﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace BLL.LinkBL
{
    public class LinkManager
    {

        public static List<ImportantLinks> GetImportantLinksList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ImportantLinks.Where(d => d.Language == language).OrderBy(d=>d.SortNumber).ToList();
                return list;
            }
        }


        public static List<ImportantLinks> GetImportantLinksListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ImportantLinks.Where(d => d.Language == language && d.Online == true).ToList();
                return list;
            }
        }

        public static bool AddImportantLinks(ImportantLinks record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.SortNumber = 9999;
                    record.Online = true;
                    db.ImportantLinks.Add(record);
                    db.SaveChanges();
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
                var list = db.ImportantLinks.SingleOrDefault(d => d.LinkId == id);
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
                    var record = db.ImportantLinks.FirstOrDefault(d => d.LinkId == id);
                    db.ImportantLinks.Remove(record);

                    db.SaveChanges();

                   

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ImportantLinks GetImportantLinksById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ImportantLinks record = db.ImportantLinks.Where(d => d.LinkId == nid).SingleOrDefault();
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


        public class JsonList
        {
            public string[] list { get; set; }
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
                        ImportantLinks sortingrecord = db.ImportantLinks.SingleOrDefault(d => d.LinkId == mid);
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

        public static bool EditImportantLink(ImportantLinks model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ImportantLinks record = db.ImportantLinks.Where(d => d.LinkId == model.LinkId).SingleOrDefault();
                    if (record != null)
                    {
                        record.Language = model.Language;
                        record.LinkName = model.LinkName;
                        record.LinkUrl = model.LinkUrl;
               
                        db.SaveChanges();
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

    }
}
