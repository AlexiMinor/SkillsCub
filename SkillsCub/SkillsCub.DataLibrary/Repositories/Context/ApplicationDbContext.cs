using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.DataLibrary.Repositories.Context
{
    /// <summary>
    /// The aplication database context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        public DbSet<Request> Requests { get; set; }

        /// <summary>
        /// Gets or sets the courses.
        /// </summary>
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// Gets or sets the exercises.
        /// </summary>
        public DbSet<Exercise> Exercises { get; set; }

        /// <summary>
        /// The database designer.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The database relationship model.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("Users").HasMany(u => u.CoursesAsTeacher)
                .WithOne(course => course.Teacher).HasForeignKey(c => c.TeacherId); ;
            builder.Entity<ApplicationUser>().ToTable("Users").HasMany(u => u.Courses)
                .WithOne(course => course.Student).HasForeignKey(c=>c.StudentId);
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users").HasMany(u => u.ReceivedMessages)
                .WithOne(message => message.Reciever).HasForeignKey(c => c.RecieverId); ;
            builder.Entity<ApplicationUser>().ToTable("Users").HasMany(u => u.SendedMessages)
                .WithOne(message => message.Sender).HasForeignKey(c => c.SenderId);
            base.OnModelCreating(builder);
        }
    }
}
