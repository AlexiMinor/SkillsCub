using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.MVC.ViewModels;
using SkillsCub.TelegramLogger;

namespace SkillsCub.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITelegramLogger _telegramLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IConfiguration _configuration;
        public HomeController(ITelegramLogger emailSender, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _telegramLogger = emailSender;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                return RedirectToAction("Index", (await _userManager.GetRolesAsync(user)).FirstOrDefault());
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Init(int initWord)
        {
            if (initWord == 0)
            {
                try
                {
                    var user = new ApplicationUser()
                    {
                        Id = Guid.Empty.ToString("D"),
                        Email = "aleximinor1310@gmail.com",
                        UserName = "aleximinor1310@gmail.com",
                        DateCreated = DateTime.Now,
                        FirstName = "Alexi",
                        LastName = "Minor",
                        Patronymic = "S",
                        IsActive = true,
                        LastModified = DateTime.Now,
                        EmailConfirmed = true,
                    };

                    await _userManager.CreateAsync(user);
                    await _userManager.AddPasswordAsync(user, _configuration["User:Password"]);
                    if (!_roleManager.Roles.Any(role => role.Name.Equals("Admin")))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _telegramLogger.Debug("Admin role added");
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return NotFound();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        public IActionResult Bonus()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
