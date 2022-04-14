using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Users;
using DataModelLayer;
namespace BusinessLogicLayer
{
    public class WishlistBs
    {
        private WishlistDb wishBs;
        public WishlistBs() => wishBs = new WishlistDb();
        public int AddToWishList(Product product, string user)
        {
            var count = wishBs.AddToWishList(product, user);
            return count;
        }

        public IList<ProductWishlist> GetAllById(string user)
        {

            return wishBs.GetAllById(user);
        }

        public void RemoveItemById(Product product, string user)
        {
            wishBs.RemoveItemById(product, user);
        }
    }
}
