using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Brifing
    {
        public int Id { get; set; }
        public  string Il { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public string Ticari { get; set; }
        public string Konut { get; set; }
        public string FiyatAraligi { get; set; }

        public string MulkTipi { get; set; }
        public string OdaSayisi { get; set; }
        public string Boyut { get; set; }
        public string KrediyeUygunluk { get; set; }
        public string Detay { get; set; }
        public string IsinmaTipi { get; set; }
        public string Yas { get; set; }
        public string KatSayisi { get; set; }
        public string Kati { get; set; }
        public string YakiTipi { get; set; }
        public string Depozito { get; set; }
       
        public string YapiDurumu { get; set; }
        public string TapDurumu { get; set; }
        public string KullanimDurumu { get; set; }
        public string GetiriBeklentisi { get; set; }
        
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Tel { get; set; }
        public string Tarih { get; set; }

        public string EvTel { get; set; }
        public string CepTel { get; set; }
        public string IsTel { get; set; }
        public string Mesaj { get; set; }
        public string Email { get; set; }



        public int BrifingTip { get; set; }

    }
}
