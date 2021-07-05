using clothBazar.Services;
using ClothBazar.Entity;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    [Authorize(Users = "Admin@blackhat.com")]
    public class ProductController : Controller

    {
      
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;
            pageNo = pageNo.HasValue ? pageNo.Value>0? pageNo.Value : 1:1;
            //Similar to above code
            //if (pageNo.HasValue)
            //{
            //    if (pageNo > 0)
            //    {
            //        model.PageNo = pageNo.Value;
            //    }
            //    else
            //    {
            //        model.PageNo = 1;
            //    }

            //}
            //else
            //{
            //    model.PageNo = 1;
            //}
           
            var totalRecords = ProductsService.Instance.GetProductsCount(search);
            model.Products = ProductsService.Instance.GetProducts(search, pageNo.Value, pageSize);
            model.Pager = new Pager(totalRecords, pageNo, pageSize);
            return PartialView(model);
        }
        #region Creation
        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

           model.AvailableCategories = CategoryService.Instance.GetCategories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            
                var newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Price = model.Price;
                newProduct.Category = CategoryService.Instance.GetCategory(model.CategoryID);
                newProduct.ImageURL = model.ImageURL;
                newProduct.ImageURL1 = model.ImageURL1;
            newProduct.ImageURL2 = model.ImageURL2;
            ProductsService.Instance.SaveProduct(newProduct);
                return RedirectToAction("productTable");
        
        }
        #endregion

        #region Updation
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();
            var product = ProductsService.Instance.GetProduct(ID);
            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
          
            model.CategoryID = product.CategoryID;
            model.ImageURL = product.ImageURL;
            model.ImageURL1 = product.ImageURL1;
            model.ImageURL2 = product.ImageURL2;
            model.AvailableCategories = CategoryService.Instance.GetCategories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsService.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            //existingProduct.Category = CategoryService.Instance.GetCategory(model.CategoryID);
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }
            if (!string.IsNullOrEmpty(model.ImageURL1))
            {
                existingProduct.ImageURL1 = model.ImageURL1;
            }
            if (!string.IsNullOrEmpty(model.ImageURL2))
            {
                existingProduct.ImageURL2 = model.ImageURL2;
            }

            ProductsService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);
            return RedirectToAction("productTable");
        }
        #endregion
        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();
            var catID = Convert.ToInt32(ProductsService.Instance.GetProduct(ID).CategoryID);
            model.Product = ProductsService.Instance.GetProduct(ID);
          
            model.Product.Category = CategoryService.Instance.GetCategory(catID);


            return View(model);
        }

    }
}