using Microsoft.AspNetCore.Mvc;

namespace Campaign.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
