using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ProductBL
{
    public class ProductSubbestGroupManager
    {
        public static List<ProductSubbestGroup> GetProductSubbestGroupList(string language, int groupid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ProductSubbestGroup.Where(d => d.Deleted == false && d.ProductSubGroupId == groupid).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static ProductSubbestGroup GetProductSubbestGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubbestGroup record = db.ProductSubbestGroup.Where(d => d.ProductSubbestGroupId == nid).SingleOrDefault();
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
        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ProductSubbestGroup.FirstOrDefault(d => d.ProductSubbestGroupId == id);
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
                var list = db.ProductSubbestGroup.SingleOrDefault(d => d.ProductSubbestGroupId == id);
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

        public static dynamic AddProductSubbestGroup(ProductSubbestGroup record)
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
                    
                    db.ProductSubbestGroup.Add(record);
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

        public static ProductSubbestGroup GetProductSubbestGroup(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubbestGroup record = db.ProductSubbestGroup.Where(d => d.ProductSubbestGroupId == id).SingleOrDefault();
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

        public static void EditSubbestGroup(int subbestgroupID, string GroupName, string pslug)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ProductSubbestGroup editrecord = db.ProductSubbestGroup.Where(d => d.ProductSubbestGroupId == subbestgroupID).SingleOrDefault();
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
                        ProductSubbestGroup sortingrecord = db.ProductSubbestGroup.SingleOrDefault(d => d.ProductSubbestGroupId == mid);
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
