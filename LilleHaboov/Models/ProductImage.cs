using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LilleHaboov.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public virtual Product Product { get; set; }
    }
}