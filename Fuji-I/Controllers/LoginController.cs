using Microsoft.AspNetCore.Mvc;

namespace Fuji_I.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
