using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }
        
        [Required(ErrorMessage="Ekipman Adı Giriniz.")]
        [Display(Name="Ekipman Adı")]
        public string Name { get; set; }
        
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama Giriniz.")]
        public string Content { get; set; }
        
        public int SortNumber { get; set; }
        public bool Online { get; set; }
        
        [Display(Name = "Dil")]
        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
    }
}
