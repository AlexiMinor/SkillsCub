using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.TelegramLogger;

namespace SkillsCub.MVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly ITelegramLogger _telegramLogger;
        private readonly IRepository<Exercise> _exerciseRepository;
        private readonly IRepository<Course> _courseRepository;



        public ExercisesController(IRepository<Exercise> exerciseRepository, 
            ITelegramLogger telegramLogger)
        {
            _exerciseRepository = exerciseRepository;
            _telegramLogger = telegramLogger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _exerciseRepository.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByTeacher(string id)
        {
            var coursesOfTeacher = (await _courseRepository
                    .FindBy(course
                        => course.TeacherId.Equals(id))).ToList();

            var exercises = (await _exerciseRepository.FindBy(exercise =>
                coursesOfTeacher.Any(course => course.Id.Equals(exercise.Id)))).ToList();

            return Ok(exercises);
        }
    }
}