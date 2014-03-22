using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }
        [Display(Name="Proje İsmi")]
        [Required(ErrorMessage="Proje İsmini Giriniz.")]
        public string Name { get; set; }
        [Display(Name = "Alt Açıklama")]
        public string SubTitle { get; set; }
        [Display(Name = "Proje Açıklaması")]
        public string Content { get; set; }
        [Display(Name = "Proje Ek Doya")]
        public string ProjectFile { get; set; }
        public bool Online { get; set; }
        public int SortOrder { get; set; }
        public DateTime TimeCreated { get; set; }
        [Display(Name = "Dil")]
        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public string Logo { get; set; }
        public bool Deleted { get; set; }
    }
}
