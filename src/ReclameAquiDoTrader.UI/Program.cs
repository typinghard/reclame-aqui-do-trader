using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ReclameAquiDoTrader.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
#if !DEBUG
                        webBuilder.UseSentry("https://956c5f0c21ea465b905b8f47b3226273@o409000.ingest.sentry.io/5280712");
#endif
                });
    }
}
