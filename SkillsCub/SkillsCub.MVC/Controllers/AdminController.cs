using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.EmailSenderService;
using SkillsCub.MVC.ViewModels;
using SkillsCub.TelegramLogger;

namespace SkillsCub.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITelegramLogger _telegramLogger;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Course> _courseRepository;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            ITelegramLogger telegramLogger,
            IEmailSender emailSender,
            IRepository<Course> courseRepository
        )
        {
            _userManager = userManager;
            _telegramLogger = telegramLogger;
            _emailSender = emailSender;
            _courseRepository = courseRepository;
        }


        public IActionResult Index()
        {
            //TODO add displaying analytical info
            return View();
        }

        [HttpGet]
        public IActionResult RegisterTeacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterTeacher(ApplicationUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.DateCreated = DateTime.Now;
                    user.LastModified = DateTime.Now;
                    user.UserName = user.Email;

                    await _telegramLogger.Debug(
                        $"Teacher with ID {user.Id:D} created. {Environment.NewLine} User body: {Environment.NewLine} {JsonConvert.SerializeObject(user)}");
                    //TODO fix security
                    var message =
                        $"Уважаемый {user.FirstName} {user.Patronymic}  {user.LastName}! {Environment.NewLine}" +
                        $" Вы были зарегестрированы как преподаватель на skillscub.com.Ваше участие должно быть подтверждено. {Environment.NewLine}" +
                        $" Для завершения регистрации пройдите по ссылке:{Environment.NewLine}" +
                        $"https://{Request.Host}/Account/ConfirmRequestForTeacher/?id={user.Id} {Environment.NewLine}" +
                        " Если вы не регистрировались, то проигноирируйте данное сообщение.";
                    await _emailSender.SendEmailAsync(user.Email, "Подтверждение регистрации", message);
                    await _telegramLogger.Debug($"Message for User {user.Id} sended");

                    var userIdentity = await _userManager.CreateAsync(user);
                    await _telegramLogger.Debug($"User {JsonConvert.SerializeObject(userIdentity)} created in DB");

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Request was submited with Error {Environment.NewLine} {ex.Message}");
                Console.WriteLine(ex);
            }

            return null;
        }


        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            var model = new CourseViewModel
            {
                Teachers = (await _userManager.GetUsersInRoleAsync("Teacher"))
                    .Select(t => new SelectListItem {Value = t.Id, Text = $"{t.FirstName} {t.Patronymic} {t.LastName}"})
                    .ToList(),

                Students = (await _userManager.GetUsersInRoleAsync("Student"))
                    .Where(user => user.CurrentCourses == null || !user.CurrentCourses.Any())
                    .Select(t =>
                        new SelectListItem
                        {
                            Value = t.Id,
                            Text = $"{t.FirstName} {t.Patronymic} {t.LastName}"
                        })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    ID = Guid.NewGuid(),
                    Type = model.Type,
                    Name = $"{model.Type.ToString()}_{model.StartDate:d}",
                    ConsultationDate = model.ConsultationDate.Date,
                    ConsultationPlace = model.Place
                };

                course.Students = new List<UserCourse>(
                    (await _userManager
                        .GetUsersInRoleAsync("Student"))
                    .Where(user =>
                        model.StudentsId
                            .Any(s => user.Id.Equals(s)))
                    .Select(user => new UserCourse()
                    {
                        ID = Guid.NewGuid(),
                        AssignationDate = DateTime.Now,
                        CourseID = course.ID,
                        StudentID = user.Id
                    }).ToList());

                course.TeacherId = model.TeacherId;
                await _courseRepository.Add(course);
                await _courseRepository.SaveChanges();

                foreach (var userCourse in course.Students)
                {
                    var user = await _userManager.FindByIdAsync(userCourse.StudentID);
                    var message =
                        $"Уважаемый {user.FirstName} {user.Patronymic}  {user.LastName}! {Environment.NewLine}" +
                        $" Вы были зарегестрированы на курс {course.Name} на skillscub.com. Консультация будет проводиться {course.ConsultationDate:f} {course.ConsultationPlace}." +
                        $"{Environment.NewLine} Если вы не регистрировались, то проигноирируйте данное сообщение.";
                    await _emailSender.SendEmailAsync(user.Email, "Подтверждение курса", message);
                }

                var teacher =
                    (await _userManager.GetUsersInRoleAsync("Teacher")).FirstOrDefault(user =>
                        user.Id.Equals(model.TeacherId));
                var messageForTeacher =
                    $"Уважаемый {teacher.FirstName} {teacher.Patronymic}  {teacher.LastName}! {Environment.NewLine}" +
                    $" Вы были зарегестрированы на курс {course.Name} на skillscub.com как преподаватель. Консультация будет проводиться {course.ConsultationDate:f} {course.ConsultationPlace}." +
                    $"{Environment.NewLine} Если вы не регистрировались, то проигноирируйте данное сообщение.";
                await _emailSender.SendEmailAsync(teacher.Email, "Подтверждение курса", messageForTeacher);
            }

            return RedirectToAction("Index");
        }
    }
}