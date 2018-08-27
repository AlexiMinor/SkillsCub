using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillsCub.DataLibrary.Entities.Implementation;

namespace SkillsCub.DataLibrary.Repositories.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
