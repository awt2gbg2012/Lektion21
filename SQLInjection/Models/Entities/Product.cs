using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLInjection.Models.Entities
{
    public class Product
    {
        public Guid ID { get; set; }
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }
    }
}