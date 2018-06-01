using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.Core.Services;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.MVC.ViewModels;

namespace SkillsCub.MVC.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStorageClient _storageClient;

        public TeacherController(
            IRepository<Course> courseRepository,
            IRepository<Exercise> exerciseRepository,
            UserManager<ApplicationUser> userManager, 
            IStorageClient storageClient)
        {
            _courseRepository = courseRepository;
            _exerciseRepository = exerciseRepository;
            _userManager = userManager;
            _storageClient = storageClient;
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
                var model = (await _courseRepository.FindBy(c => c.Id.Equals(id), c=> c.Exercises, c=> c.Student)).FirstOrDefault();

                return View(new CourseDetailViewModel() { Course = model });
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult CreateTaskForCourse(Guid id)
        {
            return View(new ExerciseModelRequest() { CourseId = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskForCourse(ExerciseModelRequest model)
        {
            try
            {
                var exercise = new Exercise()
                {
                    Id = Guid.NewGuid(),
                    CourseId = model.CourseId,
                    Name = model.Name,
                    CreationDate = DateTime.Now,
                    OpenDateTime = model.TimeToOpen,
                    CloseDateTime = model.TimeToClose,
                    ConditionOfProblem = model.Detail
                };
                await _exerciseRepository.Add(exercise);
                await _exerciseRepository.SaveChanges();

                if (model.Files != null && model.Files.Any())
                {
                    foreach (var file in model.Files)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            await _storageClient.UploadFileAsync(stream, $"{file.FileName}", $"exercise_{exercise.Id:N}");
                        }
                    }
                }

                return RedirectToAction("CourseDetails", new { id = model.CourseId });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditExerciseForCourse(Guid exId)
        {
            var model = await _exerciseRepository.GetById(exId);
            if (model != null)
            {
                var viewModel = new ExerciseModelRequest
                {
                    Id = model.Id,
                    Name = model.Name,
                    CourseId = model.CourseId,
                    TimeToOpen = model.OpenDateTime,
                    TimeToClose = model.CloseDateTime,
                    Detail = model.ConditionOfProblem,
                };

                viewModel.AttachedFiles = await _storageClient.GetFilesFromNodeAsync($"exercise_{viewModel.Id:N}");
    
                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditExerciseForCourse(ExerciseModelRequest model)
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
            if (model.Files!=null && model.Files.Any())
            {
                foreach (var file in model.Files)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        await _storageClient.UploadFileAsync(stream, $"{file.FileName}", $"exercise_{model.Id:N}");
                    }
                }
            }
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
                    Id = model.Id,
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

            await _storageClient.RemoveFolderAsync($"exercise_{exercise.Id:N}");

            return RedirectToAction("CourseDetails", new { id = exercise.CourseId });

        }


        [HttpGet]
        public async Task<IActionResult> AddMarkForAnswer(Guid exId)
        {
            var exercise = (await _exerciseRepository.FindBy(a => a.Id.Equals(exId), a => a.User)).FirstOrDefault();
            var model = new MarkViewModel
            {
                Exercise = exercise,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMarkForAnswer(MarkViewModel mark)
        {
            var exercise = await _exerciseRepository.GetById(mark.Exercise.Id);
            exercise.Mark = mark.Exercise.Mark;
            await _exerciseRepository.Update(exercise);
            await _exerciseRepository.SaveChanges();
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