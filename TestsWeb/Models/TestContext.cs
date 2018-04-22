using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestsWeb.Models
{
    public class TestContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Computer> Computers { get; set; }
    }

    public class TestDbInitialazer : DropCreateDatabaseAlways<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            context.Users.Add(new User { login = "Ivan", password = "12a" });
            context.Users.Add(new User { login = "Yan", password = "23b" });
            context.Users.Add(new User { login = "Johann", password = "34c" });
            context.Users.Add(new User { login = "Jon", password = "45d" });
            context.Users.Add(new User { login = "Jan", password = "56e" });

            context.Computers.Add(new Computer { mac_adress = "00-28-F3-E1-0E-10", ip_adress = 19216801, user_name = "Alexey", os_version = "Windows 98" });
            context.Computers.Add(new Computer { mac_adress = "00-18-F7-E3-0F-02", ip_adress = 19216802, user_name = "Grisha", os_version = "Linux" });
            context.Computers.Add(new Computer { mac_adress = "01-12-A3-B1-0A-02", ip_adress = 19216803, user_name = "Petr", os_version = "Android" });
            context.Computers.Add(new Computer { mac_adress = "00-38-E3-F1-0B-03", ip_adress = 19216804, user_name = "Sergey", os_version = "Windows 10 Mobile" });
            context.Computers.Add(new Computer { mac_adress = "05-13-F4-E2-0C-01", ip_adress = 19216805, user_name = "Vitya", os_version = "MS-DOS" });
        }
    }
}