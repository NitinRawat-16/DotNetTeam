using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModelLayer;

namespace UserInterface.ViewModel
{
    public class OrderTableViewModel
    {
        public IEnumerable<Order> ProductConfirm{ get; set; }
        public IEnumerable<OrderConfirmed> ProductPending{ get; set; }
        public IEnumerable<OrderCanceled> ProductCancel{ get; set; }
        public IEnumerable<Order> ProductSuccessDelivered { get; set; }






       
    }
}