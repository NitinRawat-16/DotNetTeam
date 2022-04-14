using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataModelLayer
{
    public  class ProductSizeValidations
    {
        [Required(ErrorMessage ="Please add quantity of small type!")]
        [Range(0,1000,ErrorMessage ="Please add between 0 to 1000!")]
        public int S { get; set; }

        [Required(ErrorMessage = "Please add quantity of small type!")]
        [Range(0, 1000, ErrorMessage = "Please add between 0 to 1000!")]
        public int M { get; set; }

        [Required(ErrorMessage = "Please add quantity of small type!")]
        [Range(0, 1000, ErrorMessage = "Please add between 0 to 1000!")]
        public int L { get; set; }

        [Required(ErrorMessage = "Please add quantity of small type!")]
        [Range(0, 1000, ErrorMessage = "Please add between 0 to 1000!")]
        public int Xl { get; set; }
    }

    [MetadataType(typeof(ProductSizeValidations))]
    public partial class ProductSize 
    {

    }

}
