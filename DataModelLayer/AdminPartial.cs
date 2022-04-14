using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataModelLayer
{

    public  class Naveen
    {
       
    }
    
    public partial class OrderConfirmData_Result
    {
        
        public DateTime ExpectedDATE { get; set; }

    }
    
    
    public partial class OrderConfirmed
    {
        
        public string OrderCancelReason { get; set; }

    }




}
