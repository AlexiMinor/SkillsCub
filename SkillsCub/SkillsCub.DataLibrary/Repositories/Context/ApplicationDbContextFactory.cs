using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SkillsCub.DataLibrary.Repositories.Context
{
        /// <summary>
        /// The aplication database context factory.
        /// </summary>
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            /// <summary>
            /// The create database context.
            /// </summary>
            /// <param name="args"></param>
            /// <returns></returns>
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseSqlServer("Server=localhost;Database=SkillsCubDev;User Id=sa; password=usw9hSl5;TrustServerCertificate=True;Trusted_Connection=False;Connection Timeout=30;Integrated Security=False;Persist Security Info=False;Encrypt=True;MultipleActiveResultSets=True;",
                    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));

                return new ApplicationDbContext(builder.Options);
            }
        }
    }