using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name="Ürün Adı")]
        
        [Required(ErrorMessage="Ürün Adını Giriniz.")]
        public string Name { get; set; }

        [Display(Name = "Ürün Kodu")]
        [Required(ErrorMessage = "Ürün Kodunu Giriniz.")]
        public string Code { get; set; }

        [Display(Name = "Ürün Fiyatı")]
        [Required(ErrorMessage = "Ürün Fiyatını Giriniz.")]
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }

        [Display(Name = "Ürün Donanım Fiyatı")]
       
        public decimal ? HardwarePrice { get; set; }

        [Display(Name = "Ürün Donanımı Varmı?")]
        public bool Hardware { get; set; }

        [Required(ErrorMessage = "Ürün Grubunu Seçiniz.")]
        public int ProductGroupId{get;set;}

        public string MoneyType { get; set; }
        public string HMoneyType { get; set; }
        public ProductGroup ProductGroup { get; set; }

        [Required(ErrorMessage = "Ürün Alt Grubunu Seçiniz.")]
        public int ProductSubGroupId { get; set; }
        public ProductSubGroup ProductSubGroup { get; set; }
        
        [Required(ErrorMessage = "Ürün Alt Grubunu Seçiniz.")]
        public int ProductSubbestGroupId { get; set; }
        public ProductSubbestGroup ProductSubbestGroup { get; set; }

        [Required(ErrorMessage = "Ürün Alt Grubunu Seçiniz.")]
        public int ProductSubSubbestGroupId { get; set; }
        public ProductSubSubbestGroup ProductSubSubbestGroup { get; set; }

        public string ProductImage { get; set; }
        public string ProductImageThumb { get; set; }
        public string Content { get; set; }
        public string filetraining { get; set; }
        public string filexperiment { get; set; }
        public string filetechnical { get; set; }
        public string filevideo { get; set; }
        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public bool Deleted { get; set; }
        public bool Online { get; set; }
        public DateTime TimeCreated { get; set; }
        public int SortNumber { get; set; }
        [Display(Name = "Ürün Açıklaması")]
        public string PageSlug { get; set; }

        [Display(Name = "Marka")]
        [Required(ErrorMessage = "Marka Giriniz.")]
        public string Brand { get; set; }
        
        [Display(Name = "Yıl")]
        public string Year { get; set; }
        
    }
}
