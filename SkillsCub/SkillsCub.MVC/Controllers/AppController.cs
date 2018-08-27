using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkillsCub.MVC.Controllers
{
    public class AppController : Controller
    {

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
    }
}