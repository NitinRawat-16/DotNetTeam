using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer
{
    public class PaymentOrderModel
    {
        public int amount { get; set; }
        public string orderId { get; set; }
        public string razorPayKey { get; set; }
        public string name { get; set; }
        public string contactNumber { get; set; }
        public string currency { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string email { get; set; }

    }
}
