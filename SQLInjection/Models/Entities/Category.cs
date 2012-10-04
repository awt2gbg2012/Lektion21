using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLInjection.Models.Entities
{
    public class Category
    {
        public Guid ID { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}