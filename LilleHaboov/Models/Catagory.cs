﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LilleHaboov.Models
{
    public class Catagory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Catagory> Catagories { get; set; }

    }
}