using System;
using System.Linq;
using System.Threading.Tasks;
using Equinox.Domain.Interfaces;
using MicroOrm.Dapper.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
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
                            Status = Status.Requested
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
            var model = _repository.GetAll().Where(request => request.Status.Equals(Status.Requested)).ToList();
            return View(model);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> Submit(Guid id)
        {
                var request = _repository.GetById(id);
                if (request != null)
                {
                    try
                    {
                        var user = RequestToUserConverter.ConvertToUser(request);
                        //TODO send email into which that info is placed: Link to Generate Password Page
                        var message =
                            $"Уважаемый {request.FirstName} {request.Patronymic}  {request.LastName}! {Environment.NewLine}" +
                            $" Вы подали заявку на skillscub.com.Ваше участие было подтверждено. {Environment.NewLine}" +
                            $" Для завершения регистрации пройдите по ссылке:{Environment.NewLine}" +
                            $"https://{Request.Host}/Account/ConfirmRequest/?id={user.Id} {Environment.NewLine}" +
                            " Если вы не регистрировались, то проигноирируйте данное сообщение.";
                        await _emailSender.SendEmailAsync(request.Email, "test", message);
                        var userIdentity = await _userManager.CreateAsync(user);
                        request.Status = Status.WaitingApprove;
                        return Json(userIdentity);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                //TODO not sure about is it a good practise
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
            try
            {
                var request = _repository.GetById(id);
                if (request==null)
                {
                    return null;
                }
                request.Status = Status.Rejected;
                _repository.Update(request);
                var repoResponce = _repository.SaveChanges();

                //TODO Get Requests from Database
                return Json(repoResponce);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}