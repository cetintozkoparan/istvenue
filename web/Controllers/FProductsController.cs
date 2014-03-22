using web.Models;
using BLL.ProductBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Areas.Admin.Helpers;
using BLL.PhotoBL;
using BLL.ServiceGroupBL;

namespace web.Controllers
{
    public class FProductsController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        //
        // GET: /Urunler/

        public ActionResult Index()
        {
            var product_group_list = ProductManager.GetProductGroupListForFront(lang);
            List<DAL.Entities.Product> product_list = new List<DAL.Entities.Product>();
            int index = 0;
            if (RouteData.Values["gid"] != null)
            {
                product_list = ProductManager.GetProductListForFront(Convert.ToInt32(RouteData.Values["gid"].ToString()));
                var grp = ProductManager.GetGroupById(Convert.ToInt32(RouteData.Values["gid"].ToString()));
                ViewBag.grpname = grp.GroupName;
                index = product_group_list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.ProductGroupId == Convert.ToInt32(RouteData.Values["gid"].ToString())).index;
            }
            else if (RouteData.Values["sgid"] != null)
            {
                product_list = ProductManager.GetProductListBySubGroupForFront(Convert.ToInt32(RouteData.Values["sgid"].ToString()));
                var subgrp = ProductManager.GetSubGroupById(Convert.ToInt32(RouteData.Values["sgid"].ToString()));
                var grp = ProductManager.GetGroupById(subgrp.ProductGroupId);
                ViewBag.grpid = subgrp.ProductGroupId;
                ViewBag.subgrpname = subgrp.GroupName;
                ViewBag.grpname = grp.GroupName;
                index = product_group_list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.ProductGroupId == subgrp.ProductGroupId).index;
            }
            else if (RouteData.Values["subbestgid"] != null)
            {
                product_list = ProductManager.GetProductListBySubbestGroupForFront(Convert.ToInt32(RouteData.Values["subbestgid"].ToString()));

                var subbestgrp = ProductManager.GetSubbestGroupById(Convert.ToInt32(RouteData.Values["subbestgid"].ToString()));
                var subgrp = ProductManager.GetSubGroupById(subbestgrp.ProductSubGroupId);
                var grp = ProductManager.GetGroupById(subgrp.ProductGroupId);

                ViewBag.grpid = subgrp.ProductGroupId;
                ViewBag.subgrpid = subgrp.ProductSubGroupId;

                ViewBag.subbestgrpname = subbestgrp.GroupName;
                ViewBag.subgrpname = subgrp.GroupName;
                ViewBag.grpname = grp.GroupName;
                index = product_group_list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.ProductGroupId == subgrp.ProductGroupId).index;
            }
            else if (RouteData.Values["subsubbestgid"] != null)
            {
                product_list = ProductManager.GetProductListBySubSubbestGroupForFront(Convert.ToInt32(RouteData.Values["subsubbestgid"].ToString()));

                var subsubbestgrp = ProductManager.GetSubSubbestGroupById(Convert.ToInt32(RouteData.Values["subsubbestgid"].ToString()));
                var subbestgrp = ProductManager.GetSubbestGroupById(Convert.ToInt32(subsubbestgrp.ProductSubbestGroupId));
                var subgrp = ProductManager.GetSubGroupById(subbestgrp.ProductSubGroupId);
                var grp = ProductManager.GetGroupById(subgrp.ProductGroupId);

                ViewBag.grpid = subgrp.ProductGroupId;
                ViewBag.subgrpid = subgrp.ProductSubGroupId;
                ViewBag.subbestgrpid = subbestgrp.ProductSubbestGroupId;

                ViewBag.subsubbestgrpname = subsubbestgrp.GroupName;
                ViewBag.subbestgrpname = subbestgrp.GroupName;
                ViewBag.subgrpname = subgrp.GroupName;
                ViewBag.grpname = grp.GroupName;
                index = product_group_list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.ProductGroupId == subgrp.ProductGroupId).index;
            }
            else
            {
                product_list = ProductManager.GetProductListAllForFront(lang);
            }

            product_group_list = ServiceGroupManager.Swap(product_group_list, 0, index);
            

            ProductWrapperModel modelbind = new ProductWrapperModel(product_list, product_group_list);
            return View(modelbind);
        }

        public ActionResult subproductgroups(int id)
        {
            var psg = ProductManager.GetProductSubGroupListForFront(lang, id);
            var pg = ProductManager.GetProductGroupById(id);
            ProductSubWrapperModel pswm = new ProductSubWrapperModel(new List<DAL.Entities.ProductSubSubbestGroup>(), new List<DAL.Entities.ProductSubbestGroup>(), psg, pg, new DAL.Entities.ProductSubGroup(), new DAL.Entities.ProductSubbestGroup());
            return PartialView("_subproductgroups", pswm);
        }

        public ActionResult subbestproductgroups(int id)
        {
            var psubbestg = ProductManager.GetProductSubbestGroupListForFront(lang, id);
            var psg = ProductManager.GetProductSubGroupById(id);
            var pg = ProductManager.GetProductGroupById(psg.ProductGroupId);
            ProductSubWrapperModel pswm = new ProductSubWrapperModel(new List<DAL.Entities.ProductSubSubbestGroup>(), psubbestg, new List<DAL.Entities.ProductSubGroup>(), pg, psg, new DAL.Entities.ProductSubbestGroup());
            return PartialView("_subbestproductgroups", pswm);
        }

        public ActionResult subsubbestproductgroups(int id)
        {
            var psubsubbestg = ProductSubSubbestGroupManager.GetProductSubSubbestGroupListForFront(lang, id);
            var psbstg = ProductManager.GetProductSubbestGroupById(id);
            var psg = ProductManager.GetProductSubGroupById(psbstg.ProductSubGroupId);
            var pg = ProductManager.GetProductGroupById(psg.ProductGroupId);
            ProductSubWrapperModel pswm = new ProductSubWrapperModel(psubsubbestg, new List<DAL.Entities.ProductSubbestGroup>(), new List<DAL.Entities.ProductSubGroup>(), pg, psg, psbstg);
            return PartialView("_subsubbestproductgroups", pswm);
        }

        //public ActionResult ProductList(int gid)
        //{
        //    var product_group_list = ProductManager.GetProductGroupListForFront(lang);
        //    var product_list = ProductManager.GetProductList(gid);
        //    ProductWrapperModel modelbind = new ProductWrapperModel(product_list, product_group_list);
        //    return View(modelbind);
        //}

        public ActionResult ProductDetail(int pid)
        {
            var product_group_list = ProductManager.GetProductGroupListForFront(lang);
            int index = 0;
            
            var product = ProductManager.GetProductById(pid);
            var psg = ProductManager.GetProductSubGroupById(product.ProductSubGroupId);
            var pg = ProductManager.GetProductGroupById(product.ProductGroupId);
            index = product_group_list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.ProductGroupId == pg.ProductGroupId).index;
            product_group_list = ServiceGroupManager.Swap(product_group_list, 0, index);

            ViewBag.grpid = pg.ProductGroupId;
            ViewBag.sgid = psg.ProductSubGroupId;
            ViewBag.subgrpname = psg.GroupName;
            ViewBag.grpname = pg.GroupName;

            var photos = PhotoManager.GetList((int)PhotoType.Product, pid);

            ProductDetailWrapperModel model = new ProductDetailWrapperModel(product, product_group_list, photos);
            
            return View(model);
        }

        [HttpPost]
        public string AddToList(string id)
        {
            if (!this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OfferList"))
            {
                HttpCookie cookie = new HttpCookie("OfferList");
                cookie.Value = "[{id:'" + id + "'}]";
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return "1";
            }
            else
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
                cookie.Value = "[";

                foreach (var element in values)
                {
                    foreach (var entry in element)
                    {
                        if (entry.Value == id)
                            return values.Count().ToString();

                        cookie.Value += "{id:'" + entry.Value + "'},";
                    }
                }

                cookie.Value += "{id:'" + id + "'}]";

                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return (values.Count() + 1).ToString();
            }
        }

        [HttpPost]
        public string RemoveFromList(string id)
        {
            HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["OfferList"];
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(cookie.Value);
            cookie.Value = "[";

            foreach (var element in values)
            {
                foreach (var entry in element)
                {
                    if (entry.Value == id)
                        continue;

                    cookie.Value += "{id:'" + entry.Value + "'},";
                }
            }
            if (cookie.Value.Equals("["))
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
            else
            {
                cookie.Value = cookie.Value.Substring(0, cookie.Value.Length-1) + "]";
            }

            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            return (values.Count() - 1).ToString();
        }

    }
}
