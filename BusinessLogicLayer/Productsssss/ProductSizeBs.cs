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
    }
}
