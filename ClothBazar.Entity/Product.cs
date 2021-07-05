using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entity
{
    public class Product : BaseEntity
    {
        [Range(1,10000)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public string ImageURL { get; set; }
        public string ImageURL1 { get; set; }
        public string ImageURL2 { get; set; }

    }
}
