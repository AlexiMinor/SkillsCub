using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    /// <summary>
    /// The application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the patronymic.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the last modified.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        #region Questionnaire

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the education.
        /// </summary>
        public string Education { get; set; } //base, medium, startProf, sr spec, high

        /// <summary>
        /// Gets or sets the additional education.
        /// </summary>
        public string AdditionalEducation { get; set; }

        /// <summary>
        /// Gets or sets the previous activities.
        /// </summary>
        public string PreviousActivities { get; set; }

        /// <summary>
        /// Gets or sets the previous projects.
        /// </summary>
        public string PreviousProjects { get; set; }

        /// <summary>
        /// Gets or sets the self education.
        /// </summary>
        public string SelfEducation { get; set; }

        /// <summary>
        /// Gets or sets the profession.
        /// </summary>
        public bool IsPrManager { get; set; }
        public bool IsEventManager { get; set; }
        public bool IsProjectManager { get; set; }
        public bool IsMarketer { get; set; }
        public bool IsClothesDesigner { get; set; }
        public bool IsWebDesigner { get; set; }
        public bool IsGraphicalDesigner { get; set; }
        public bool IsIllustrator { get; set; }
        public bool IsPhotographer { get; set; }
        public bool IsCameraman { get; set; }
        public bool IsWriter { get; set; }
        public bool IsEditor { get; set; }
        public bool IsInterpreter { get; set; }
        public bool IsSmm { get; set; }
        public bool IsLayer { get; set; }
        public bool IsArchitect { get; set; }
        public bool IsScreenwriter  { get; set; }

        /// <summary>
        /// Gets or sets the psychotic.
        /// </summary>
        public string Psychotic { get; set; }

        /// <summary>
        /// Gets or sets the kind of thinking.
        /// </summary>
        public string KindOfThinking { get; set; }

        /// <summary>
        /// Gets or sets the kind of activity.
        /// </summary>
        public string KindOfActivity { get; set; }

        /// <summary>
        /// Gets or sets the work time.
        /// </summary>
        public string WorkTime { get; set; }

        /// <summary>
        /// Gets or sets the command work.
        /// </summary>
        public string CommandWork { get; set; }

        /// <summary>
        /// Gets or sets the command professional experience.
        /// </summary>
        public string CommandProfessionalExperience { get; set; }

        /// <summary>
        /// Gets or sets the action plan.
        /// </summary>
        public string ActionPlan { get; set; }

        /// <summary>
        /// Gets or sets the planning experience.
        /// </summary>
        public string PlanningExperience { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether responsibility.
        /// </summary>
        public bool Responsibility { get; set; }

        /// <summary>
        /// Gets or sets the need for communication.
        /// </summary>
        public string NeedForCommunication { get; set; }

        /// <summary>
        /// Gets or sets the communication experience.
        /// </summary>
        public string CommunicationExperience { get; set; }

        /// <summary>
        /// Gets or sets the smm experience.
        /// </summary>
        // ReSharper disable once StyleCop.SA1650
        public string SMMExperience { get; set; }

        /// <summary>
        /// Gets or sets the relation to performances.
        /// </summary>
        public string RelationToPerformances { get; set; }

        /// <summary>
        /// Gets or sets the response for critic.
        /// </summary>
        public string ResponseForCritic { get; set; }

        /// <summary>
        /// Gets or sets the creativity limitation.
        /// </summary>
        public string CreativityLimitation { get; set; }

        /// <summary>
        /// Gets or sets the combine incongruous.
        /// </summary>
        public string CombineIncongruous { get; set; }

        /// <summary>
        /// Gets or sets the add assessment.
        /// </summary>
        public string AddAssessment { get; set; }

        /// <summary>
        /// Gets or sets the maintaining statistics.
        /// </summary>
        public string MaintainingStatistics { get; set; }

        /// <summary>
        /// Gets or sets the purchasing algorithm.
        /// </summary>
        public string PurchasingAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets the budget planning.
        /// </summary>
        public string BudgetPlanning { get; set; }

        /// <summary>
        /// Gets or sets the explaining comfort.
        /// </summary>
        public string ExplainingComfort { get; set; }

        /// <summary>
        /// Gets or sets the end justifies the means.
        /// </summary>
        public string EndJustifiesTheMeans { get; set; }

        /// <summary>
        /// Gets or sets the disclosure o pf secrecy.
        /// </summary>
        public string DisclosureOPfSecrecy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mailing lists.
        /// </summary>
        public bool IsAddToTheMailingList { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether full day.
        /// </summary>
        public bool FullDay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether flexible schedule.
        /// </summary>
        public bool FlexibleSchedule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether flextime.
        /// </summary>
        public bool Flextime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shift chart.
        /// </summary>
        public bool ShiftChart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether temporary job.
        /// </summary>
        public bool TemporaryJob { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether one off work.
        /// </summary>
        public bool OneOffWork { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether freelance work.
        /// </summary>
        public bool FreelanceWork { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether remote work.
        /// </summary>
        public bool RemoteWork { get; set; }

        #endregion

        #region EF_LINKS

        /// <summary>
        /// Gets or sets the courses.
        /// </summary>
        public IEnumerable<Course> Courses { get; set; }

        /// <summary>
        /// Gets or sets the courses as teacher.
        /// </summary>
        public IEnumerable<Course> CoursesAsTeacher { get; set; }

        /// <summary>
        /// Gets or sets the exercises.
        /// </summary>
        public IEnumerable<Exercise> Exercises { get; set; }

        /// <summary>
        /// Gets or sets the received messages.
        /// </summary>
        public IEnumerable<Message> ReceivedMessages { get; set; }

        /// <summary>
        /// Gets or sets the sended messages.
        /// </summary>
        // ReSharper disable once StyleCop.SA1650
        public IEnumerable<Message> SendedMessages { get; set; }

        #endregion
    }
}