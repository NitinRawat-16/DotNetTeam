using System.Collections.Generic;
using DataModelLayer;

namespace UserInterface.ViewModel
{
    public class ViewProductsViewModel
    {
            public IEnumerable<Product> Products { get; set; }
            public List<string> SelectListItems = new List<string>() { "S", "M", "L", "XL" };
        

    }
}