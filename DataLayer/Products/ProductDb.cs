using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataModelLayer;

namespace DataLayer
{
    public class ProductDb
    {
        PortalEntities db = new PortalEntities();

        public IEnumerable<Product> GetAll()
        {
            var products=db.Products
                .Include(m=> m.ProductSize)
                .Include(m=> m.ProductCategory)
                .Include(m=>m.OrderConfirmeds);
            return products;
        }

        public void Insert(Product product)
        {
            db.Products.Add(product);
            Save();
        }

        public Product GetById(int id)
        {
            var product = db.Products.SingleOrDefault(x=> x.ProductId==id);
            return product;
        }
        public void Edit(Product product, int productQuantityAvailable)
        {
            var productInDb = GetById(product.ProductId);

            productInDb.ProductName = product.ProductName;
            productInDb.ProductDescription = product.ProductDescription;
            productInDb.ProductCategoryId = product.ProductCategoryId;
            productInDb.ProductAddedDate = product.ProductAddedDate;
            productInDb.ProductPrice = product.ProductPrice;
            productInDb.ProductAdminNotification = product.ProductAdminNotification;
            productInDb.ProductAvailability += productQuantityAvailable;

            productInDb.TotalProductAdded += productQuantityAvailable;

            Save();
        }

        public void AddProductQuantity(int quantity,int id)
        {
            var product = GetById(id);
            product.ProductAvailability += quantity;
            product.TotalProductAdded += quantity;
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
