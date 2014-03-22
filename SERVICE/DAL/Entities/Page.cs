using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Page
    {
        public int PageId{get;set;}
        [Required(ErrorMessage = "Başlık giriniz")]
        [DisplayName("Başlık")]
        public string Header { get; set; }
        [Required(ErrorMessage = "İçerik giriniz")]
        [DisplayName("İçerik")]
        public string Content { get; set; }
        public string PageSlug { get; set; }
        public DateTime TimeUpdated{get;set;}
        public string Language { get; set; }
        public bool Deleted { get; set; }
        public int SortOrder { get; set; }
    }
}
