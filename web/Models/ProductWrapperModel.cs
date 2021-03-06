﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProductWrapperModel
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<ProductGroup> productgroups { get; set; }
        //public IEnumerable<ProductSubGroup> productsubgroups { get; set; }

        public ProductWrapperModel(IEnumerable<Product> products, IEnumerable<ProductGroup> productgroups)
        {
            this.products = products;
            this.productgroups = productgroups;
            //this.productsubgroups = productsubgroups;
        }
    }

}