using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;

namespace DataLayer
{
    public class ProductDb
    {
        
        readonly PortalEntities _db;
        public ProductDb()
        {
            _db = new PortalEntities();
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public void Insert(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }
    }
}
