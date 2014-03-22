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
    public class ProductSubSubbestGroupController : Controller
    {
        public ActionResult Add(int subid)
        {

            //return View();
            //string lang = FillLanguagesListForList();

            //string firstgroupID = GetProductGroups(lang);
            //string firstsubgroupID = GetProductSubGroups(firstgroupID);
            var subgrouplist = ProductSubSubbestGroupManager.GetProductSubSubbestGroupList("", subid);
            var subgroup = ProductSubbestGroupManager.GetProductSubbestGroupById(subid);
            ViewBag.SubGroupName = subgroup.GroupName;
            ViewBag.SubGroupId = subid;
            return View(subgrouplist);

        }

        [HttpPost]
        public ActionResult Add(string txtname, int sgID)
        {
            // subgroup add işlemi yapılacak
            ProductSubSubbestGroup model = new ProductSubSubbestGroup();
            model.GroupName = txtname;
            model.ProductSubbestGroupId = sgID;
            model.PageSlug = Utility.SetPagePlug(txtname);

            ViewBag.ProcessMessage = ProductSubSubbestGroupManager.AddProductSubSubbestGroup(model);

            var subgrouplist = ProductSubSubbestGroupManager.GetProductSubSubbestGroupList("", sgID);
            var subgroup = ProductManager.GetProductSubGroupById(sgID);
            ViewBag.SubGroupName = subgroup.GroupName;
            ViewBag.SubGroupId = sgID;
            return View(subgrouplist);
        }

        public ActionResult Edit(int subid)
        {
            ProductSubSubbestGroup model = ProductSubSubbestGroupManager.GetProductSubSubbestGroup(subid);
            ViewBag.SubbestGroupId = model.ProductSubbestGroupId;
            ViewBag.SubSubbestGroupId = subid;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string GroupName, int sgID, int subsubbestGroupID)
        {
            ProductSubSubbestGroupManager.EditSubSubbestGroup(subsubbestGroupID, GroupName, Utility.SetPagePlug(GroupName));
            return RedirectToAction("Add", new { id = 2, subid = sgID});
        }

        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ProductSubSubbestGroupManager.Sort(idsList);
            return Json(issorted);
        }

        public class JsonList
        {
            public string[] list { get; set; }
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(ProductSubSubbestGroupManager.Delete(id));
        }

        public JsonResult UpdateStatus(int id)
        {
            return Json(ProductSubSubbestGroupManager.UpdateStatus(id));
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
        //        ProductSubSubbestGroup model = new ProductSubSubbestGroup();
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
        //        ViewBag.ProcessMessage = ProductSubSubbestGroupManager.AddProductSubSubbestGroup(model);

        //        var grouplist = new List<ProductSubSubbestGroup>();// ProductSubSubbestGroupManager.GetProductSubSubbestGroupList(lang, gid);

        //        return View(grouplist);


        //    }
        //    return View();
        //}


        //public ActionResult EdtiGroup() 
        //{
        //    var lang = FillLanguagesList();

        //    var grouplist = ProductSubSubbestGroupManager.GetProductGroupList(lang);
        //    var grp = new SelectList(grouplist, "ProductGroupId", "GroupName");
        //    ViewBag.MainGroup = grp;
        //        if(RouteData.Values["id"]!=null)
        //        {
        //            int nid=0;
        //            bool isnumber=int.TryParse(RouteData.Values["id"].ToString(),out nid);
        //            if (isnumber)
        //            {
        //                ProductSubSubbestGroup editnews = ProductSubSubbestGroupManager.GetSubGroupById(nid);
        //                return View(editnews);
        //            }
        //            else
        //                return View();
        //        }
        //        else
        //            return View();
        // }

        //[HttpPost]
        //public ActionResult EdtiGroup(ProductSubSubbestGroup model, HttpPostedFileBase uploadfile)
        //{
        //    var lang = FillLanguagesList();

        //    var grouplist = ProductSubSubbestGroupManager.GetProductGroupList(lang);
        //    var grp = new SelectList(grouplist, "ProductGroupId", "GroupName");
        //    ViewBag.MainGroup = grp;
        //    if (ModelState.IsValid)
        //    {
        //        //ProductSubSubbestGroup model = new ProductSubSubbestGroup();
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
        //                model.ProductSubSubbestGroupId = nid;
        //                ViewBag.ProcessMessage = ProductSubSubbestGroupManager.EditProductSubSubbestGroup(model);
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
        //    ProductSubSubbestGroupManager.EditProductSubSubbestGroup(id, clearname, pageslug);
        //}


        //public JsonResult GroupEditStatus(int id)
        //{
        //    return Json(ProductSubSubbestGroupManager.UpdateSubGroupStatus(id));
        //}

        //public JsonResult DeleteRecord(int id)
        //{
        //    return Json(ProductSubSubbestGroupManager.DeleteSubGroup(id));
        //}


        //public JsonResult SortRecords(string list)
        //{
        //    JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
        //    string[] idsList = psl.list;
        //    bool issorted = ProductSubSubbestGroupManager.SortSubRecords(idsList);
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
