using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SearchBL
{
    public class SearchManager
    {
        public static List<Tuple<string, string>> Search(string text)
        {
            string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
            
            using (MainContext db = new MainContext())
            {
                var references = db.ProjectReferences.Where(d => d.Online == true && d.Language == lang).FullTextSearch(text);
                var servicegs = db.ServiceGroup.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var services = db.Service.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var sectorgs = db.SectorGroup.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var sectors = db.Sector.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var news = db.News.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var projects = db.Projects.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var prods = db.Product.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                
                var result = new List<Tuple<string, string>>();
                string route, link = string.Empty;

                foreach (var item in references)
                {
                    if (lang.Equals("tr")) route = "referanslar"; else route = "references";
                    link = "/" + lang + "/" + route + "/" + item.ProjectReferenceGroupId + "/" + item.PageSlug + "/" + item.ProjectReferenceId;
                    result.Add(Tuple.Create(item.Name, link));
                }

                foreach (var item in servicegs)
                {
                    if (lang.Equals("tr")) route = "hizmetler"; else route = "services";
                    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.ServiceGroupId;
                    result.Add(Tuple.Create(item.GroupName, link));
                }

                foreach (var item in services)
                {
                    if (lang.Equals("tr")) route = "hizmetler"; else route = "services";
                    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.ServiceGroupId + "/" + item.ServiceId;
                    result.Add(Tuple.Create(item.Name, link));
                }

                foreach (var item in sectorgs)
                {
                    if (lang.Equals("tr")) route = "sektorler"; else route = "sectors";
                    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.SectorGroupId;
                    result.Add(Tuple.Create(item.GroupName, link));
                }

                foreach (var item in sectors)
                {
                    if (lang.Equals("tr")) route = "sektorler"; else route = "sectors";
                    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.SectorGroupId +"/" + item.SectorId;
                    result.Add(Tuple.Create(item.Name, link));
                }

                foreach (var item in news)
                {
                    if (lang.Equals("tr")) route = "haberler"; else route = "news";
                    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.NewsId;
                    result.Add(Tuple.Create(item.Header, link));
                }

                foreach (var item in projects)
                {
                    if (lang.Equals("tr")) route = "projeler"; else route = "projects";
                    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.ProjectId;
                    result.Add(Tuple.Create(item.Name, link));   
                }

                foreach (var item in prods)
                {
                    if (lang.Equals("tr"))
                        route = "urunler";
                    else
                        route = "products";

                    DAL.Entities.Product prod = ProductBL.ProductManager.GetProductById(item.ProductId);

                    if (prod != null)
                    {
                        link = "/" + lang + "/" + route + "/" + prod.ProductGroup.PageSlug + "/" + item.PageSlug + "/" + item.ProductId;

                        result.Add(Tuple.Create(item.Name, link));
                    }

                    
                }
                return result;
            }
        }
    }
}
