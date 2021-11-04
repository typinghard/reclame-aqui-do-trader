using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReclameAquiDoTrader.UI.Config;
using System;

namespace ReclameAquiDoTrader.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            if (Env.IsDevelopment())
            {
                services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            }
#endif
            services.AddControllersWithViews();

            services.AddMediatR(typeof(Startup));

            services.AddMvcConfiguration();

            services.AddAutoMapper(typeof(Startup));

            services.Configure<ReCaptchaConfig>(Configuration.GetSection("RecaptchaSettings"));
            services.AddTransient<GoogleReCaptchaService>();

            services.ResolveDependencies();

            services.AddRavenDbConfig(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRavenDbConfig(serviceProvider);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Pesquisa}/{action=Index}");
            }); 
        }
    }
}
