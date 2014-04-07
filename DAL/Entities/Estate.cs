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
        public int Price { get; set; }
        public int PriceTypeId { get; set; }
        //Metre kare
        public int Size { get; set; }
        //Bina yaşı
        public int Age { get; set; }
        //Danışman
        public int Consultant { get; set; }
        //Oda Sayısı
        public int RoomNumber { get; set; }

        public int ReferenceNo { get; set; }

        public  string Photo { get; set; }
        //ilan açıklaması
        public string Content { get; set; }
       
        //İan Adı
        public string Header { get; set; }
        
    }
}
