using System;
using System.Threading.Tasks;
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

        public TeacherController(IRepository<Answer> answerRepository,
            IRepository<Course> courseRepository,
            IRepository<Exercise> exerciseRepository)
        {
            _answerRepository = answerRepository;
            _courseRepository = courseRepository;
            _exerciseRepository = exerciseRepository;
        }


        //TODO List of current courses
        //TODO Create homework
        //todo add mark for answer
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CourseDetails(Guid id)
        {
            var model = await _courseRepository.FindBy(course => course.ID.Equals(id), course => course.Exercises,
                course => course.Students);
            return View(model);
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
                    ID = Guid.NewGuid(),
                    CourseID = model.CourseId,
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
                CourseId =  model.CourseID,
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
                ID = model.Id,
                CourseID = model.CourseId,
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
                    CourseId = model.CourseID,
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

            return RedirectToAction("CourseDetails", new { id = exercise.CourseID });

        }



        [HttpGet]
        public async Task<IActionResult> AddMarkForAnswer(Guid ansId)
        {
            var answer = await _answerRepository.GetById(ansId);
            var model = new MarkViewModel
            {
                Answer = answer,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMarkForAnswer(MarkViewModel mark)
        {
            var answer = await _answerRepository.GetById(mark.Answer.ID);
            answer.Mark = mark.Answer.Mark;
            await _answerRepository.Update(answer);

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