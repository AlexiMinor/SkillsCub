﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.MVC.ViewModels;
using SkillsCub.TelegramLogger;

namespace SkillsCub.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITelegramLogger _telegramLogger;


        public StudentController(IRepository<Course> courseRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Exercise> exerciseRepository,
            IRepository<Answer> answerRepository, 
            ITelegramLogger telegramLogger)
        {
            _courseRepository = courseRepository;
            _userManager = userManager;
            _exerciseRepository = exerciseRepository;
            _answerRepository = answerRepository;
            _telegramLogger = telegramLogger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var course = (await
                    _courseRepository.FindBy(uc => uc.StudentId.Equals(user.Id))).FirstOrDefault();
                return course != null
                    ? (IActionResult) View(
                        (await _courseRepository.FindBy(c => c.ID.Equals(course.ID), c => c.Exercises))
                        .FirstOrDefault())
                    : RedirectToAction("NotAssigned");
                ;
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercise = await _exerciseRepository.GetById(id);
            var ans = (await _answerRepository.FindBy(answer =>
                answer.ExerciseId.Equals(id) && answer.UserId.Equals(user.Id))).FirstOrDefault();
            if (exercise != null)
            {
                return View(new AnswerViewModel()
                {
                    Answer = ans ??  new Answer() {Id = Guid.NewGuid(), UserId = user.Id, ExerciseId = exercise.Id},
                    Exercise = exercise
                });
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(AnswerViewModel model) //add model
        {
            var ans = model.Answer;
            ans.AnswerDateTime = DateTime.Now;

            if (await _answerRepository.GetById(ans.Id) != null)
                await _answerRepository.Update(ans);
            else
                await _answerRepository.Add(ans);

            await _answerRepository.SaveChanges();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ExerciseDetails(Guid id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercise = await _exerciseRepository.GetById(id);
            var answer =
                (await _answerRepository.FindBy(a => a.ExerciseId.Equals(exercise.Id) && a.UserId.Equals(user.Id)))
                .FirstOrDefault();

            return View(new StudentExerciseDetailsViewModel() {Exercise = exercise, Answer = answer});
        }
    }
}