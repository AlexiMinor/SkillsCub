using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Message :  IBaseEntity
    {
        public Guid Id { get; set; }

        public string MessageText { get; set; }

        public DateTime SendedDateTime { get; set; }
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        public string RecieverId { get; set; }
        [ForeignKey("RecieverId")]
        public ApplicationUser Reciever { get; set; }

        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}