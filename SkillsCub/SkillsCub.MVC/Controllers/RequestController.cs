using System;
using System.Linq;
using System.Threading.Tasks;
using Equinox.Domain.Interfaces;
using MicroOrm.Dapper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillsCub.DataLibrary;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.EmailSenderService;
using SkillsCub.MVC.Extensions;
using SkillsCub.MVC.ViewModels;
using SkillsCub.Utilities;

namespace SkillsCub.MVC.Controllers
{
    public class RequestController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IRepository<Request> _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(
            IEmailSender emailSender,
            ILogger<AccountController> logger, IRepository<Request> repository, UserManager<ApplicationUser> userManager)
        {
            _emailSender = emailSender;
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
        }
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RequestViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.IsAgreeWithLicense)
                    {
                        var request = new Request()
                        {
                            Id = Guid.NewGuid(),
                            Phone = model.Phone,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Patronymic = model.Patronymic,
                            AppliedDate = DateTime.Now,
                            Course = model.Course,
                            FirstTime = model.FirstTime,
                            Source = model.Source,
                        };
                        _repository.Add(request);
                        var x = _repository.SaveChanges();
                    }
                    else
                    {
                        //TODO redirect
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            //TODO Send email
            return View();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            var model = _repository.GetAll().ToList();
            return View(model);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> Submit(Guid id)
        {
            var request = _repository.GetById(id);
            if (request!=null)
            {
                try
                {
                   var user = RequestToUserConverter.ConvertToUser(request);
                    //TODO send email into which that info is placed: Link to Generate Password Page
                    //Save this user to temp DBSet
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                //TODO send user link to create password and finish registration after it
            }

            //TODO cast request to USER, get him a role, send him a password setup link
            return null;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id)
        {
            var model = _repository.GetAll().ToList();
            //TODO Get Requests from Database
            return null;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public JsonResult Reject(Guid id)
        {
            //Ajax
            _repository.Remove(id);
            var repoResponce = _repository.SaveChanges();

            //TODO Get Requests from Database
            return null;
        }
    }
}