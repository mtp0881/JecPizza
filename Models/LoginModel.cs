using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JecPizza.Models
{
    public class LoginModel
    {
        public string id { get; set; }
        public string passWord { get; set; }
        public string message { get; set; }

        //public char isLogin { get; set; }
    }
}