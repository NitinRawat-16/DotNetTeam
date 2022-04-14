using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataModelLayer;

namespace BusinessLogicLayer
{
    public class ProductCategoryBs
    {
        ProductCategoryDb db = new ProductCategoryDb();
        public IEnumerable<ProductCategory> GetAll()
        {
            return db.GetAll();
        }
    }
}
