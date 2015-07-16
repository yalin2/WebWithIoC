using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWithIoC.Models
{
    public class HomeIndexViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
}