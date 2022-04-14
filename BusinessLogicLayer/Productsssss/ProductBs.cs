using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataModelLayer;

namespace BusinessLogicLayer
{
    public class ProductBs
    {
        ProductDb db= new ProductDb();
        public IEnumerable<Product> GetAll()
        {
            return db.GetAll();
        }
        public void Insert(Product product)
        {
            db.Insert(product);
        }
    }
}
