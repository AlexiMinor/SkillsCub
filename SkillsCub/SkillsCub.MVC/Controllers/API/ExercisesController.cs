using System;
using System.Threading.Tasks;
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
        private readonly IRepository<Exercise> _repository;

        public ExercisesController(IRepository<Exercise> repository, 
            ITelegramLogger telegramLogger)
        {
            _repository = repository;
            _telegramLogger = telegramLogger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _repository.GetById(id);

            return Ok(result);
        }
    }
}