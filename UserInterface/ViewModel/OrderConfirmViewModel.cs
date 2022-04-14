using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModelLayer;
using System.ComponentModel.DataAnnotations;
namespace UserInterface.ViewModel
{
    public class OrderConfirmViewModel
    {
        public OrderConfirmData_Result order { get; set; }
        [Required]
        public DateTime ExpectedDate { get; set; }
    }
}