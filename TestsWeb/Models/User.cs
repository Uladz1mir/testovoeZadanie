using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestsWeb.Models
{
    public class User
    {
        public int userId { get; set; }
        public string login { get; set; }
        private string passw;
        public string password
        {
            get
            {
                return passw;
            }

            set
            {
                passw = Controllers.HomeController.HashhCode(value);
            }
        }

    }
}