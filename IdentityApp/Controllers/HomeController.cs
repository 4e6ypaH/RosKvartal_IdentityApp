using IdentityApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = db.Users.ToList();
            }

            return View(users);
        }

        public string GetBook()
        {
            Book b = new Book { Name = "Война и мир", Author = "Л. Толстой" };
            Book firstBook = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Books.Add(b);
                db.SaveChanges();
                firstBook = db.Books.FirstOrDefault();
            }
            if (firstBook == null)
                return "неизвестно";
            return firstBook.Name + " (" + firstBook.Author + ")";
        }

        [Authorize]
        public ActionResult GetMyRoles()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }

        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}