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

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
