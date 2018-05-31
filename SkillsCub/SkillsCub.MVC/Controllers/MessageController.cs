using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;

namespace SkillsCub.MVC.Controllers
{
    public class MessageController : Controller
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<Course> _courseRepository;

        public MessageController(IRepository<Course> repository, IRepository<Message> messageRepository)
        {
            _courseRepository = repository;
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public async Task<bool> Insert(Message message)
        {
            if (message.CourseId == Guid.Empty || string.IsNullOrEmpty(message.SenderId) ||
                string.IsNullOrEmpty(message.RecieverId)) return false;

            await _messageRepository.Add(message);
            await _messageRepository.SaveChanges();
            return true;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> Get(Guid courseId, DateTime? lastMessageTime = null)
        {
            if (await _courseRepository.GetById(courseId) != null)
            {
                var messages = new List<Message>();
                if (lastMessageTime != null)
                {
                   var orderedMessage = (await _messageRepository.FindBy(message
                                => message.CourseId.Equals(courseId) && message.SendedDateTime > lastMessageTime,
                            message
                                => message.Sender))
                        .OrderBy(message
                            => message.SendedDateTime)
                        .ToList();
                }
                else
                    return (await _messageRepository.FindBy(message 
                        => message.CourseId.Equals(courseId),
                    message 
                        => message.Sender))
                    .OrderBy(message 
                        => message.SendedDateTime)
                    .ToList();
            }

            return null;
        }
    }
}