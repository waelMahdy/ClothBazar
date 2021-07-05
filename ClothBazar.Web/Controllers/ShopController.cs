using clothBazar.Services;
using ClothBazar.Entity;
using ClothBazar.Web.Code;
using ClothBazar.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

 
  
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();
            ShopViewModel model = new ShopViewModel();
            model.FeaturedCategories = CategoryService.Instance.GetFeaturedCategories();
            model.Categories = CategoryService.Instance.GetCategories();
            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SearchTerm = searchTerm;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;
            model.products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.pager = new Pager(totalCount, pageNo, pageSize);
            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.ShopPageSize();
            FilterProductsViewModel model = new FilterProductsViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SearchTerm = searchTerm;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;
            model.products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);
            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            model.pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }

        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var CartProductCookie = Request.Cookies["CartProducts"];
            if (CartProductCookie != null && !string.IsNullOrEmpty(CartProductCookie.Value))
            {
                //var productIDs = CartProductCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();
                model.CartProductsIDs = CartProductCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductsIDs);
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);

        }
        public JsonResult PlaceOrder(string productsIDs)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(productsIDs))
            {
            var productQuantities = productsIDs.Split('-').Select(x => int.Parse(x)).ToList();
            var boughtProducts = ProductsService.Instance.GetProducts(productQuantities.Distinct().ToList());

            Order newOrder = new Order();
            newOrder.UserID = User.Identity.GetUserId();
            newOrder.OrderAt = DateTime.Now;
            newOrder.Status = "Pending";
            newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.ID).Count());
            newOrder.OrderItems = new List<OrderItem>();
            newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID,Quantity= productQuantities.Where(productID => productID == x.ID).Count() }));
            var rowsEffected = ShopService.Instance.SaveOrder(newOrder);
            result.Data = new { Success = true,Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }
    }
}