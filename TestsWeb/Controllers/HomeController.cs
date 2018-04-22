using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using TestsWeb.Models;

namespace TestsWeb.Controllers
{
    public class HomeController : Controller
    {
        TestContext db = new TestContext();

        public ActionResult Index()
        {
            SelectList slist = new SelectList(db.Computers, "id", "user_name");
            ViewBag.Slist = slist;

            return View();
        }

        [HttpPost]
        public ActionResult GetComp(string name)
        {
            int id = Convert.ToInt32(name);
            Computer c = db.Computers.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Computer comp)
        {
            db.Computers.Add(comp);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (TestContext db = new TestContext())
                {
                    var ph = HomeController.HashhCode(model.Password);
                    user = db.Users.FirstOrDefault(u => u.login == model.Name && u.password == ph);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (TestContext db = new TestContext())
                {
                    user = db.Users.FirstOrDefault(u => u.login == model.Name);
                }
                if (user == null)
                {
                    using (TestContext db = new TestContext())
                    {
                        var ph = HomeController.HashhCode(model.Password);
                        db.Users.Add(new User { login = model.Name, password = model.Password });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.login == model.Name && u.password == ph).FirstOrDefault();
                    }

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public static string HashhCode(string myDataEncoded)
        {
            try
            {
                SHA1 sha = SHA1.Create();
                byte[] bytes = new ASCIIEncoding().GetBytes(myDataEncoded);
                sha.ComputeHash(bytes);
                myDataEncoded = Convert.ToBase64String(sha.Hash);
            }
            catch (Exception exception)
            {
                string text1 = "Error in HashCode : " + exception.Message;
            }
            return myDataEncoded;
        }

    }
}