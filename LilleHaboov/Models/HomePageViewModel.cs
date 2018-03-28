using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LilleHaboov.Models
{
    public class HomePageViewModel
    {

        public AdminPanel AdminPanel { get; set; }
        public ICollection<Catagory> Catagories { get; set; }
    }
}