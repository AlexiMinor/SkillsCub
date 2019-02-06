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
                builder.UseSqlServer("Server=server;Database=db;User Id=sa; password=Password12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));

                return new ApplicationDbContext(builder.Options);
            }
        }
    }