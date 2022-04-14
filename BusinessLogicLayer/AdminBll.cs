using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;
using DataLayer;
namespace BusinessLogicLayer
{
    public class AdminBll
    {
        AdminDll adminDll;

        public AdminBll()
        {
            adminDll = new AdminDll();
        }


        public IEnumerable<Product> GetProducts()
        {
            return adminDll.GetProducts();
        }



        public IEnumerable<AlertProductDetails_Result> GetProductsAlert()
        {
            var products = adminDll.GetProductsAlert();
            return products;
        }


        public IEnumerable<OrderConfirmData_Result> GetOrderConfirm()
        {
            var orders = adminDll.GetOrderConfirm();
            return orders;
        }

        public void AddOrderDetails(OrderDetail orderDetail)
        {
            adminDll.AddOrderDetails(orderDetail);
        }

        public void AddOrder(Order order)
        {
            adminDll.AddOrder(order);
        }

        public void RemoveOrderConfirm(int orderConfirmedId)
        {
            adminDll.RemoveOrderConfirm(orderConfirmedId);
        }

        public void QuantitySubstractFromProduct(int productId, string Size)
        {
            adminDll.QuantitySubstractFromProduct(productId, Size);
        }
        public void CancelOrder(OrderCanceled orderCanceled)
        {

            adminDll.CancelOrder(orderCanceled);
        }

        public IEnumerable<DeliveryOrderData_Result> OrderDelivery()
        {
            return adminDll.OrderDelivery();
        }




        public void OrderDeliveryConfirm(int orderId)
        {
            adminDll.OrderDeliveryConfirm(orderId);

        }

    }
}
