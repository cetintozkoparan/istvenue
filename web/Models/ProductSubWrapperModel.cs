using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProductSubWrapperModel
    {
        //public IEnumerable<Product> products { get; set; }
        public ProductGroup productgroup { get; set; }
        public ProductSubGroup productsubgroup { get; set; }
        public ProductSubbestGroup productsubbestgroup { get; set; }
        public IEnumerable<ProductSubGroup> productsubgroups { get; set; }
        public IEnumerable<ProductSubbestGroup> productsubbestgroups { get; set; }
        public IEnumerable<ProductSubSubbestGroup> productsubsubbestgroups { get; set; }

        public ProductSubWrapperModel(IEnumerable<ProductSubSubbestGroup> productsubsubbestgroups, IEnumerable<ProductSubbestGroup> productsubbestgroups, IEnumerable<ProductSubGroup> productsubgroups, ProductGroup productgroup, ProductSubGroup productsubgroup, ProductSubbestGroup productsubbestgroup)
        {
            this.productsubsubbestgroups = productsubsubbestgroups;
            this.productsubbestgroups = productsubbestgroups;
            this.productsubgroups = productsubgroups;
            this.productgroup = productgroup;
            this.productsubgroup = productsubgroup;
            this.productsubbestgroup = productsubbestgroup;
        }
    }
}