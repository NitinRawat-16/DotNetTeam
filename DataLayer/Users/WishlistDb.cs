using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;
using System.Data.Entity;
using DataLayer;

namespace DataLayer.Users
{
    public class WishlistDb
    {

        private PortalEntities wishDb;
        public WishlistDb() => wishDb = new PortalEntities();

        public int AddToWishList(Product product, string user)
        {
            var users = wishDb.AspNetUsers.Where(u => u.UserName == user).First();
            IQueryable<ProductWishlist> wishlistItem = wishDb.ProductWishlists.Where(w => w.UserId == users.Id && w.ProductId == product.ProductId);

            if (wishlistItem.Count() == 0)
            {
                var wishList = new ProductWishlist()
                {
                    UserId = users.Id,
                    ProductId = product.ProductId
                };
                wishDb.ProductWishlists.Add(wishList);
                wishDb.SaveChanges();
            }
            return wishlistItem.Count();
        }

        public IList<ProductWishlist> GetAllById(string user)
        {
            List<Product> products = new List<Product>();
            var users = wishDb.AspNetUsers.Where(x => x.UserName == user).FirstOrDefault();
            var wishlist = wishDb.ProductWishlists.Include(c => c.Product).Where(c => c.UserId == users.Id).ToList();
            foreach (var cartItem in wishlist)
            {
                products.Add(cartItem.Product);
            }
            return wishlist;
        }

        public void RemoveItemById(Product product, string user)
        {
            var users = wishDb.AspNetUsers.Where(x => x.UserName == user).First();
            var wishlist = wishDb.ProductWishlists.Where(c => c.UserId == users.Id && c.ProductId == product.ProductId).First();
            wishDb.ProductWishlists.Remove(wishlist);
            wishDb.SaveChanges();
        }
    }
}