﻿using BLL.LogBL;
using DAL.Context;
using DAL.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.EstateBL
{
    public class EstateManager
    {
        #region Country
        /************
         * İL
         * İLÇE
         * SEMTT 
        */
        public static List<Country> GetCountryList()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Country.ToList();
                return list;
            }
        }

        public static List<Town> GetTownList(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Town.Where(x=>x.CountryId==id).ToList();
                return list;
            }
        }

        public static List<District> GetDistrictList(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.District.Where(x => x.TownId == id).ToList();
                return list;
            }
        }

        //public static List<string> GetNames(int cid,int tid,int did)
        //{
        //    using (MainContext db = new MainContext())
        //    {
        //        var countrylist = db.Country.Where(x => x.TownId == id).FirstOrDefault();
        //        if(countrylist!=null)

        //        return list;
        //    }
        //}

        #endregion Country

        #region Estate

        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool AddEstate(Estate record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    db.Estate.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Emlak.ToString();
                    logkeeper.Message = LogMessages.EstateAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static List<Estate> GetEstateList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Estate.Include("Country").Include("Town").Include("District").Where(d => d.Language == language).ToList();
                return list;
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Estate.FirstOrDefault(d => d.Id == id);
                    db.Estate.Remove(record);

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Emlak.ToString();
                    logkeeper.Message = LogMessages.EstateDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion Estate
    }
}
