using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;

namespace DataLayer
{
    public  class ProductCategoryDb
    {
        private readonly PortalEntities _db;
        public ProductCategoryDb()
        {
            _db = new PortalEntities();
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _db.ProductCategories;
        }

        public ProductCategory GetById(int id)
        {
            return _db.ProductCategories.SingleOrDefault(m => m.ProductCategoryId == id);
        }
    }
}
