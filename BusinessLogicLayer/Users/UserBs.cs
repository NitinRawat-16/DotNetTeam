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

        public IEnumerable<Product> ShowAllProducts()
        {
            var products = _userDb.GetAllProduct();
            return products;
        }

        public IEnumerable<Cart> GetCartItemsByUser(string userName)
        {
            return _userDb.GetCartItemsByUser(userName);
        }

        public void AddAddress(DeliveryAddress deliveryAddress, string userName)
        {
            _userDb.AddAddress(deliveryAddress, userName);
        }

        public IList<DeliveryAddress> GetDeliveryAddresses(string userName)
        {
            var deliveryAddress = _userDb.GetDeliveryAddresses(userName);
            return deliveryAddress;
        }

        public AspNetUser GetUserById(string userName)
        {
            return _userDb.GetUserById(userName);
        }
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

        public void OrderConfirmedByList(List<OrderConfirmed> orderConfirmeds)
        {
            _userDb.OrderConfirmedByList(orderConfirmeds);
        }



        public IEnumerable<Order> GetConfirmedOrder(string userName)
        {
         return  _userDb.GetConfirmedOrder(userName);  
        }

        public IEnumerable<Order> GetDeliveredOrder(string userName)
        {
         return  _userDb.GetDeliveredOrder(userName);

        }

        public IEnumerable<OrderConfirmed> GetPendingOrder(string userName)
        {
         return  _userDb.GetPendingOrder(userName);

        }

        public IEnumerable<OrderCanceled> GetcancelOrder(string userName)
        {
         return  _userDb.GetcancelOrder(userName);

        }

        public void RemoveFromCart(string userName)
        {
            _userDb.RemoveFromCart(userName);
           
        }


    }
}
