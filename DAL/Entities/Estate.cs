using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Estate
    {
        [Key]
        public int Id { get; set; }
         [Display(Name = "Emlak Tipi")]
        public int TypeId { get; set; }
        //İl
        [Display(Name = "İl Seçimi")]
        [Required(ErrorMessage = "İli giriniz")]
        public int CountryId { get; set; }

        //İlçe
        [Display(Name = "İlçe Seçimi")]
        [Required(ErrorMessage = "İlçeyi giriniz")]
        public int TownId { get; set; }
        //Semt
        [Display(Name = "Semt Seçimi")]
        [Required(ErrorMessage = "Semti giriniz")]
        public int DistrictId { get; set; }
        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyatı giriniz")]
        public decimal Price { get; set; }

        public string PriceTypeId { get; set; }
        
        //Metre kare
        [Display(Name = "Metre Kare")]
         public int Size { get; set; }
        
        //Bina yaşı
        [Display(Name = "Bina Yaşı")]
        public int Age { get; set; }
       
        //Danışman
        [Display(Name = "Danışman")]
        public string Consultant { get; set; }
        
        //Oda Sayısı
        [Display(Name = "Oda Sayısı")]
        public string RoomNumber { get; set; }

        [Display(Name = "Referans Numarası")]
        [Required(ErrorMessage = "Referans numarasını giriniz")]
        public string ReferenceNo { get; set; }

        [Display(Name = "Emlak Resmi")]
        public  string Photo { get; set; }
        //ilan açıklaması
        [Display(Name = "İlan İçeriği")]
        [Required(ErrorMessage = "İçeriği giriniz")]
        public string Content { get; set; }
       
        //İan Adı
        [Display(Name = "İlan Başlığı")]
        [Required(ErrorMessage = "İlan başlığını giriniz")]
        public string Header { get; set; }

        [Display(Name = "Öne Çıkan Emlak")]
        public bool Popular { get; set; }

        //ziyaret edilme sayısı. öne çıkanar için belki bunu kullanırız.
        public int VisitedCount { get; set; }

        [Display(Name = "Dil Seçimi")]
        [Required(ErrorMessage = "Dili giriniz")]
        public string Language { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        [Required(ErrorMessage = "Eklenme tarihini giriniz")]
        public string TimeCreated { get; set; }


        public virtual Country Country { get; set; }
        public virtual Town Town { get; set; }
        public virtual District District { get; set; }
    }
}
