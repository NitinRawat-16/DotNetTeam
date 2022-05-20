using UserInterface.Models;
using System.Web.Mvc;
using System;
using Razorpay.Api;
using System.Collections.Generic;
using DataModelLayer;

namespace UserInterface.Controllers
{
    [Authorize(Roles ="User")]
    public class PaymentsController : Controller
    {
        private const string _Key = "rzp_test_jwn0B9TkI5barg";
        private const string _secret = "MgA7FVvdyYdItsQSbGRdSycX";

        // GET: Payments
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(PaymentInitial payment)
        {
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(_Key, _secret);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Amount * 100);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
                                                 //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();

            // Create order model for return on view
            PaymentOrderModel orderModel = new PaymentOrderModel
            {
                orderId = orderResponse.Attributes["id"],
                razorPayKey = _Key,
                amount = payment.Amount * 100,
                currency = "INR",
                name = payment.CName,
                email = payment.Email,
                contactNumber = payment.ContactNumber,
                address = payment.Address,
                description = "Testing description"
            };

            // Return on PaymentPage with Order data
            return View("PaymentPage", orderModel);
        }

        [HttpPost]
        public ActionResult Complete()
        {
            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = Request.Params["rzp_paymentid"];

            // This is orderId
            string orderId = Request.Params["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(_Key, _secret);

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                // Create these action method
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Failed()
        {
            return View();
        }
    }
}