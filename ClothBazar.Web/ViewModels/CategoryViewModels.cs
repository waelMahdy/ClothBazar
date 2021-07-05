using ClothBazar.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }
        
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
    public class NewCategoryViewModels
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
    }
    public class EditCategoryViewModel
    {
        public int ID { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageURL { get; set; }

        public bool IsFeatured { get; set; }
    }
}