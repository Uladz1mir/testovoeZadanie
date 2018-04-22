using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestsWeb.Models
{
    public class Computer
    {
        public int id { get; set; }
        public string mac_adress { get; set; }
        public int ip_adress { get; set; }
        public string user_name { get; set; }
        public string os_version { get; set; }
    }
}