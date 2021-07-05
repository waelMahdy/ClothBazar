using ClothBazar.DataBase;
using ClothBazar.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clothBazar.Services
{
    public class CategoryService
    {
        #region singelton
        public static CategoryService Instance
        {
            get
            {
                if (instance == null) instance = new CategoryService();
                return instance;
            }
        }
        private static CategoryService instance { get; set; }
        private CategoryService()
        {

        }
        #endregion
        public Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);
            }
        }
        public List<Category> GetCategories()
        {
            using (var context=new CBContext())
            {
                return context.Categories.ToList();
            }
        }
        public int GetCategoriesCount(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(p => p.Name != null && p.Name.ToLower()
                    .Contains(search.ToLower()))
                    .Count();

                }
                else
                {
                    return context.Categories.Count();

                }
               
            }
        }

        public List<Category> GetCategories(string search,int pageNo)
        {
            int pageSize = 10;
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(p => p.Name != null && p.Name.ToLower()
                    .Contains(search.ToLower()))
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Products)
                    .ToList();
                   
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();

                }
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Where(x => x.IsFeatured && x.ImageURL != null).Take(6).ToList();
            }
        }
        public void SaveCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var category = context.Categories.Find(ID);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
        //public int GetCategoriesCount(string search)
        //{
        //    using (var context = new CBContext())
        //    {
        //        if (!string.IsNullOrEmpty(search))
        //        {
        //            return context.Categories.Where(category => category.Name != null &&
        //                 category.Name.ToLower().Contains(search.ToLower())).Count();
        //        }
        //        else
        //        {
        //            return context.Categories.Count();
        //        }
        //    }
        //}
        //public List<Category> GetCategories(string search, int pageNo)
        //{
        //    int pageSize = 3;

        //    using (var context = new CBContext())
        //    {
        //        if (!string.IsNullOrEmpty(search))
        //        {
        //            return context.Categories.Where(category => category.Name != null &&
        //                 category.Name.ToLower().Contains(search.ToLower()))
        //                 .OrderBy(x => x.ID)
        //                 .Skip((pageNo - 1) * pageSize)
        //                 .Take(pageSize)
        //                 .Include(x => x.Products)
        //                 .ToList();
        //        }
        //        else
        //        {
        //            return context.Categories
        //                .OrderBy(x => x.ID)
        //                .Skip((pageNo - 1) * pageSize)
        //                .Take(pageSize)
        //                .Include(x => x.Products)
        //                .ToList();
        //        }
        //    }
        //}
    }
}
