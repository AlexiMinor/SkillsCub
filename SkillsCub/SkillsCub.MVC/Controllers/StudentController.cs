using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillsCub.Core;
using SkillsCub.Core.Services;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.MVC.ViewModels;
using SkillsCub.TelegramLogger;

namespace SkillsCub.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITelegramLogger _telegramLogger;
        private readonly IStorageClient _storageClient;


        public StudentController(IRepository<Course> courseRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Exercise> exerciseRepository,
            ITelegramLogger telegramLogger,
            IStorageClient storageClient)
        {
            _courseRepository = courseRepository;
            _userManager = userManager;
            _exerciseRepository = exerciseRepository;
            _telegramLogger = telegramLogger;
            _storageClient = storageClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var course = (await
                    _courseRepository.FindBy(c => c.StudentId.Equals(user.Id), c => c.Exercises)).FirstOrDefault();

                if (course != null && course.Exercises.Any())
                {
                    var dict = new Dictionary<Exercise, IEnumerable<AttachedFile>>();

                    foreach (var exercise in course.Exercises)
                    {
                        var files = await _storageClient.GetFilesFromNodeAsync($"exercise_{exercise.Id:N}");
                        dict.Add(exercise, files);
                    }

                    var viewModel = new StudentCourseViewModel() { CourseName = course.Name, Exercises = dict };
                    return View(viewModel);
                }

                return RedirectToAction("NotAssigned");
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction("NotAssigned");
            }
            catch (Exception ex)
            {
                await _telegramLogger.Error($"Password was added with Error {Environment.NewLine} {ex.Message}");
                return BadRequest();
            }
        }

        public IActionResult NotAssigned()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddAnswer(Guid id)
        {
            var exercise = await _exerciseRepository.GetById(id);

            var files = await _storageClient.GetFilesFromNodeAsync($"exercise_{id:N}");

            if (exercise != null)
            {
                return View(new StudentExerciseDetailsViewModel()
                {
                    Exercise = exercise,
                    AttachedFiles = files
                });
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(StudentExerciseDetailsViewModel model) //add model
        {
            var exercise = model.Exercise;
            var targetEx = await _exerciseRepository.GetById(model.Exercise.Id);

            if (targetEx == null)
                return NotFound();

            targetEx.AnswerValue = exercise.AnswerValue;
            targetEx.UserId = (await _userManager.GetUserAsync(HttpContext.User)).Id;

            await _exerciseRepository.Update(new Exercise()
            {
                Id = targetEx.Id,
                CourseId = targetEx.CourseId,
                Name = targetEx.Name,
                AnswerDateTime = DateTime.Now,
                OpenDateTime = targetEx.OpenDateTime,
                CloseDateTime = targetEx.CloseDateTime,
                ConditionOfProblem = targetEx.ConditionOfProblem,
                AnswerValue = exercise.AnswerValue,
                UserId = (await _userManager.GetUserAsync(HttpContext.User)).Id
            });
            await _exerciseRepository.SaveChanges();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ExerciseDetails(Guid id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercise = (await _exerciseRepository.GetById(id));

            if (exercise == null)
            {
                return NotFound();
            }

            var files = await _storageClient.GetFilesFromNodeAsync($"exercise_{id:N}");

            return View(new StudentExerciseDetailsViewModel()
            {
                Exercise = exercise,
                AttachedFiles = files
            });
        }
    }
}