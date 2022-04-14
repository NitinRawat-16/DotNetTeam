using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using DataModelLayer;

namespace DataLayer.Users
{
    public class UserDb
    {
        private readonly PortalEntities _dbContext;
        public UserDb()
        {
            _dbContext = new PortalEntities();
        }

        public IEnumerable<Product> GetAllProduct()
        {
            var products = _dbContext.Products;
            return products;
        }

        public IEnumerable<Cart> GetCartItemsByUser(string userName)
        {
            var user = _dbContext.AspNetUsers.Where(u => u.UserName == userName).First();
            return _dbContext.Carts.Include(c => c.Product).Include(u => u.AspNetUser).Where(u => u.UserId == user.Id).ToList();
        }

        public void AddAddress(DeliveryAddress deliveryAddress, string userName)
        {
            var user = _dbContext.AspNetUsers.Where(u => u.UserName == userName).First();
            DeliveryAddress address = new DeliveryAddress
            {
                UserId = user.Id,
                Name = deliveryAddress.Name,
                Address = deliveryAddress.Address,
                City = deliveryAddress.City,
                State = deliveryAddress.State,
                PhoneNumber = deliveryAddress.PhoneNumber,
            };
            _dbContext.DeliveryAddresses.Add(address);
            _dbContext.SaveChanges();
        }

        public IList<DeliveryAddress> GetDeliveryAddresses(string userName)
        {
            var user = _dbContext.AspNetUsers.Where(u => u.UserName == userName).First();
            var deliveryAddress = _dbContext.DeliveryAddresses.Where(u => u.UserId == user.Id).ToList();
            return deliveryAddress;
        }
        public AspNetUser GetUserById(string userName)
        {
            return _dbContext.AspNetUsers.Where(_u => _u.UserName == userName).First();
        }
        public DeliveryAddress GetAddressById(int id)
        {
                return _dbContext.DeliveryAddresses.Where(_u => _u.Id == id).First();
             
        }

        public void OrderConfirmedByList(List<OrderConfirmed>orderConfirmeds)
        {
            foreach(OrderConfirmed item in orderConfirmeds)
            { 
            _dbContext.OrderConfirmeds.Add(item);
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetConfirmedOrder(string userName)
        {
            var users=_dbContext.AspNetUsers.Where(u=>u.UserName==userName).First();
            return _dbContext.Orders
                .Include(o => o.OrderDetail)
                .Include(o => o.Product)
                .Where(o => o.UserId == users.Id && o.OrderDetail.OrderDeliveryDate==null).ToList();
        }

        public IEnumerable<Order> GetDeliveredOrder(string userName)
        {
            var users = _dbContext.AspNetUsers.Where(u => u.UserName == userName).First();
            var data = _dbContext.Orders
                .Include(o => o.OrderDetail)
                .Include(o => o.Product)
                .Where(o => o.UserId == users.Id).ToList();

            return data.Where(o => o.OrderDetail.OrderDeliveryDate!=null);
        }

        public IEnumerable<OrderConfirmed> GetPendingOrder(string userName)
        {
            var users = _dbContext.AspNetUsers.Where(u => u.UserName == userName).First();
            return _dbContext.OrderConfirmeds.Include(o => o.Product).Where(o => o.CustomerId == users.Id).ToList();
        }

        public IEnumerable<OrderCanceled> GetcancelOrder(string userName)
        {
            var users = _dbContext.AspNetUsers.Where(u => u.UserName == userName).First();
            return _dbContext.OrderCanceleds.Include(o => o.Product).Where(o => o.CustomerId == users.Id).ToList();
        }


        public void RemoveFromCart(string userName)
        {
            var users=_dbContext.AspNetUsers.Where(u => u.UserName == userName).First();
            var cartItem=_dbContext.Carts.Where(c=>c.UserId==users.Id).ToList();
            foreach(var cart in cartItem)
            {

                _dbContext.Carts.Remove(cart);
            }
            _dbContext.SaveChanges();
        }


    }
}
