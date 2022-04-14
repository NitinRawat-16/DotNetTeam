using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer;
using UserInterface.ViewModel;

namespace UserInterface.Controllers
{

    public class ProductsController : Controller
    {
        // GET: Products
        ProductCategoryBs productCategory = new ProductCategoryBs();
        ProductBs product = new ProductBs();
        ProductSizeBs productSize = new ProductSizeBs();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="CanManagePortal")]
        public ActionResult AddProduct()
        {
            var categoryViewModel = new ProductViewModel
            {
                productCategories = productCategory.GetAll().ToList(),
                product = new DataModelLayer.Product()
            };
            return View(categoryViewModel);
        }


        [HttpPost]
        public ActionResult SaveOrEdit(ProductViewModel productViewModel)
        {
            var ViewModel = new ProductViewModel
            {
                product = productViewModel.product,
                productSize = productViewModel.productSize,

            };


            if (productViewModel.product.ProductId == 0)
            {
                if (!ModelState.IsValid)
                {
                    var categoryViewModel = new ProductViewModel
                    {
                        productCategories = productCategory.GetAll().ToList()
                    };
                    return View("AddProduct", categoryViewModel);
                }
                if (productViewModel.imageUrl != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(productViewModel.imageUrl.FileName);
                    string extension = Path.GetExtension(productViewModel.imageUrl.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                    productViewModel.product.ProductImage = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    productViewModel.imageUrl.SaveAs(fileName);
                }

                productSize.Insert(ViewModel.productSize);
                productViewModel.product.ProductSizeId = ViewModel.productSize.ProductSizeId;
                productViewModel.product.TotalProductAdded = ViewModel.productSize.L
                    + ViewModel.productSize.S
                    + ViewModel.productSize.M
                    + ViewModel.productSize.Xl;
                productViewModel.product.ProductAvailability = ViewModel.productSize.L
                    + ViewModel.productSize.S
                    + ViewModel.productSize.M
                    + ViewModel.productSize.Xl;
                productViewModel.product.ProductAddedDate = DateTime.Now.Date;
                product.Insert(ViewModel.product);

            }

            else
            {
                var productSizeById = productSize.GetById(productViewModel.product.ProductSizeId);

                var productQuantityAvailable = (ViewModel.productSize.S - productSizeById.S)
                                             + (ViewModel.productSize.M - productSizeById.M)
                                             + (ViewModel.productSize.L - productSizeById.L)
                                             + (ViewModel.productSize.Xl - productSizeById.Xl);
                productSize.Edit(productSizeById.ProductSizeId, ViewModel.productSize);

                product.Edit(ViewModel.product, productQuantityAvailable);

                if (productViewModel.imageUrl != null)
                {
                    var productInDb = product.GetById(productViewModel.product.ProductSizeId);
                    string fileName = Path.GetFileNameWithoutExtension(productViewModel.imageUrl.FileName);
                    string extension = Path.GetExtension(productViewModel.imageUrl.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                    productInDb.ProductImage = "~/Images/" + fileName;
                    product.Save();
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    productViewModel.imageUrl.SaveAs(fileName);

                }
            }


            return RedirectToAction("ProductList", "Admins");
        }

        public ActionResult Edit(int id)
        {
            var productById = product.GetById(id);

            var categoryViewModel = new ProductViewModel
            {
                productCategories = productCategory.GetAll().ToList(),
                product = productById,
                productSize = productSize.GetById(productById.ProductSizeId)
            };

            return View("EditProduct", categoryViewModel);
        }

        public ActionResult ViewProducts()
        {
            var productsVm = new ViewProductsViewModel
            {
                Products = product.GetAll().ToList()
            };

            return View(productsVm);
        }
    

        public ActionResult AddProductQuantity(int id )
        {
            var productDetail = product.GetById(id);
            var viewModel = new ProductViewModel
            {
                product = productDetail
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveProductQuantity(ProductViewModel productViewModel)
        {
            productSize.AddProductQuantity(productViewModel.product.ProductSizeId, productViewModel.productSize);
            var quantity = productViewModel.productSize.S
                         + productViewModel.productSize.M
                         + productViewModel.productSize.L
                         + productViewModel.productSize.Xl;
            product.AddProductQuantity(quantity, productViewModel.product.ProductId);

          return RedirectToAction("ProductList", "Admins");
        }

        public ActionResult Views()
        {
            return View();
        }
    }
}