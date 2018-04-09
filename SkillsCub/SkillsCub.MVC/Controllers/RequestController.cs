using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.EmailSenderService;
using SkillsCub.MVC.ViewModels;
using SkillsCub.TelegramLogger;
using SkillsCub.Utilities;

namespace SkillsCub.MVC.Controllers
{
    public class RequestController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Request> _repository;
        private readonly ITelegramLogger _telegramLogger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(
            IEmailSender emailSender,
            IRepository<Request> repository,
            UserManager<ApplicationUser> userManager,
            ITelegramLogger telegramLogger)
        {
            _emailSender = emailSender;
            _repository = repository;
            _userManager = userManager;
            _telegramLogger = telegramLogger;
        }
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RequestViewModel model)
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
                        await _telegramLogger.Debug(
                            $"Request created. {Environment.NewLine} Request body: {Environment.NewLine} {JsonConvert.SerializeObject(request)}");
                        _repository.Add(request);
                        var x = _repository.SaveChanges();
                        await _telegramLogger.Debug($"Request {request.Id:D} added to DB with status Requested");
                        return RedirectToAction("Index", "Home");
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Request was created with Error {Environment.NewLine} {ex.Message}");
                Console.WriteLine(ex);
                throw;
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var model = (await _repository.GetAll()).Where(request => request.Status.Equals(Status.Requested)).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Submit(Guid id)
        {
            var request = await _repository.GetById(id);
            
            if (request != null)
            {
                await _telegramLogger.Debug($"Request {id:D} loaded from DB. {Environment.NewLine} Request body: {Environment.NewLine} {Json(request)}");
                try
                {
                    var user = RequestToUserConverter.ConvertToUser(request);
                    await _telegramLogger.Debug($"User {user.Id:D} created from request from Request {id:D}. {Environment.NewLine} User body: {Environment.NewLine} {JsonConvert.SerializeObject(user)}");

                    var message =
                        $"Уважаемый {request.FirstName} {request.Patronymic}  {request.LastName}! {Environment.NewLine}" +
                        $" Вы подали заявку на skillscub.com.Ваше участие было подтверждено. {Environment.NewLine}" +
                        $" Для завершения регистрации пройдите по ссылке:{Environment.NewLine}" +
                        $"https://{Request.Host}/Account/ConfirmRequest/?id={user.Id} {Environment.NewLine}" +
                        " Если вы не регистрировались, то проигноирируйте данное сообщение.";
                    await _emailSender.SendEmailAsync(request.Email, "test", message);
                    await _telegramLogger.Debug($"Message for Student {user.Id} sended");

                    var userIdentity = await _userManager.CreateAsync(user);
                    await _telegramLogger.Debug($"Student {JsonConvert.SerializeObject(userIdentity)} created in DB");

                    request.Status = Status.WaitingApprove;

                    await _repository.Update(request);
                    await _repository.SaveChanges();

                    await _telegramLogger.Debug($"Request {id:D} status changes to WaitingApprove in DB.");


                    return Json(userIdentity);
                }
                catch (Exception ex)
                {
                    await _telegramLogger.Error($"Request was submited with Error {Environment.NewLine} {ex.Message}");
                    Console.WriteLine(ex);
                }
            }
            await _telegramLogger.Debug($"Request {id:D} not exist in DB.");

            //TODO not sure about is it a good practise
            return null;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _repository.GetById(id);
            //TODO Get Requests from Database
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Reject(Guid id)
        {
            try
            {
                var request = await _repository.GetById(id);
                if (request == null)
                {
                    await _telegramLogger.Debug($"Request {id:D} not exist in DB.");
                    return null;
                }
                await _telegramLogger.Debug($"Request {id:D} loaded from DB. {Environment.NewLine} Request body: {Environment.NewLine} {JsonConvert.SerializeObject(request)}");

                var message =
                    $"Уважаемый {request.FirstName} {request.Patronymic}  {request.LastName}! {Environment.NewLine}" +
                    $"Вы подали заявку на skillscub.com.Ваше участие было отклонено.{Environment.NewLine}" + 
                    " Если вы не регистрировались, то проигноирируйте данное сообщение.";
                await _emailSender.SendEmailAsync(request.Email, "test", message);
                await _telegramLogger.Debug($"Message for rejected request {request.Id} sended");

                request.Status = Status.Rejected;
                _repository.Update(request);
                var repoResponse = _repository.SaveChanges();
                await _telegramLogger.Debug($"Request {id:D} status changes to Rejected in DB.");
                return Json(repoResponse);
            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Request was rejected with Error {Environment.NewLine} {ex.Message}");
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}