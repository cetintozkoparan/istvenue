using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            ControllerBuilder.Current.DefaultNamespaces.Add("web.Controllers");

         //   routes.MapRoute("home_default", "/", new { action = "Index", Controller = "Home" });
            routes.MapRoute("onecikangayrimenkuller_tr", "tr/one-cikan-gayrimenkuller", new { action = "PopularEstates", Controller = "FEstates" });
            routes.MapRoute("onecikangayrimenkuller_en", "en/popular-estates", new { action = "PopularEstates", Controller = "FEstates" });

            routes.MapRoute("home_tr", "tr/anasayfa", new { action = "Index", Controller = "FHome" });
            routes.MapRoute("home_en", "en/homepage", new { action = "Index", Controller = "FHome" });

            routes.MapRoute("aboutus_tr", "tr/hakkimizda", new { action = "Index", Controller = "FInstitutional" });
            routes.MapRoute("aboutus_en", "en/aboutus", new { action = "Index", Controller = "FInstitutional" });

            routes.MapRoute("whyus_tr", "tr/neden-biz", new { action = "WhyUs", Controller = "FInstitutional" });
            routes.MapRoute("whyus_en", "en/why-us", new { action = "WhyUs", Controller = "FInstitutional" });

            routes.MapRoute("sayfalar_tr", "tr/hakkimizda/{slug}/{pid}", new { action = "Page", Controller = "FInstitutional" });
            routes.MapRoute("sayfalar_en", "en/aboutus/{slug}/{pid}", new { action = "Page", Controller = "FInstitutional" });

            routes.MapRoute("visionmision_tr", "tr/vizyon-misyon", new { action = "VisionMision", Controller = "FInstitutional" });
            routes.MapRoute("visionmision_en", "en/vision-mision", new { action = "VisionMision", Controller = "FInstitutional" });

            routes.MapRoute("ourteam_tr", "tr/ekibimiz", new { action = "Index", Controller = "FOurTeam" });
            routes.MapRoute("ourteam_en", "en/ourteam", new { action = "Index", Controller = "FOurTeam" });

            //routes.MapRoute("products_tr", "tr/urunler", new { action = "Index", Controller = "FProducts" });
            //routes.MapRoute("products_en", "en/products", new { action = "Index", Controller = "FProducts" });

            routes.MapRoute("products_tr", "tr/urunler", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_en", "en/products", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_tr2", "tr/urunler/{group}/{gid}", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_en2", "en/products/{group}/{gid}", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_tr3", "tr/urunler/{group}/{subgroup}/{sgid}", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_en3", "en/products/{group}/{subgroup}/{sgid}", new { action = "Index", Controller = "FProducts" });

            routes.MapRoute("products_tr4", "tr/urunler/{group}/{subgroup}/{subbestgroup}/{subbestgid}", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_en4", "en/products/{group}/{subgroup}/{subbestgroup}/{subbestgid}", new { action = "Index", Controller = "FProducts" });

            routes.MapRoute("products_tr5", "tr/urunler/{group}/{subgroup}/{subbestgroup}/{subsubbestgroup}/{subsubbestgid}", new { action = "Index", Controller = "FProducts" });
            routes.MapRoute("products_en5", "en/products/{group}/{subgroup}/{subbestgroup}/{subsubbestgroup}/{subsubbestgid}", new { action = "Index", Controller = "FProducts" });

            routes.MapRoute("product_list_tr", "tr/urunler/{group}/{gid}", new { action = "ProductList", Controller = "FProducts" });
            routes.MapRoute("product_list_en", "en/products/{group}/{gid}", new { action = "ProductList", Controller = "FProducts" });

            routes.MapRoute("product_detail_tr1", "tr/urun-detay/{group}/{subgroup}/{productname}/{pid}", new { action = "ProductDetail", Controller = "FProducts" });
            routes.MapRoute("product_detail_en1", "en/product-detail/{group}/{subgroup}/{productname}/{pid}", new { action = "ProductDetail", Controller = "FProducts" });

            routes.MapRoute("product_detail_tr", "tr/urunler/{group}/{productname}/{pid}", new { action = "ProductDetail", Controller = "FProducts" });
            routes.MapRoute("product_detail_en", "en/products/{group}/{productname}/{pid}", new { action = "ProductDetail", Controller = "FProducts" });

            routes.MapRoute("documentgroups_tr", "tr/dokumanlar", new { action = "Index", Controller = "FDocuments" });
            routes.MapRoute("documentgroups_en", "en/documents", new { action = "Index", Controller = "FDocuments" });

            routes.MapRoute("documents_tr", "tr/dokumanlar/{group}/{gid}", new { action = "DocumentList", Controller = "FDocuments" });
            routes.MapRoute("documents_en", "en/documents/{group}/{gid}", new { action = "DocumentList", Controller = "FDocuments" });

            routes.MapRoute("contactdetails_tr", "tr/iletisim-bilgileri", new { action = "Index", Controller = "FContact" });
            routes.MapRoute("contactdetails_en", "en/contact-details", new { action = "Index", Controller = "FContact" });

            routes.MapRoute("contactform_tr", "tr/iletisim-formu", new { action = "Form", Controller = "FContact" });
            routes.MapRoute("contactform_en", "en/contact-form", new { action = "Form", Controller = "FContact" });

            routes.MapRoute("contactbank_tr", "tr/banka-hesaplari", new { action = "Bank", Controller = "FContact" });
            routes.MapRoute("contactbank_en", "en/bank-accounts", new { action = "Bank", Controller = "FContact" });

            routes.MapRoute("news_tr", "tr/tum-haberler", new { action = "Index", Controller = "FNews" });
            routes.MapRoute("news_en", "en/all-news", new { action = "Index", Controller = "FNews" });

            routes.MapRoute("newscontent_tr", "tr/haberler/{header}/{hid}", new { action = "NewsContent", Controller = "FNews" });
            routes.MapRoute("newscontent_en", "en/news/{header}/{hid}", new { action = "NewsContent", Controller = "FNews" });

            routes.MapRoute("career_tr", "tr/kariyer", new { action = "Index", Controller = "FCareer" });
            routes.MapRoute("career_en", "en/career", new { action = "Index", Controller = "FCareer" });

            routes.MapRoute("career_trx", "tr/insan-kaynaklari", new { action = "Index", Controller = "FCareer" });
            routes.MapRoute("career_enx", "en/human-resources", new { action = "Index", Controller = "FCareer" });
            routes.MapRoute("career_trx1", "tr/insan-kaynaklari/acik-pozisyonlar", new { action = "Positions", Controller = "FCareer" });
            routes.MapRoute("career_enx1", "en/human-resources/open-positions", new { action = "Positions", Controller = "FCareer" });

            routes.MapRoute("career_app_tr", "tr/kariyer/basvuru", new { action = "ApplicationForm", Controller = "FCareer" });
            routes.MapRoute("career_app_en", "en/career/application", new { action = "ApplicationForm", Controller = "FCareer" });

            routes.MapRoute("links_tr", "tr/linkler", new { action = "Index", Controller = "FLinks" });
            routes.MapRoute("links_en", "en/links", new { action = "Index", Controller = "FLinks" });

            routes.MapRoute("equipment_tr", "tr/ekipmanlar", new { action = "Index", Controller = "FEquipment" });
            routes.MapRoute("equipment_en", "en/equipments", new { action = "Index", Controller = "FEquipment" });

            routes.MapRoute("projects_tr", "tr/projeler", new { action = "Index", Controller = "FProjects" });
            routes.MapRoute("projects_en", "en/projects", new { action = "Index", Controller = "FProjects" });

            
            routes.MapRoute("projectcontent_tr", "tr/projeler/{header}/{id}", new { action = "ProjectContent", Controller = "FProjects" });
            routes.MapRoute("projectcontent_en", "en/projects/{header}/{id}", new { action = "ProjectContent", Controller = "FProjects" });

            routes.MapRoute("offers_tr", "tr/teklif", new { action = "Index", Controller = "FOffers" });
            routes.MapRoute("offers_en", "en/offer", new { action = "Index", Controller = "FOffers" });
            routes.MapRoute("empty_offer_list", "offer/empty", new { action = "_emptyOfferList", Controller = "FOffers" });

            routes.MapRoute("search_tr", "tr/arama", new { action = "Index", Controller = "FSearch" });
            routes.MapRoute("search_en", "en/search", new { action = "Index", Controller = "FSearch" });

            routes.MapRoute("reference_tr", "tr/referanslar", new { action = "Index", Controller = "FReferences" });
            routes.MapRoute("reference_en", "en/references", new { action = "Index", Controller = "FReferences" });

            //routes.MapRoute("reference_tr2", "tr/referanslar/{group}/{gid}", new { action = "Index", Controller = "FReferences" });
            //routes.MapRoute("reference_en2", "en/references/{group}/{gid}", new { action = "Index", Controller = "FReferences" });

            //routes.MapRoute("reference_tr3", "tr/referanslar/{devam}", new { action = "Index", Controller = "FReferences" });
            
            //routes.MapRoute("reference_tr5", "tr/referanslar/{gid}/{ref}/{rid}", new { action = "Detail", Controller = "FReferences" });
            //routes.MapRoute("reference_en5", "en/references/{gid}/{ref}/{rid}", new { action = "Detail", Controller = "FReferences" });

            routes.MapRoute("solutionpartner_tr", "tr/cozum-ortaklari", new { action = "Index", Controller = "FSolutionPartner" });
            routes.MapRoute("solutionpartner_en", "en/solution-partners", new { action = "Index", Controller = "FSolutionPartner" });

            routes.MapRoute("certificate_tr", "tr/sertifikalar", new { action = "Index", Controller = "FCertificate" });
            routes.MapRoute("certificate_en", "en/certificates", new { action = "Index", Controller = "FCertificate" });

            routes.MapRoute("service_tr", "tr/hizmetler/{group}/{service}/{sid}", new { action = "Index", Controller = "FService" });
            routes.MapRoute("service_en", "en/services/{group}/{service}/{sid}", new { action = "Index", Controller = "FService" });

            routes.MapRoute("service_trx", "tr/hizmetler/{group}/{gid}", new { action = "Index", Controller = "FService" });
            routes.MapRoute("service_enx", "en/services/{group}/{gid}", new { action = "Index", Controller = "FService" });

            routes.MapRoute("services_tr", "tr/hizmetler/", new { action = "Index", Controller = "FService" });
            routes.MapRoute("services_en", "en/services/", new { action = "Index", Controller = "FService" });

            routes.MapRoute("ourservices_tr", "tr/hizmetlerimiz/", new { action = "Hizmetlerimiz", Controller = "FService" });
            routes.MapRoute("ourservices_en", "en/ourservices/", new { action = "Hizmetlerimiz", Controller = "FService" });

            routes.MapRoute("sector_tr", "tr/sektorler/{group}/{sector}/{sid}", new { action = "Index", Controller = "FSector" });
            routes.MapRoute("sector_en", "en/sectors/{group}/{sector}/{sid}", new { action = "Index", Controller = "FSector" });

            routes.MapRoute("sectorgrp_tr", "tr/sektorler/{group}/{gid}", new { action = "Index", Controller = "FSector" });
            routes.MapRoute("sectorgrp_en", "en/sectors/{group}/{gid}", new { action = "Index", Controller = "FSector" });

            routes.MapRoute("oursectors_tr", "tr/sektorler/", new { action = "Index", Controller = "FSector" });
            routes.MapRoute("oursectors_en", "en/sectors/", new { action = "Index", Controller = "FSector" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FHome", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}