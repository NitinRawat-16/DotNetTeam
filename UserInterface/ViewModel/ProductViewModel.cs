using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModelLayer;

namespace UserInterface.ViewModel
{
    public class ProductViewModel
    {
        public List<ProductCategory> productCategories { get; set; }
        public Product product { get; set; }
        public List<Product> products { get; set; }
        public ProductSize productSize { get; set; }
        public HttpPostedFileBase imageUrl { get; set; }
        public string image {  get; set; }
    }
}