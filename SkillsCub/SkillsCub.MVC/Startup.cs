using CG.Web.MegaApiClient;
using ContentTypeResolver;
using MegaNzService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillsCub.Core.Services;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;
using SkillsCub.DataLibrary.Repositories.Implementation;
using SkillsCub.DataLibrary.Repositories.Interfaces;
using SkillsCub.EmailSenderService;
using SkillsCub.TelegramLogger;

namespace SkillsCub.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ITelegramLogger, TelegramLogger.TelegramLogger>();
            services.AddTransient<IRepository<Course>, CourseRepository>();
            services.AddTransient<IRepository<Exercise>, ExerciseRepository>();
            services.AddTransient<IRepository<Request>, RequestRepository>();
            services.AddTransient<IMegaApiClient, MegaApiClient>();
            services.AddTransient<IStorageClient, MegaNzClient>();
            services.AddTransient<IContentTypeResolver, ContentTypeResolver.ContentTypeResolver>();

            services.AddMvc();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
