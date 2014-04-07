using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Estate
    {
        [Key]
        public int Id { get; set; }
        public int TypeId { get; set; }
        //İl
        public int CountryId { get; set; }
        //İlçe
        public int TownId { get; set; }
        //Semt
        public int DistrictId { get; set; }
        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyatı giriniz")]
        public int Price { get; set; }
        public int PriceTypeId { get; set; }
        //Metre kare
        [Display(Name = "Metre Kare")]
       
        public int Size { get; set; }
        //Bina yaşı
        [Display(Name = "Bina Yaşı")]
      
        public int Age { get; set; }
        //Danışman
        [Display(Name = "Danışman adı")]
        public int Consultant { get; set; }
        //Oda Sayısı
        [Display(Name = "Oda Sayısı adı")]
        public int RoomNumber { get; set; }

        [Display(Name = "Referans Numarası adı")]
        public int ReferenceNo { get; set; }

        [Display(Name = "Emlak Resmi adı")]
        public  string Photo { get; set; }
        //ilan açıklaması
        [Display(Name = "İlan İçeriği")]
        [Required(ErrorMessage = "İçeriği giriniz")]
        public string Content { get; set; }
       
        //İan Adı
        [Display(Name = "İlan Başlığı")]
        [Required(ErrorMessage = "İlan başlığını giriniz")]
        public string Header { get; set; }
        
    }
}
