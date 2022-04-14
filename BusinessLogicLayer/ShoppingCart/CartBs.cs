using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataModelLayer;

namespace BusinessLogicLayer
{
    public class CartBs
    {
        CartDb cartDb = new CartDb();


        public IEnumerable<Product> GetProductsById(List<int> productIds)
        {
            return cartDb.GetProductsById(productIds);
        }

        public void MigrateToDb(List<Product> products, string userName)
        {
            cartDb.MigrateToDb(products, userName);
        }
        public IList<Cart> GetAllById(string user)
        {

            return cartDb.GetAllById(user);
        }
        public void RemoveCartItemByUserId(string user, Product product)
        {
            cartDb.RemoveCartItemByUserId(user, product);
        }

        public int GetCartCountByUserId(string user)
        {
            return cartDb.GetCartCountByUserId(user);
        }


        public void MigrateToDbById(List<int> productIds, string userName)
        {
            cartDb.MigrateToDbById(productIds, userName);
        }
    }
}
