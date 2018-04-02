using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.MVC.ViewModels;

namespace SkillsCub.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<UserCourse> _userCourseRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(IRepository<Course> courseRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<UserCourse> userCourseRepository,
            IRepository<Exercise> exerciseRepository,
            IRepository<Answer> answerRepository)
        {
            _courseRepository = courseRepository;
            _userManager = userManager;
            _userCourseRepository = userCourseRepository;
            _exerciseRepository = exerciseRepository;
            _answerRepository = answerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userCourse = (await
                _userCourseRepository.FindBy(uc => uc.StudentID.Equals(user.Id))).FirstOrDefault();
            var course = (await _courseRepository.FindBy(c => c.ID.Equals(userCourse.CourseID), c => c.Exercises))
                .FirstOrDefault();

            return userCourse != null ? View(course) : null;
        }

        [HttpGet]
        public async Task<IActionResult> AddAnswer(Guid id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercise = await _exerciseRepository.GetById(id);
            if (exercise != null &&
                (await _answerRepository.FindBy(answer =>
                    answer.ExerciseID.Equals(id) && answer.UserID.Equals(user.Id))).FirstOrDefault() != null)
            {
                return View(new AnswerViewModel()
                {
                    Answer = new Answer() {ID = Guid.NewGuid(), UserID = user.Id, ExerciseID = exercise.ID},
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

            await _answerRepository.Add(ans);
            await _answerRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAnswer(Guid id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var ans = await _answerRepository.GetById(id);
            var exercise = await _exerciseRepository.GetById(ans.ExerciseID);
            if (exercise != null &&
                (await _answerRepository.FindBy(answer =>
                    answer.ExerciseID.Equals(id) && answer.UserID.Equals(user.Id))).FirstOrDefault() != null)
            {
                return View(new AnswerViewModel()
                {
                    Answer = ans,
                    Exercise = exercise
                });
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAnswer(AnswerViewModel model) //add model
        {
            var ans = model.Answer;
            ans.AnswerDateTime = DateTime.Now;

            await _answerRepository.Add(ans);
            await _answerRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExerciseDetails(Guid id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var exercise = await _exerciseRepository.GetById(id);
            var answer =
                (await _answerRepository.FindBy(a => a.ExerciseID.Equals(exercise.ID) && a.UserID.Equals(user.Id)))
                .FirstOrDefault();

            return View(new StudentExerciseDetailsViewModel() {Exercise = exercise, Answer = answer});
        }
    }
}