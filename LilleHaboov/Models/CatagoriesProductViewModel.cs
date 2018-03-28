using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LilleHaboov.Models
{
    public class CatagoriesProductViewModel
    {
        public IEnumerable<Catagory> Catagories { get; set; }
        public Product Product { get; set; }
    }
}