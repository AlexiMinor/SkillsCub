using System;

namespace SkillsCub.MVC.ViewModels
{
    public class MessageViewModel
    {
        public string MessageText { get; set; }
        public string MessageSender { get; set; }
        public DateTime SendedDateTime { get; set; }
        public bool IsYour { get; set; }
    }
}
