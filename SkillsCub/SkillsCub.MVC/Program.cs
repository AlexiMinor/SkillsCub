using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SkillsCub.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .UseIISIntegration()
                .Build();
    }
}
