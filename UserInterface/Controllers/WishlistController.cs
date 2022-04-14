using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using DataModelLayer;

namespace UserInterface.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist

        private readonly WishlistBs _Db;

        public WishlistController() => _Db = new WishlistBs();
        public ActionResult AddToWishList(Product product)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity.Name;
                var count = _Db.AddToWishList(product, user);

                if (count != 0)
                {
                    ViewBag.Message = "This Product is already in your wishlist";
                    return RedirectToAction("ShowProducts", "Users");
                }
                else
                {
                    ViewBag.Message = "PRoduct added to wishlist";

                    return RedirectToAction("ShowProducts", "Users");
                }

            }
            else
            {
                return RedirectToAction("Login", "Account"); ;
            }
        }


        //View Wishlist


        public ActionResult ViewWishlist()
        {
            var wishlist = _Db.GetAllById(User.Identity.Name);
            if (wishlist == null)
            {
                TempData["wishlist"] = "Wishlist is Empty";
                return RedirectToAction("ShowProducts", "Users");
            }
            else
            {
                return View("ViewWishlist", wishlist);
            }

        }


        //Remove Item from wishlist

        public ActionResult RemoveItemById(Product product)
        {
            _Db.RemoveItemById(product, User.Identity.Name);
            return RedirectToAction("ViewWishlist");
        }
    }
}