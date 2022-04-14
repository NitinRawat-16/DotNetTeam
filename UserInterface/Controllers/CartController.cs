using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModelLayer;
using BusinessLogicLayer;

namespace UserInterface.Controllers
{

    public class CartController : Controller
    {
        private CartBs cartBs;

        public CartController()
        {
            cartBs = new CartBs();
        }
        // GET: Cart
        public ActionResult AddtoCart(Product product)
        {
            if (User.Identity.IsAuthenticated)
            {

                List<Product> products = new List<Product>();
                products.Add(product);
                var userId = User.Identity.Name;
                cartBs.MigrateToDb(products, userId);

                var count = cartBs.GetCartCountByUserId(userId);
                HttpCookie counts = new HttpCookie("Count",count.ToString());
                Response.Cookies.Add(counts);
                
            }
            else
            {
                int a;
                int.TryParse(Request.Cookies["count"].Value, out a);

                HttpCookie ProductList = new HttpCookie("ProductIds");
                System.Collections.Specialized.NameValueCollection product1 = new System.Collections.Specialized.NameValueCollection();

                for (int i = 0; i < a; i++)
                {
                    var productKey = "items_" + i;
                    var productValue = Request.Cookies["ProductIds"].Values[i];

                    product1.Add(productKey, productValue);

                }

                var name = "items_" + a;
                
                product1.Add(name, product.ProductId.ToString());
                a++;

                // var previousData= Request.Cookies["ProductIds"].Values[1];

                ProductList.Values.Add(product1);

                Response.Cookies.Add(ProductList);
                HttpCookie count = new HttpCookie("Count", a.ToString());
                Response.Cookies.Add(count);
                return RedirectToAction("ViewProducts", "Products");

            }
            return RedirectToAction("ShowProducts","Users");
        }

        //View products in  Cart
        public ActionResult ViewCart()
        {
           if(User.Identity.IsAuthenticated)
            {
             var cartItem =   cartBs.GetAllById(User.Identity.Name);
               
                if (cartItem == null)
                {
                   
                    HttpCookie counts = new HttpCookie("Count", "0");
                    Response.Cookies.Add(counts);

                    return RedirectToAction("ShowProducts", "User");
                }
                else
                {
                    var count = cartBs.GetCartCountByUserId(User.Identity.Name);
                    HttpCookie counts = new HttpCookie("Count", count.ToString());
                    Response.Cookies.Add(counts);

                    return View("ViewCartLogin",cartItem);
                }
              
            }
            else
            {
                int a;
                int.TryParse(Request.Cookies["count"].Value, out a);
                List<int> cartItem = new List<int>();
                for (int i = 0; i < a; i++)
                {
                    int values;
                    var productValue = int.TryParse(Request.Cookies["ProductIds"].Values[i], out values);
                    cartItem.Add(values);

                }

                if (cartItem == null)
                {
                    return RedirectToAction("ShowProducts", "User");
                }
                else
                {
                    var products = cartBs.GetProductsById(cartItem);
                    return View(products);
                }
            }

        }

        public ActionResult Delete(Product product)
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identity.Name;
                cartBs.RemoveCartItemByUserId(user, product);


                int count =  cartBs.GetCartCountByUserId(user);
                HttpCookie counts = new HttpCookie("Count", count.ToString());
                Response.Cookies.Add(counts);

            }
            else 
            {
                int a;
                int.TryParse(Request.Cookies["count"].Value, out a);

                HttpCookie ProductList = new HttpCookie("ProductIds");
                System.Collections.Specialized.NameValueCollection product1 = new System.Collections.Specialized.NameValueCollection();

                for (int i = 0; i < a; i++)
                {
                    var productKey = "items_" + i;
                    var productValue = Request.Cookies["ProductIds"].Values[i];
                    int productId;
                    int.TryParse(productValue, out productId);
                    if (product.ProductId == productId)
                    {

                    }
                    else
                    {
                        product1.Add(productKey, productValue);
                    }
                }
                a--;

                ProductList.Values.Add(product1);

                Response.Cookies.Add(ProductList);
                HttpCookie count = new HttpCookie("Count", a.ToString());
                Response.Cookies.Add(count);

            }

            return RedirectToAction("ViewCart");
        }
        
        public ActionResult BuyNow()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Order", "Orders");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }

}
