using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.MVC.ViewModels;

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
        public async Task<MessageViewModel> Insert(Message message)
        {
            var sender = await _userManager.GetUserAsync(HttpContext.User);
            var id = Guid.NewGuid();
            var dt = DateTime.Now;
            var courseId = !message.CourseId.Equals(Guid.Empty) ? message.CourseId : (await _courseRepository.FindBy(course => course.StudentId.Equals(sender.Id) && course.TeacherId.Equals(message.RecieverId))).FirstOrDefault()?.Id;
            var mes = new Message()
            {
                Id = id,
                MessageText = message.MessageText,
                CourseId = courseId.GetValueOrDefault(),
                SendedDateTime = dt,
                SenderId = sender.Id,
                Sender= sender,
                RecieverId = message.RecieverId
            };

            if (mes.CourseId == Guid.Empty || string.IsNullOrEmpty(mes.SenderId) ||
                string.IsNullOrEmpty(mes.RecieverId)) return null;

            await _messageRepository.Add(mes);
            await _messageRepository.SaveChanges();
            var messageViewModel = new MessageViewModel()
            {
                SendedDateTime = dt,
                MessageText = mes.MessageText,
                MessageSender = $"{sender.FirstName} {sender.Patronymic} {sender.LastName}",
                IsYour = true
            };
            return messageViewModel;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageViewModel>> Get(Guid courseId, DateTime? lastMessageTime = null)
        {
            var yourId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var cId = !courseId.Equals(Guid.Empty) ? courseId : (await _courseRepository.FindBy(course => course.StudentId.Equals(yourId))).FirstOrDefault()?.Id;

            if (cId !=  Guid.Empty &&  await _courseRepository.GetById(cId.GetValueOrDefault()) != null)
            {
                var messages = lastMessageTime != null
                    ? (await _messageRepository.FindBy(message
                            => message.CourseId.Equals(cId) && message.SendedDateTime > lastMessageTime,
                        message
                            => message.Sender)).Select(message => new MessageViewModel
                    {
                        MessageText = message.MessageText,
                        SendedDateTime = message.SendedDateTime,
                        MessageSender = $"{message.Sender.FirstName} {message.Sender.Patronymic} {message.Sender.LastName}",
                        IsYour = message.SenderId.Equals(yourId)
                            })
                    .OrderBy(message
                        => message.SendedDateTime)
                    .ToList()
                    : (await _messageRepository.FindBy(message
                            => message.CourseId.Equals(cId),
                        message
                            => message.Sender)).Select(message => new MessageViewModel
                    {
                        MessageText = message.MessageText,
                        SendedDateTime = message.SendedDateTime,
                        MessageSender = $"{message.Sender.FirstName} {message.Sender.Patronymic} {message.Sender.LastName}",
                        IsYour = message.SenderId.Equals(yourId)
                    })
                    .OrderBy(message
                        => message.SendedDateTime)
                    .ToList();
                if (messages.Any())
                {
                    return messages;
                }
            }

            return null;
        }
    }
}