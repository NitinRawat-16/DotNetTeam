using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace UserInterface.Controllers
{
    public class CookiesController : Controller
    {
        private CartBs cartBs;
        public CookiesController() => cartBs = new CartBs();
        // GET: Cookies
        public ActionResult Index()
        {
            HttpCookie Users = new HttpCookie("UserID", null);
            HttpCookie Product = new HttpCookie("ProductIds", null);
            if (User.Identity.IsAuthenticated)
            {
                var counts = cartBs.GetCartCountByUserId(User.Identity.Name);
                HttpCookie count = new HttpCookie("Count", counts.ToString());
                Response.Cookies.Add(count);
            }
            else
            {
                HttpCookie count = new HttpCookie("Count", "0");
                Response.Cookies.Add(count);
            }
            Response.Cookies.Add(Users);
            Response.Cookies.Add(Product);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddToUserCart()
        {
            if (Request.Cookies["Count"].Value == "0")
            {
                HttpCookie Users = new HttpCookie("UserID", null);
                HttpCookie Product = new HttpCookie("ProductIds", null);
                HttpCookie count = new HttpCookie("Count", "0");
                Response.Cookies.Add(count);
                Response.Cookies.Add(Users);
                Response.Cookies.Add(Product);
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
                var products = cartBs.GetProductsById(cartItem).ToList();
                cartBs.MigrateToDb(products, User.Identity.Name);

                HttpCookie count = new HttpCookie("Count", "0");
                Response.Cookies.Add(count);
            }
            return RedirectToAction("Index");
        }

    }
}