using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModelLayer;
using BusinessLogicLayer;
using UserInterface.ViewModel;

namespace UserInterface.Controllers
{
    [Authorize(Roles ="CanManagePortal")]
    public class AdminsController : Controller
    {

        AdminBll adminBll;
        public AdminsController()
        {
            adminBll = new AdminBll();
        }

        // GET: Admins
        public ActionResult Home()
        {

            return View(adminBll.GetProducts());
        }

        public ActionResult ProductList()
        {
            return View(adminBll.GetProducts());
        }

        public ActionResult ProductAlert()
        {
            return View(adminBll.GetProductsAlert());
        }

        public ActionResult Orders()
        {
            return View(adminBll.GetOrderConfirm());

        }
        public ActionResult OrderConfirm(OrderConfirmData_Result order)
        {
            order.ExpectedDATE = DateTime.Now.Date;
            return View(order);
        }


        public ActionResult OrderEntry(OrderConfirmData_Result order)
        {
            if (!ModelState.IsValid)
            {
                return View("OrderConfirm", order);
            }

            OrderDetail orderDetail = new OrderDetail()
            {
                CustomerName = order.CustomerName,
                DeliveryAddress = order.CustomerAddress,
                CustomerMobile = order.CustomerMobile,
                OrderDate = order.OrderDate,
                OrderConfirmedDate = DateTime.Now.Date,
                PaymentMode = order.PaymentMode,
                OrderDeliveryDate = null,
                OrderExcpectedDelivery = order.ExpectedDATE,
                Size = order.Size
            };
            adminBll.AddOrderDetails(orderDetail);

            Order orderTable = new Order()
            {
                UserId = order.CustomerId,
                ProductId = (int)order.ProductId,
                OrderDetailsId = orderDetail.OrderDetailsId
            };

            adminBll.AddOrder(orderTable);
            adminBll.RemoveOrderConfirm(order.OrderConfirmedId);

            adminBll.QuantitySubstractFromProduct((int)order.ProductId, order.Size);

            return RedirectToAction("Orders");




        }

        public ActionResult OrderCancel(OrderConfirmData_Result order)
        {
            OrderConfirmed orderConfirm = new OrderConfirmed()
            {
                OrderConfirmedId = order.OrderConfirmedId,
                ProductId = (int)order.ProductId,
                CustomerId = order.CustomerId,
                CustomerName = order.CustomerName,
                CustomerAddress = order.CustomerAddress,
                CustomerMobile = order.CustomerMobile,
                PaymentMode = order.PaymentMode,
                OrderDate = order.OrderDate,
                Size = order.Size
            };


            return View(orderConfirm);

        }

        public ActionResult OrderCancelEntry(OrderConfirmed orderConfirmed)
        {
            if (!ModelState.IsValid)
            {
                return View("OrderCancel");
            }
            OrderCanceled orderCanceled = new OrderCanceled()
            {
                ProductId = (int)orderConfirmed.ProductId,
                CustomerId = orderConfirmed.CustomerId,
                CustomerName = orderConfirmed.CustomerName,
                CustomerAddress = orderConfirmed.CustomerAddress,
                CustomerMobile = orderConfirmed.CustomerMobile,
                PaymentMode = orderConfirmed.PaymentMode,
                OrderCancelReason = orderConfirmed.OrderCancelReason,
                OrderDate = orderConfirmed.OrderDate,
                Size = orderConfirmed.Size
            };

            adminBll.CancelOrder(orderCanceled);
            adminBll.RemoveOrderConfirm(orderConfirmed.OrderConfirmedId);

            return RedirectToAction("Orders");
        }

        public ActionResult OrderDelivery()
        {
            return View(adminBll.OrderDelivery());
        }
        public ActionResult OrderDeliveredConfirm(DeliveryOrderData_Result order)
        {
            adminBll.OrderDeliveryConfirm(order.OrderId);
            return RedirectToAction("OrderDelivery");

        }
    }
}