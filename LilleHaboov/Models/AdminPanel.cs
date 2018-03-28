using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LilleHaboov.Models
{
    public class AdminPanel
    {
        public int Id { get; set; }

        public string UserID { get; set; }

        public int ProductsInRow { get; set; }
        public virtual ICollection<ProductImage> CarouselImages { get; set; }
    }
}