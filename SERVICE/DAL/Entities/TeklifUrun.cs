using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TeklifUrun
    {
        public int TeklifUrunId { get; set; }
        public int TeklifId { get; set; }
        public int UrunId { get; set; }
        public decimal Fiyat { get; set; }
        public int Adet { get; set; }
        public string Toplam { get; set; }
        public bool Donanim { get; set; }
        public decimal DonanimFiyat { get; set; }
        public string ParaBirimi { get; set; }
       
    }
}
