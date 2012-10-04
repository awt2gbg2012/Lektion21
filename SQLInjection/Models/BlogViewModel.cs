using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLInjection.Models
{
    public class BlogViewModel
    {
        public List<string> Comments { get; set; }
        public string Comment { get; set; }
    }
}