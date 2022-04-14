using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModelLayer
{
    public  class ProductValidation
    {
        [Required(ErrorMessage ="Name is required!")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = " Category is required!")]
        public int ProductCategoryId { get; set; }
        [Required(ErrorMessage = "Please fill up ,when you wan to get notified!")]
        public int ProductAdminNotification { get; set; }
        [Required(ErrorMessage = "Price is Required!")]
        [Range(1,100000,ErrorMessage ="Price value must be between 0 to 1 lakh!")]
        public int ProductPrice { get; set; }
    }

    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {
        
    }
}
