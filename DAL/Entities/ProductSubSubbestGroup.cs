using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductSubSubbestGroup
    {
        [Key]
        public int ProductSubSubbestGroupId { get; set; }
        
        public int ProductSubbestGroupId { get; set; }

        [Required(ErrorMessage = "Grup Adını Giriniz")]
        [Display(Name = "Grup Adı")]
        public string GroupName { get; set; }
      
        public bool Online { get; set; }
        public bool Deleted { get; set; }

        public List<Product> Product { get; set; }

        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }

        public string PageSlug { get; set; }
        public int SortNumber { get; set; }
    }
}
