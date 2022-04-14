using DataModelLayer;
using System.Collections.Generic;

namespace UserInterface.ViewModel
{
    public class DeliveryAddressesViewModel
    {

        public IList<DeliveryAddress> DeliveryAddress { get; set; }
        public int DeliveryAddressSelect { get; set; }
        public string Name { get; set; }
        public IEnumerable<Cart> Carts { get; set; }

    }
}