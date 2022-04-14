using System.Collections.Generic;
using DataModelLayer;

namespace DataLayer
{
    public class ProductCategoryDb
    {
        readonly PortalEntities _db;
        public ProductCategoryDb()
        {
            _db = new PortalEntities();
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _db.ProductCategories;
        }
    }
}
