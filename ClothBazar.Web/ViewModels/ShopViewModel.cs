using ClothBazar.Entity;
using ClothBazar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductsIDs { get; set; }
        public ApplicationUser User{ get; set; }

}
    public class ShopViewModel
    {
        public List<Product> products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public List<Category>Categories { get; set; }
        public int MaximumPrice { get; set; }
        public int? SortBy { get;  set; }
        public int? CategoryID { get; set; }
        public Pager pager { get; set; }
        public string SearchTerm { get; set; }
    }
    public class FilterProductsViewModel
    {
        public List<Product> products { get; set; }
      
        public Pager pager { get; set; }
        public int? CategoryID { get; set; }
        public int? SortBy { get; set; }
        public string SearchTerm { get; set; }
    }

}