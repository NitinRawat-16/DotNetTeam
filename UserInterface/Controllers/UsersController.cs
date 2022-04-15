using System.Web.Mvc;
using System.Collections.Generic;
using DataModelLayer;
using BusinessLogicLayer.Users;
using UserInterface.ViewModel;
using System.Security.Claims;
using System.Web;
using BusinessLogicLayer;

namespace UserInterface.Controllers
{
    [Authorize(Roles = "User")]
    public class UsersController : Controller
    {
        private readonly UserBs _userBs;
        private readonly CartBs cartBs;
        public UsersController()
        {
            cartBs = new CartBs();
            _userBs = new UserBs();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // Get All Products
        public ActionResult ShowProducts()
        {
            var products = new ViewProductsViewModel
            {
                Products = _userBs.ShowAllProducts()
            };
            return View(products);
        }

        // Show Wishlist
        public ActionResult Wishlist()
        {
            return View();
        }


        // Buy Now When Items Added In Cart
        public  ActionResult BuyNows(DeliveryAddressesViewModel deliveryAddressesViewModel)
        {
            deliveryAddressesViewModel.Carts = _userBs.GetCartItemsByUser(User.Identity.Name);
            return View(deliveryAddressesViewModel);
        }
        
        // Direct Buy Now from Products View
        public ActionResult BuyNow(Product product)
        {
                List<Product> products = new List<Product>();
                products.Add(product);
                var userId = User.Identity.Name;
                cartBs.MigrateToDb(products, userId);

                var count = cartBs.GetCartCountByUserId(userId);
                HttpCookie counts = new HttpCookie("Count", count.ToString());
                Response.Cookies.Add(counts);

                return RedirectToAction("ViewCart", "Cart");

        }

        // Show Add Address Form
        public ActionResult AddDeliveryAddress()
        {
            return View();
        }

        // Add New Address In Customer Address Table
        public ActionResult AddAddress(DeliveryAddress deliveryAddress)
        {
            var userName = User.Identity.Name;
            _userBs.AddAddress(deliveryAddress, userName);
            return RedirectToAction("SelectAddress");
        }

        // View All Orders
        public ActionResult Orders()
        {
            var userName = User.Identity.Name;
            OrderTableViewModel data = new OrderTableViewModel()
            {
                ProductPending = _userBs.GetPendingOrder(userName),
                ProductConfirm = _userBs.GetConfirmedOrder(userName),
                ProductCancel = _userBs.GetcancelOrder(userName),
                ProductSuccessDelivered = _userBs.GetDeliveredOrder(userName)
            };
            return View(data);
        }

        // Select Address Form Saved Addresses
        public ActionResult SelectAddress()
        {
            var user = User.Identity.Name;
            var deliveryAddresses = _userBs.GetDeliveryAddresses(user);
            var viewModel = new DeliveryAddressesViewModel
            {
                DeliveryAddress = deliveryAddresses,
            };
            return View(viewModel);
        }

        // After Adding Products In Cart Confirm Order
        public ActionResult OrderConfirm(DeliveryAddressesViewModel deliveryAddressesViewModel)
        {
            var CurrentUser=User.Identity.Name;
            var UserData=_userBs.GetUserById(CurrentUser);
            var AddressData = _userBs.GetAddressById(deliveryAddressesViewModel.DeliveryAddressSelect);
            var UserAddress = AddressData[1];
            var UserMobile=AddressData[0];
            var cartItem = _userBs.GetCartItemsByUser(CurrentUser);
            List<OrderConfirmed> orderConfirmeds = new List<OrderConfirmed>();
            foreach (var item in cartItem)
            {
                for(int i = 0; i < item.Quantity; i++)
                {
                    OrderConfirmed order = new OrderConfirmed()
                    {
                        ProductId = item.ProductId,
                        CustomerId = UserData.Id,
                        CustomerName=deliveryAddressesViewModel.Name,
                        CustomerAddress=UserAddress,
                        CustomerMobile=UserMobile,
                        PaymentMode="Online",
                        OrderStatus="Pending",
                        OrderDate=System.DateTime.Now.Date,
                        Size="L"
                    };
                    orderConfirmeds.Add(order);
                }
            }
            _userBs.OrderConfirmedByList(orderConfirmeds);
            _userBs.RemoveFromCart(CurrentUser);

            HttpCookie count = new HttpCookie("Count", "0");
            Response.Cookies.Add(count);
            return RedirectToAction("Orders");
        }
    }
}