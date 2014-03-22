using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Areas.Admin.Filters;
using web.Areas.Admin.Helpers;
using BLL.LanguageBL;
using BLL.ProductBL;
using DAL.Entities;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ProductSubbestGroupController : Controller
    {
        public ActionResult Add(int id)
        {

            //return View();
            //string lang = FillLanguagesListForList();

            //string firstgroupID = GetProductGroups(lang);
            //string firstsubgroupID = GetProductSubGroups(firstgroupID);
            var subgrouplist = ProductSubbestGroupManager.GetProductSubbestGroupList("", id);
            var subgroup = ProductManager.GetProductSubGroupById(id);
            ViewBag.SubGroupName = subgroup.GroupName;
            ViewBag.SubGroupId = id;
            return View(subgrouplist);

        }

        [HttpPost]
        public ActionResult Add(string txtname, int sgID)
        {
            // subgroup add işlemi yapılacak
            ProductSubbestGroup model = new ProductSubbestGroup();
            model.GroupName = txtname;
            model.ProductSubGroupId = sgID;
            model.PageSlug = Utility.SetPagePlug(txtname);

            ViewBag.ProcessMessage = ProductSubbestGroupManager.AddProductSubbestGroup(model);

            var subgrouplist = ProductSubbestGroupManager.GetProductSubbestGroupList("", sgID);
            var subgroup = ProductManager.GetProductSubGroupById(sgID);
            ViewBag.SubGroupName = subgroup.GroupName;
            ViewBag.SubGroupId = sgID;
            return View(subgrouplist);
        }

        public ActionResult Edit(int id)
        {
            ProductSubbestGroup model = ProductSubbestGroupManager.GetProductSubbestGroup(id);
            ViewBag.SubGroupId = model.ProductSubGroupId;
            ViewBag.SubbestGroupId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string GroupName, int sgID, int subbestGroupID)
        {
            ProductSubbestGroupManager.EditSubbestGroup(subbestGroupID, GroupName, Utility.SetPagePlug(GroupName));
            return RedirectToAction("Add", new { id = sgID });
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ProductSubbestGroupManager.Sort(idsList);
            return Json(issorted);
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(ProductSubbestGroupManager.Delete(id));
        }

        public JsonResult UpdateStatus(int id)
        {
            return Json(ProductSubbestGroupManager.UpdateStatus(id));
        }

        private string GetProductSubGroups(string firstgroupID)
        {
            var subgroups = ProductManager.GetProductSubGroupList("", Convert.ToInt32(firstgroupID));
            string id;
            if (RouteData.Values["id"] == null)
            {
                if (subgroups != null && subgroups.Count != 0)
                    id = subgroups.First().ProductGroupId.ToString();
                else id = "0";
            }

            else id = RouteData.Values["id"].ToString();
            var subgrouplist = new SelectList(subgroups, "ProductSubGroupId", "GroupName");
            ViewBag.SubGroupList = subgrouplist;
            return id;
        }

        private string GetProductGroups(string lang)
        {
            var groups = ProductManager.GetProductGroupList(lang);
            string id;
            if (RouteData.Values["id"] == null)
            {
                if (groups != null && groups.Count != 0)
                    id = groups.First().ProductGroupId.ToString();
                else id = "0";
            }
            else id = RouteData.Values["id"].ToString();

            var grouplist = new SelectList(groups, "ProductGroupId", "GroupName", id);
            ViewBag.GroupList = grouplist;
            return id;
        }

        private string FillLanguagesListForList()
        {
            string lang = "";

            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;
            return lang;
        }

        //[HttpPost]
        //public ActionResult Index(string drplanguage, string txtname, HttpPostedFileBase uploadfile, string drpgroup)
        //{
        //    string id = FillLanguagesListForList();
        //    if (ModelState.IsValid)
        //    {
        //        ProductSubbestGroup model = new ProductSubbestGroup();
        //        model.GroupName = txtname;
        //        model.Language = drplanguage;
        //        model.ProductGroupId = Convert.ToInt32(drpgroup);
        //        if (uploadfile != null && uploadfile.ContentLength > 0)
        //        {
        //            Random random = new Random();
        //            int rand = random.Next(1000, 99999999);
        //            new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/productcategory/", Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
        //            model.GroupImage = "/Content/images/productcategory/" + Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
        //        }
        //        else
        //        {
        //            model.GroupImage = "/Content/images/front/noimage.jpeg";
        //        }

        //        model.PageSlug = Utility.SetPagePlug(txtname);
        //        ViewBag.ProcessMessage = ProductSubbestGroupManager.AddProductSubbestGroup(model);

        //        var grouplist = new List<ProductSubbestGroup>();// ProductSubbestGroupManager.GetProductSubbestGroupList(lang, gid);

        //        return View(grouplist);


        //    }
        //    return View();
        //}


        //public ActionResult EdtiGroup() 
        //{
        //    var lang = FillLanguagesList();

        //    var grouplist = ProductSubbestGroupManager.GetProductGroupList(lang);
        //    var grp = new SelectList(grouplist, "ProductGroupId", "GroupName");
        //    ViewBag.MainGroup = grp;
        //        if(RouteData.Values["id"]!=null)
        //        {
        //            int nid=0;
        //            bool isnumber=int.TryParse(RouteData.Values["id"].ToString(),out nid);
        //            if (isnumber)
        //            {
        //                ProductSubbestGroup editnews = ProductSubbestGroupManager.GetSubGroupById(nid);
        //                return View(editnews);
        //            }
        //            else
        //                return View();
        //        }
        //        else
        //            return View();
        // }

        //[HttpPost]
        //public ActionResult EdtiGroup(ProductSubbestGroup model, HttpPostedFileBase uploadfile)
        //{
        //    var lang = FillLanguagesList();

        //    var grouplist = ProductSubbestGroupManager.GetProductGroupList(lang);
        //    var grp = new SelectList(grouplist, "ProductGroupId", "GroupName");
        //    ViewBag.MainGroup = grp;
        //    if (ModelState.IsValid)
        //    {
        //        //ProductSubbestGroup model = new ProductSubbestGroup();
        //       // model.GroupName = txtname;
        //        //model.Language = drplanguage;
        //        if (uploadfile != null && uploadfile.ContentLength > 0)
        //        {
        //            Random random = new Random();
        //            int rand = random.Next(1000, 99999999);
        //            new ImageHelper(280, 240).SaveThumbnail(uploadfile, "/Content/images/productcategory/", Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName));
        //            model.GroupImage = "/Content/images/productcategory/" + Utility.SetPagePlug(model.GroupName) + "_" + rand + Path.GetExtension(uploadfile.FileName);
        //        }
        //        if (RouteData.Values["id"] != null)
        //        {
        //            int nid = 0;
        //            bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
        //            if (isnumber)
        //            {
        //                model.PageSlug = Utility.SetPagePlug(model.GroupName);
        //                model.ProductSubbestGroupId = nid;
        //                ViewBag.ProcessMessage = ProductSubbestGroupManager.EditProductSubbestGroup(model);
        //                return View(model);
        //            }
        //            else
        //            {
        //                ViewBag.ProcessMessage = false;
        //                return View(model);
        //            }
        //        }

        //    }
        //    return View();
        //}




        //public void UpdateRecord(int id, string name)
        //{
        //    string clearname = name.Replace("%47", "\'");
        //    string pageslug = Utility.SetPagePlug(clearname);
        //    ProductSubbestGroupManager.EditProductSubbestGroup(id, clearname, pageslug);
        //}


        //public JsonResult GroupEditStatus(int id)
        //{
        //    return Json(ProductSubbestGroupManager.UpdateSubGroupStatus(id));
        //}

        //public JsonResult DeleteRecord(int id)
        //{
        //    return Json(ProductSubbestGroupManager.DeleteSubGroup(id));
        //}


        //public JsonResult SortRecords(string list)
        //{
        //    JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
        //    string[] idsList = psl.list;
        //    bool issorted = ProductSubbestGroupManager.SortSubRecords(idsList);
        //    return Json(issorted);


        //}

        //public class JsonList
        //{
        //    public string[] list { get; set; }
        //}


        //string FillLanguagesList()
        //{
        //    string lang = "";
        //    if (RouteData.Values["lang"] == null)
        //        lang = "tr";
        //    else lang = RouteData.Values["lang"].ToString();

        //    var languages = LanguageManager.GetLanguages();
        //    var list = new SelectList(languages, "Culture", "Language", lang);
        //    ViewBag.LanguageList = list;
        //    return lang;
        //}
    }
}
