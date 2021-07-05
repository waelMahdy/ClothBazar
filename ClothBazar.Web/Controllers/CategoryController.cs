using clothBazar.Services;
using ClothBazar.Entity;
using ClothBazar.Web.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ClothBazar.Web.Controllers
{
    [Authorize(Users  = "Admin@blackhat.com")]
    public class CategoryController : Controller
    {

        public ActionResult Index()
        {
            var categories = CategoryService.Instance.GetCategories();

            return View(categories);
        }

        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
           
             pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SearchTerm = search;
            var totalRecords = CategoryService.Instance.GetCategoriesCount(search);
            model.Categories = CategoryService.Instance.GetCategories(search,pageNo.Value);
            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo);
                            
                return PartialView("_CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        #region Creation

        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModels model = new NewCategoryViewModels();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModels model)
        {
            if (ModelState.IsValid)
            {
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageURL = model.ImageURL;
            newCategory.IsFeatured = model.IsFeatured;
            CategoryService.Instance.SaveCategory(newCategory);
            return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        #endregion

        #region Updation
        [HttpGet]
        public ActionResult Edit(int ID)

        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = CategoryService.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.IsFeatured = category.IsFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoryService.Instance.UpdateCategory(category);
            return RedirectToAction("CategoryTable");
        }
        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    var category = CategoryService.Instance.GetCategory(ID);


        //    return View(category);
        //}

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            //category = CategoryService.Instance.GetCategory(category.ID);
            CategoryService.Instance.DeleteCategory(ID);
            return RedirectToAction("Index");
        }
        public ActionResult GetMainGategories()
        {
            var categories = CategoryService.Instance.GetCategories();
            return PartialView(categories);
        }
        #endregion
    }

}