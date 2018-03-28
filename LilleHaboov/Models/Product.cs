using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LilleHaboov.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }
        public virtual ICollection<Catagory> Catagories { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }
    }
}