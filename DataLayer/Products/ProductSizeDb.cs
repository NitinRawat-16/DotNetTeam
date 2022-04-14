using System.Collections.Generic;
using System.Linq;
using DataModelLayer;

namespace DataLayer
{
    public  class ProductSizeDb
    {
        PortalEntities db= new PortalEntities();
        public IEnumerable<ProductSize> GetAll()
        {
            return db.ProductSizes;
        }
        public ProductSize GetById(int id)
        {
            return db.ProductSizes.SingleOrDefault(m=> m.ProductSizeId == id);
        }

        public void Insert(ProductSize productSize)
        {
            db.ProductSizes.Add(productSize);
            db.SaveChanges();
        }
        public void Edit(int id,ProductSize productSize)
        {
            var productSizeInDb = GetById(id);
           
            productSizeInDb.S = productSize.S;
            productSizeInDb.M = productSize.M;
            productSizeInDb.L = productSize.L;
            productSizeInDb.Xl = productSize.Xl;
            db.SaveChanges();
        }

        public void AddProductQuantity(int id,ProductSize productSize)
        {
            var sizeInDb = GetById(id);
            sizeInDb.S += productSize.S;
            sizeInDb.M += productSize.M;
            sizeInDb.L += productSize.L;
            sizeInDb.Xl += productSize.Xl;
            db.SaveChanges();
        }

    }
}
