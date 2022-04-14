using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace DataLayer
{
    public class CartDb
    {
        private PortalEntities Db;
        public CartDb() => Db = new PortalEntities();

        public IEnumerable<Product> GetAll()
        {
            var products = Db.Products.ToList();
            return products;
        }


        public IEnumerable<Product> GetProductsById(List<int> ProductIds)
        {
            return Db.Products.Where(x => ProductIds.Contains(x.ProductId)).ToList();
        } 
        public void MigrateToDb(List<Product> products , string userName)
        {
            var user= Db.AspNetUsers.Where(x => x.UserName == userName).First();
            
            foreach(var product in products)
            {
                int quantity = 1;
                var qty = Db.Carts.Where(c => c.ProductId == product.ProductId && c.UserId == user.Id).FirstOrDefault();
                if(qty != null)
                {
                    qty.Quantity = qty.Quantity + 1; 
                }
                else
                {
                    var cartItem = new Cart()
                    {
                        UserId = user.Id,
                        ProductId = product.ProductId,
                        Quantity = quantity
                    };
                    Db.Carts.Add(cartItem);

                }
               Db.SaveChanges();
            }
        }

        public void MigrateToDbById(List<int> productIds, string userName)
        {
            var user = Db.AspNetUsers.Where(x => x.UserName == userName).First();
            var products=GetProductsById(productIds);
            foreach (var product in products)
            {
                int quantity = 1;
                var qty = Db.Carts.Where(c => c.ProductId == product.ProductId && c.UserId == user.Id).FirstOrDefault();
                if (qty != null)
                {
                    qty.Quantity = qty.Quantity + 1;
                }
                else
                {
                    var cartItem = new Cart()
                    {
                        UserId = user.Id,
                        ProductId = product.ProductId,
                        Quantity = quantity
                    };
                    Db.Carts.Add(cartItem);

                }
                Db.SaveChanges();
            }
        }



        public IList<Cart> GetAllById(string user )
        {
            List<Product> products = new List<Product>();
            var users = Db.AspNetUsers.Where(x => x.UserName == user).FirstOrDefault();
             var cartItems = Db.Carts.Include(c => c.Product).Where(c => c.UserId == users.Id).ToList();
            foreach (var cartItem in cartItems)
            {
                products.Add(cartItem.Product);
            }
            return cartItems;
        }

      public void RemoveCartItemByUserId(string user , Product product)
        {
            var users = Db.AspNetUsers.Where(x => x.UserName == user).First();
            var cartItem = Db.Carts.Where(c=> c.UserId == users.Id && c.ProductId == product.ProductId).First();
           if(cartItem.Quantity == 1)
            {
                Db.Carts.Remove(cartItem);
             
            }
            else
            {
                cartItem.Quantity = cartItem.Quantity - 1;
            }
            Db.SaveChanges();
        }
        public int GetCartCountByUserId(string user)
        {
            var users = Db.AspNetUsers.Where(x => x.UserName == user).FirstOrDefault();
           
            if(users == null)
                return 0;

            var cartItem = Db.Carts.Where(c => c.UserId == users.Id).ToList();

             return cartItem.Count();
        }

    



    }
}
