using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magic_Books_Project.Models
{
    public class Asset
    {
        public Product prdct { get; set; }
        public byte quantity { get; set; }
    }
}