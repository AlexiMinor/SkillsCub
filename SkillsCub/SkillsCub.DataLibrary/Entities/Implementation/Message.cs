using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    /// <summary>
    /// The message.
    /// </summary>
    public class Message :  IBaseEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the message text. 
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Gets or sets the sended data time.
        /// </summary>
        public DateTime SendedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the sender id.
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Gets or sets the sender
        /// </summary>
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        /// <summary>
        /// Gets or sets the reciecer id.
        /// </summary>
        public string RecieverId { get; set; }

        /// <summary>
        /// Gets or sets the reciver.
        /// </summary>
        [ForeignKey("RecieverId")]
        public ApplicationUser Reciever { get; set; }

        /// <summary>
        /// Gets or sets the course id.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Gets or sets the course.
        /// </summary>
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}