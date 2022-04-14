using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;
using DataLayer;

namespace BusinessLogicLayer
{
    public class ProductSizeBs
    {
        ProductSizeDb db= new ProductSizeDb();
        public void Insert(ProductSize productSize)
        {
            db.Insert(productSize);
        }
        public IEnumerable<ProductSize> GetAll()
        {
            return db.GetAll();
        }

        public ProductSize GetById(int id)
        {
            return db.GetById(id);
        }
        public void Edit(int id,ProductSize productSize)
        {
            db.Edit(id,productSize);
        }
        public void AddProductQuantity(int id,ProductSize productSize)
        {
            db.AddProductQuantity(id,productSize);
        }
    }
}
