using DataLayer.Users;
using DataModelLayer;
using System.Collections.Generic;

namespace BusinessLogicLayer.Users
{
    public class UserBs
    {
        private readonly UserDb _userDb;
        public UserBs()
        {
            _userDb = new UserDb();
        }

        // View All Products
        public IEnumerable<Product> ShowAllProducts()
        {
            var products = _userDb.GetAllProduct();
            return products;
        }

        // View Cart Items Of User
        public IEnumerable<Cart> GetCartItemsByUser(string userName)
        {
            return _userDb.GetCartItemsByUser(userName);
        }

        // Add Address To Address Table
        public void AddAddress(DeliveryAddress deliveryAddress, string userName)
        {
            _userDb.AddAddress(deliveryAddress, userName);
        }

        // Return Save Addresses
        public IList<DeliveryAddress> GetDeliveryAddresses(string userName)
        {
            var deliveryAddress = _userDb.GetDeliveryAddresses(userName);
            return deliveryAddress;
        }

        // Return User Detail
        public AspNetUser GetUserById(string userName)
        {
            return _userDb.GetUserById(userName);
        }

        
        // 
        public List<string> GetAddressById(int id)
        {
            List<string> data = new List<string>();
            var deliveryAddress = _userDb.GetAddressById(id);
            var address = deliveryAddress.Address + "  " + deliveryAddress.City + "  " + deliveryAddress.State;
            var Mobile = deliveryAddress.PhoneNumber;
            data.Add(Mobile);
            data.Add(address);

            return data;
        }

        // Get Order Items And Confirmed Them
        public void OrderConfirmedByList(List<OrderConfirmed> orderConfirmeds)
        {
            _userDb.OrderConfirmedByList(orderConfirmeds);
        }

        // Return Confirmed Order Of User
        public IEnumerable<Order> GetConfirmedOrder(string userName)
        {
         return  _userDb.GetConfirmedOrder(userName);  
        }

        // Return Delivered Order Of User
        public IEnumerable<Order> GetDeliveredOrder(string userName)
        {
         return  _userDb.GetDeliveredOrder(userName);

        }

        // Return Pending Order Of User
        public IEnumerable<OrderConfirmed> GetPendingOrder(string userName)
        {
         return  _userDb.GetPendingOrder(userName);

        }

        // Return Canceled Order Of User
        public IEnumerable<OrderCanceled> GetcancelOrder(string userName)
        {
         return  _userDb.GetcancelOrder(userName);

        }

        // Remove Item From Cart After Buy Now
        public void RemoveFromCart(string userName)
        {
            _userDb.RemoveFromCart(userName);
        }


    }
}
