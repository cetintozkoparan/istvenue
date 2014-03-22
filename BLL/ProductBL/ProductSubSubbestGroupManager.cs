using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ProductBL
{
    public class ProductSubSubbestGroupManager
    {
        public static List<ProductSubSubbestGroup> GetProductSubSubbestGroupList(string language, int groupid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductSubSubbestGroup.Where(d => d.Deleted == false && d.ProductSubbestGroupId == groupid).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<ProductSubSubbestGroup> GetProductSubSubbestGroupListForFront(string language, int groupid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductSubSubbestGroup.Where(d => d.Deleted == false && d.ProductSubbestGroupId == groupid && d.Online == true).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ProductSubSubbestGroup.FirstOrDefault(d => d.ProductSubSubbestGroupId == id);
                    record.Deleted = true;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductSubSubbestGroup.SingleOrDefault(d => d.ProductSubSubbestGroupId == id);
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

        public static dynamic AddProductSubSubbestGroup(ProductSubSubbestGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortNumber = 9999;
                    record.TimeUpdated = DateTime.Now;
                    
                    db.ProductSubSubbestGroup.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupAdded;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static ProductSubSubbestGroup GetProductSubSubbestGroup(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubSubbestGroup record = db.ProductSubSubbestGroup.Where(d => d.ProductSubSubbestGroupId == id).SingleOrDefault();
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

        public static void EditSubSubbestGroup(int subbestgroupID, string GroupName, string pslug)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubSubbestGroup editrecord = db.ProductSubSubbestGroup.Where(d => d.ProductSubSubbestGroupId == subbestgroupID).SingleOrDefault();
                    editrecord.GroupName = GroupName;
                    editrecord.PageSlug = pslug;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    
                }
            }

        }

        public static bool Sort(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        ProductSubSubbestGroup sortingrecord = db.ProductSubSubbestGroup.SingleOrDefault(d => d.ProductSubSubbestGroupId == mid);
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
