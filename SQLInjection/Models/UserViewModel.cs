using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLInjection.Models
{
    public class UserViewModel
    {
        public string Name { get { return "David"; } }
        public string Password { get; set; }
    }
}