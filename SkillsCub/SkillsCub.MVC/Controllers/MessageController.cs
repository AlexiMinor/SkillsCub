using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;

namespace SkillsCub.MVC.Controllers
{
    public class MessageController : Controller
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public MessageController(IRepository<Course> repository, IRepository<Message> messageRepository, UserManager<ApplicationUser> userManager)
        {
            _courseRepository = repository;
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<bool> Insert(Message message)
        {
            var senderId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var id = Guid.NewGuid();
            var dt = DateTime.Now;

            var mes = new Message()
            {
                Id = id,
                MessageText = message.MessageText,
                CourseId = message.CourseId,
                SendedDateTime = dt,
                SenderId = senderId,
                RecieverId = message.RecieverId
            };

            if (mes.CourseId == Guid.Empty || string.IsNullOrEmpty(mes.SenderId) ||
                string.IsNullOrEmpty(mes.RecieverId)) return false;

            await _messageRepository.Add(mes);
            await _messageRepository.SaveChanges();
            return true;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get(Guid courseId, DateTime? lastMessageTime = null)
        {
            if (await _courseRepository.GetById(courseId) != null)
            {
                var messages = lastMessageTime != null
                    ? (await _messageRepository.FindBy(message
                            => message.CourseId.Equals(courseId) && message.SendedDateTime > lastMessageTime,
                        message
                            => message.Sender))
                    .OrderBy(message
                        => message.SendedDateTime)
                    .ToList()
                    : (await _messageRepository.FindBy(message
                            => message.CourseId.Equals(courseId),
                        message
                            => message.Sender))
                    .OrderBy(message
                        => message.SendedDateTime)
                    .ToList();

                return messages;
            }

            return null;
        }
    }
}