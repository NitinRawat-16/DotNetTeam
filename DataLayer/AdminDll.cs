
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataModelLayer;

namespace DataLayer
{
    public class AdminDll
    {
        PortalEntities _context;
        public AdminDll()
        {
            _context = new PortalEntities();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.Include(p => p.ProductCategory).Include(p => p.ProductSize).OrderBy(P => P.ProductName).ToList();
        }


        public IEnumerable<AlertProductDetails_Result> GetProductsAlert()
        {
            var products = _context.AlertProductDetails().ToList();
            return products;
        }


        public IEnumerable<OrderConfirmData_Result> GetOrderConfirm()
        {
            var orders = _context.OrderConfirmData().ToList();
            return orders;
        }


        public void AddOrderDetails(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void RemoveOrderConfirm(int orderConfirmedId)
        {
            var order = _context.OrderConfirmeds.Where(p => p.OrderConfirmedId == orderConfirmedId).ToList();
            _context.OrderConfirmeds.Remove(order[0]);
            _context.SaveChanges();
        }

        public void QuantitySubstractFromProduct(int productId, string Size)
        {
            var product = _context.Products.Where(p => p.ProductId == productId).First();
            var productSizeId = product.ProductSizeId;
            var productsize = _context.ProductSizes.Where(P => P.ProductSizeId == productSizeId).First();
            var Sizes = Size.ToUpper();
            if (Sizes.Equals("S"))
            {
                productsize.S = productsize.S - 1;
            }
            else if (Sizes.Equals("M"))
            {
                productsize.M = productsize.M - 1;
            }
            else if (Sizes.Equals("L"))
            {
                productsize.L = productsize.L - 1;
            }
            else if (Sizes.Equals("xL"))
            {
                productsize.Xl = productsize.Xl - 1;
            }

            product.ProductAvailability = product.ProductAvailability - 1;
            _context.SaveChanges();


        }




        public void CancelOrder(OrderCanceled orderCanceled)
        {

            _context.OrderCanceleds.Add(orderCanceled);
            _context.SaveChanges();

        }


        public IEnumerable<DeliveryOrderData_Result> OrderDelivery()
        {
            return _context.DeliveryOrderData().ToList();
        }



        public void OrderDeliveryConfirm(int orderId)
        {

            var order = _context.Orders.Where(o => o.OrderId == orderId).ToList();
            var OrderDetailId = order[0].OrderDetailsId;
            var orderDetail = _context.OrderDetails.Where(o => o.OrderDetailsId == OrderDetailId).ToList();
            orderDetail[0].OrderDeliveryDate = DateTime.Now.Date;
            _context.SaveChanges();



        }

    }
}
