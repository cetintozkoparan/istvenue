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
//using BLL.ProductBL;
using DAL.Entities;
using BLL.PhotoBL;

namespace web.Areas.Admin.Controllers
{
    [AuthenticateUser]
    public class ProductController : Controller
    {


        public ActionResult Index()
        {
            string id = FillLanguagesListForProductList();

            int groupid = Convert.ToInt32(id);

            var list = ProductManager.GetProductList(groupid);
            return View(list);
        }

        public ActionResult AddProduct()
        {
            ImageHelperNew.DestroyImageCashAndSession(398, 299);
            FillLanguagesList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(IEnumerable<HttpPostedFileBase> attachments, Product model, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadfile2, HttpPostedFileBase uploadtechfile, HttpPostedFileBase uploadvideo, HttpPostedFileBase uploadexperimentfile, HttpPostedFileBase uploadtraining)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {
                if (Session["ModifiedImageId"] != null)
                {
                    model.ProductImage = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }
                else
                {
                    model.ProductImage = "/Content/images/front/noimage.jpeg";
                }

                if (uploadtraining != null && uploadtraining.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtraining.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName)));
                    model.filetraining = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName);
                }


                if (uploadtechfile != null && uploadtechfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName)));
                    model.filetechnical = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName);
                }

                if (uploadexperimentfile != null && uploadexperimentfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadexperimentfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName)));
                    model.filexperiment = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName);
                }

                if (uploadvideo != null && uploadvideo.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadvideo.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName)));
                    model.filevideo = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName);
                }
                model.PageSlug = Utility.SetPagePlug(model.Name);

                model.MoneyType = "TL";
                ModelState.Clear();
                ViewBag.ProcessMessage = ProductManager.AddProduct(model);

                foreach (var item in attachments)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        Random random = new Random();
                        int rand = random.Next(1000, 99999999);
                        new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(item.FileName));
                        Photo p = new Photo();
                        p.CategoryId = (int)PhotoType.Product;
                        p.ItemId = model.ProductId;
                        p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(item.FileName);
                        p.Online = true;
                        p.SortOrder = 9999;
                        p.Language = model.Language;
                        p.TimeCreated = DateTime.Now;
                        p.Title = PhotoType.Product.ToString();
                        PhotoManager.Save(p);
                    }
                }
                return View();
            }
            else
                return View();
            
        }


        string FillLanguagesListForProductList()
        {
            string lang = "";
            string id = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = ProductManager.GetProductGroupList(lang);

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


        public ActionResult EditProduct()
        {
            ImageHelperNew.DestroyImageCashAndSession(398, 299);
            //FillLanguagesList();

            if (RouteData.Values["id"] != null)
            {
                int nid = 0;
                bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                if (isnumber)
                {
                    Product record = ProductManager.GetProductById(nid);
                    var languages = LanguageManager.GetLanguages();
                    var list = new SelectList(languages, "Culture", "Language", record.Language);
                    ViewBag.LanguageList = list;
                    var groups = ProductManager.GetProductGroupList(record.Language);
                    var grouplist = new SelectList(groups, "ProductGroupId", "GroupName", record.ProductGroupId);
                    ViewBag.GroupList = grouplist;

                    var subgroups = ProductManager.GetProductSubGroupList(record.Language, record.ProductGroupId);
                    var subgrouplist = new SelectList(subgroups, "ProductSubGroupId", "GroupName", record.ProductSubGroupId);
                    ViewBag.subgrouplist = subgrouplist;


                    var subbestgroups = ProductManager.GetProductSubbestGroupList(record.Language, record.ProductSubGroupId);
                    ProductSubbestGroup psg = new ProductSubbestGroup();
                    psg.ProductSubbestGroupId = 37;
                    psg.GroupName = "Alt Grup Yok...";
                    subbestgroups.Insert(0,psg);

                    var subsubbestgroups = ProductManager.GetProductSubSubbestGroupList(record.Language, record.ProductSubbestGroupId);
                    ProductSubSubbestGroup psbg = new ProductSubSubbestGroup();
                    psbg.ProductSubSubbestGroupId = 5;
                    psbg.GroupName = "Alt Grup Yok...";
                    subsubbestgroups.Insert(0,psbg);

                    var subbestgrouplist = new SelectList(subbestgroups, "ProductSubbestGroupId", "GroupName", record.ProductSubbestGroupId);
                    ViewBag.subbestgrouplist = subbestgrouplist;
                    var subsubbestgrouplist = new SelectList(subsubbestgroups, "ProductSubSubbestGroupId", "GroupName", record.ProductSubSubbestGroupId);
                    ViewBag.subsubbestgrouplist = subsubbestgrouplist;

                    return View(record);
                }
                else
                    return View();
            }
            else
                return View();

        }


        [HttpPost]
        [ValidateInput(false)]

        public ActionResult EditProduct(IEnumerable<HttpPostedFileBase> attachments, Product model, HttpPostedFileBase uploadfile, HttpPostedFileBase uploadfile2, HttpPostedFileBase uploadtechfile, HttpPostedFileBase uploadvideo, HttpPostedFileBase uploadexperimentfile, HttpPostedFileBase uploadtraining)
        {
            FillLanguagesList();

            if (ModelState.IsValid)
            {

              
                if (Session["ModifiedImageId"] != null)
                {
                    model.ProductImage = "/Content/images/userfiles/" + Session["ModifiedImageId"].ToString() + Session["WorkingImageExtension"].ToString();
                    ImageHelperNew.DestroyImageCashAndSession(0, 0);
                }

                if (uploadtraining != null && uploadtraining.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtraining.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName)));
                    model.filetraining = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtraining.FileName);
                }


                if (uploadtechfile != null && uploadtechfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadtechfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName)));
                    model.filetechnical = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadtechfile.FileName);
                }

                if (uploadexperimentfile != null && uploadexperimentfile.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadexperimentfile.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName)));
                    model.filexperiment = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadexperimentfile.FileName);
                }

                if (uploadvideo != null && uploadvideo.ContentLength > 0)
                {
                    Random random = new Random();
                    int rand = random.Next(1000, 99999999);
                    uploadvideo.SaveAs(Server.MapPath("/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName)));
                    model.filevideo = "/Content/images/products/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(uploadvideo.FileName);
                }
                model.PageSlug = Utility.SetPagePlug(model.Name);
                
                if (RouteData.Values["id"] != null)
                {
                    int nid = 0;
                    bool isnumber = int.TryParse(RouteData.Values["id"].ToString(), out nid);
                    if (isnumber)
                    {
                        model.ProductId = nid;
                        
                        ViewBag.ProcessMessage = ProductManager.EditProduct(model);
                        foreach (var item in attachments)
                        {
                            if (item != null && item.ContentLength > 0)
                            {
                                Random random = new Random();
                                int rand = random.Next(1000, 99999999);
                                new ImageHelper(1024, 768).SaveThumbnail(item, "/Content/images/userfiles/", Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(item.FileName));
                                Photo p = new Photo();
                                p.CategoryId = (int)PhotoType.Product;
                                p.ItemId = model.ProductId;
                                p.Path = "/Content/images/userfiles/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(item.FileName);
                                p.Thumbnail = "/Content/images/userfiles/" + Utility.SetPagePlug(model.Name) + "_" + rand + Path.GetExtension(item.FileName);
                                p.Online = true;
                                p.SortOrder = 9999;
                                p.Language = model.Language;
                                p.TimeCreated = DateTime.Now;
                                p.Title = PhotoType.Product.ToString();
                                PhotoManager.Save(p);
                            }
                        }
                        return RedirectToAction("EditProduct", "Product", new { id = nid });                        
                    }
                    else
                    {
                        ViewBag.ProcessMessage = false;
                        return View(model);
                    }
                }
                else
                    return View();


               
                
            }
            else
                return View();
            
        }

        string FillLanguagesList()
        {
            string lang = "";
            if (RouteData.Values["lang"] == null)
                lang = "tr";
            else lang = RouteData.Values["lang"].ToString();

            var languages = LanguageManager.GetLanguages();
            var list = new SelectList(languages, "Culture", "Language");
            //var list = new SelectList(languages, "Culture", "Language", lang);
            ViewBag.LanguageList = list;

            var groups = ProductManager.GetProductGroupList(lang);

            ViewBag.GroupList = new SelectList(groups, "ProductGroupId", "GroupName");

            //ProductSubGroup xsg = new ProductSubGroup();
            //xsg.ProductSubGroupId = 55;
            //xsg.GroupName = "Alt Grup Seçiniz...";

            ProductSubbestGroup xsbg = new ProductSubbestGroup();
            xsbg.ProductSubbestGroupId = 37;
            xsbg.GroupName = "Alt Grup Seçiniz...";

            ProductSubSubbestGroup xssbg = new ProductSubSubbestGroup();
            xssbg.ProductSubSubbestGroupId =5;
            xssbg.GroupName = "Alt Grup Seçiniz...";

            //var lxsg = new List<ProductSubGroup>();
            //lxsg.Add(xsg);

            var lxsbg = new List<ProductSubbestGroup>();
            lxsbg.Add(xsbg);

            var lxssbg = new List<ProductSubSubbestGroup>();
            lxssbg.Add(xssbg);

            ViewBag.SubGroupList = new SelectList(new List<ProductSubGroup>(), "ProductSubGroupId", "GroupName");
            ViewBag.SubbestGroupList = new SelectList(lxsbg, "ProductSubbestGroupId", "GroupName",37);
            ViewBag.subSubbestGroupList = new SelectList(lxssbg, "ProductSubSubbestGroupId", "GroupName",5);
            return lang;
        }

        [HttpPost]
        public ActionResult LoadGroup(string lang)
        {
            var grouplst = ProductManager.GetProductGroupList(lang);
            JsonResult result = Json(new SelectList(grouplst, "ProductGroupId", "GroupName"));
            return result;
        }

        [HttpPost]
        public ActionResult LoadSubGroup(int id)
        {
            var grouplst = ProductManager.GetProductSubGroupList("",id);
            JsonResult result = Json(new SelectList(grouplst, "ProductSubGroupId", "GroupName"));
            return result;
        }

        [HttpPost]
        public ActionResult LoadSubbestGroup(int id)
        {
            var grouplst = ProductManager.GetProductSubbestGroupList("", id);
            JsonResult result = Json(new SelectList(grouplst, "ProductSubbestGroupId", "GroupName"));
            return result;
        }

        [HttpPost]
        public ActionResult LoadSubSubbestGroup(int id)
        {
            var grouplst = ProductManager.GetProductSubSubbestGroupList("", id);
            JsonResult result = Json(new SelectList(grouplst, "ProductSubSubbestGroupId", "GroupName"));
            return result;
        }

        public JsonResult EditStatus(int id)
        {
            return Json(ProductManager.UpdateStatus(id));
        }

        public JsonResult DeleteRecord(int id)
        {
            return Json(ProductManager.DeleteProduct(id));
        }

        public JsonResult RemoveTechnic(int id)
        {
            return Json(ProductManager.RemoveTechnic(id));
        }

        public JsonResult RemoveTraining(int id)
        {
            return Json(ProductManager.RemoveTraining(id));
        }

        public JsonResult RemoveExperimental(int id)
        {
            return Json(ProductManager.RemoveExperimental(id));
        }
        
        public JsonResult RemoveVideo(int id)
        {
            return Json(ProductManager.RemoveVideo(id));
        }
        
        public JsonResult SortRecords(string list)
        {
            JsonList psl = (new JavaScriptSerializer()).Deserialize<JsonList>(list);
            string[] idsList = psl.list;
            bool issorted = ProductManager.SortProducts(idsList);
            return Json(issorted);


        }
        public class JsonList
        {
            public string[] list { get; set; }
        }
    }
}
