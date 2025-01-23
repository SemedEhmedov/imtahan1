using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Manage.Controllers
{
    public class DashBoard : ManageBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
