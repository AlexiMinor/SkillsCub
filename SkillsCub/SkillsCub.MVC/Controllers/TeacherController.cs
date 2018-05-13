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
    public class TeacherController : Controller
    {
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherController(IRepository<Answer> answerRepository,
            IRepository<Course> courseRepository,
            IRepository<Exercise> exerciseRepository, 
            UserManager<ApplicationUser> userManager)
        {
            _answerRepository = answerRepository;
            _courseRepository = courseRepository;
            _exerciseRepository = exerciseRepository;
            _userManager = userManager;
        }


        //todo add mark for answer
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = (await _courseRepository.FindBy(course => course.TeacherId.Equals(user.Id), course => course.Exercises)).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CourseDetails(Guid id)
        {
            if (id != Guid.Empty)
            {
                var model = (await _courseRepository.FindBy(course => course.ID.Equals(id), course => course.Exercises, course => course.Student)).FirstOrDefault();
                return View(new CourseDetailViewModel() { Course = model});

            }

            return View(new CourseDetailViewModel());
        }

        [HttpGet]
        public IActionResult CreateTaskForCourse(Guid id)
        {
            return View(new ExerciseModel { CourseId = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskForCourse(ExerciseModel model)
        {
            //Insert data in DB
            await _exerciseRepository.Add(
                new Exercise()
                {
                    Id = Guid.NewGuid(),
                    CourseId = model.CourseId,
                    Name = model.Name,
                    CreationDate = DateTime.Now,
                    OpenDateTime = model.TimeToOpen,
                    CloseDateTime = model.TimeToClose,
                    ConditionOfProblem = model.Detail
                });
            await _exerciseRepository.SaveChanges();
            return RedirectToAction("CourseDetails", new { id = model.CourseId });
        }

        [HttpGet]
        public async Task<IActionResult> EditExerciseForCourse(Guid exId)
        {
            var model = await _exerciseRepository.GetById(exId);
            return View(new ExerciseModel
            {
                Name = model.Name,
                CourseId =  model.CourseId,
                TimeToOpen = model.OpenDateTime,
                TimeToClose = model.CloseDateTime,
                Detail = model.ConditionOfProblem
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditExerciseForCourse(ExerciseModel model)
        {
            await _exerciseRepository.Update(new Exercise()
            {
                Id = model.Id,
                CourseId = model.CourseId,
                Name = model.Name,
                LastEditDate = DateTime.Now,
                OpenDateTime = model.TimeToOpen,
                CloseDateTime = model.TimeToClose,
                ConditionOfProblem = model.Detail
            });
            await _exerciseRepository.SaveChanges();
            return RedirectToAction("CourseDetails", new { id = model.CourseId });
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var model = await _exerciseRepository.GetById(id);

            if (model != null)
                return View(new ExerciseModel
                {
                    Name = model.Name,
                    CourseId = model.CourseId,
                    TimeToOpen = model.OpenDateTime,
                    TimeToClose = model.CloseDateTime,
                    Detail = model.ConditionOfProblem
                });

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exercise = await _exerciseRepository.GetById(id);

            if (exercise == null)
                return NotFound();

            await _exerciseRepository.Remove(id);
            await _exerciseRepository.SaveChanges();

            return RedirectToAction("CourseDetails", new { id = exercise.CourseId });

        }


        [HttpGet]
        public async Task<IActionResult> AnswersList(Guid exId)
        {
            var ex = await _exerciseRepository.GetById(exId);
            var answers = (await _answerRepository.FindBy(answer => answer.ExerciseId.Equals(exId),
                answer => answer.User)).ToList();
            var model = new ExerciseAnswersViewModel(){Exercise = ex, Answers = answers};

            return model.Exercise != null 
                ? (IActionResult) View(model) 
                : NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> AddMarkForAnswer(Guid ansId)
        {
            var answer = (await _answerRepository.FindBy(a => a.Id.Equals(ansId), a=>a.Exercise, a=>a.User)).FirstOrDefault();
            var model = new MarkViewModel
            {
                Answer = answer,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMarkForAnswer(MarkViewModel mark)
        {
            var answer = await _answerRepository.GetById(mark.Answer.Id);
            answer.Mark = mark.Answer.Mark;
            await _answerRepository.Update(answer);
            await _answerRepository.SaveChanges();
            return RedirectToAction("Index");
        }



        //todo think
        [HttpPost]
        public async Task<IActionResult> CloseCourse(Guid id)
        {

            var course = await _courseRepository.GetById(id);
            course.IsActive = false;
            await _courseRepository.Update(course);
            await _courseRepository.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}