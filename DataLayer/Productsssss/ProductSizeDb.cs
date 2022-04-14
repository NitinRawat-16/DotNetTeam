using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;

namespace DataLayer
{
    public  class ProductSizeDb
    {
        readonly PortalEntities _db;
        public ProductSizeDb()
        {
            _db = new PortalEntities();
        }

        public IEnumerable<ProductSize> GetAll()
        {
            return _db.ProductSizes;
        }

        public void Insert(ProductSize productSize)
        {
            _db.ProductSizes.Add(productSize);
            _db.SaveChanges();
        }
    }
}
