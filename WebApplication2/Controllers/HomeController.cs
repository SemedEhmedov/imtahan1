using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDBContext appDBContext;

        public HomeController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public IActionResult Index()
        {
            var users = appDBContext.Users.Include(x => x.WebSite).ToList();
            return View(users);
        }

    }
}
