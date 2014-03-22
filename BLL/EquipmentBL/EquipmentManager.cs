using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace BLL.LinkBL
{
    public class EquipmentManager
    {

        public static List<Equipment> GetList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Equipment.Where(d => d.Language == language).OrderBy(d=>d.SortNumber).ToList();
                return list;
            }
        }


        public static List<Equipment> GetListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Equipment.Where(d => d.Language == language && d.Online == true).ToList();
                return list;
            }
        }

        public static bool Add(Equipment record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.SortNumber = 9999;
                    record.Online = true;
                    db.Equipment.Add(record);
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
                var list = db.Equipment.SingleOrDefault(d => d.EquipmentId == id);
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
                    var record = db.Equipment.FirstOrDefault(d => d.EquipmentId == id);
                    db.Equipment.Remove(record);

                    db.SaveChanges();

                   

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Equipment GetById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Equipment record = db.Equipment.Where(d => d.EquipmentId == nid).SingleOrDefault();
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
                        Equipment sortingrecord = db.Equipment.SingleOrDefault(d => d.EquipmentId == mid);
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

        public static bool Edit(Equipment model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Equipment record = db.Equipment.Where(d => d.EquipmentId == model.EquipmentId).SingleOrDefault();
                    if (record != null)
                    {
                        record.Language = model.Language;
                        record.Name = model.Name;
                        record.Content = model.Content;
               
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
