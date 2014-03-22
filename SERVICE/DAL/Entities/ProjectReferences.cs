using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProjectReferences
    {
        [Key]
        public int ProjectReferenceId { get; set; }
        [Required(ErrorMessage = "Referans Grubunu Seçiniz.")]
        public int ProjectReferenceGroupId { get; set; }
        [Display(Name = "Referans İsmi")]
        [Required(ErrorMessage = "Referans İsmini Giriniz.")]
        public string Name { get; set; }
        [Display(Name = "Referans Alt Başlık")]
        public string SubTitle { get; set; }
        [Display(Name = "Referans Açıklaması")]
        public string Content { get; set; }
        [Display(Name = "Referans Ek Doya")]
        public string ProjectReferenceFile { get; set; }
        public string Logo { get; set; }
        public bool Online { get; set; }
        public int SortOrder { get; set; }
        public DateTime TimeCreated { get; set; }
        [Display(Name = "Dil")]
        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public bool Devam { get; set; }
    }
}
