using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.DependencyInjection;
using Raven.Identity;
using ReclameAquiDoTrader.Data.Extensions;
using ReclameAquiDoTrader.UI.Identity.Models;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
//using ReclameAquiDoTrader.UI.Query.Indexes;

namespace ReclameAquiDoTrader.UI.Config
{
    public static class RavenConfig
    {
        public static IApplicationBuilder UseRavenDbConfig(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var docStore = serviceProvider.GetService<IDocumentStore>();
            docStore.Inicializar();
            //QueryIndexesConfiguration.Execute(docStore);
            return app;
        }
        public static IServiceCollection AddRavenDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRavenDbDocStore(options =>
                {
                    using var client = new WebClient();
                    var certBytes = client.DownloadData(configuration["RavebDBConnectionConfigs:Certificate:DownloadPath"]);

                    options.Certificate = new X509Certificate2(certBytes,
                                                               configuration["RavenSettings:CertPassword"],
                                                               X509KeyStorageFlags.MachineKeySet);
                })
                .AddRavenDbSession()
                .AddRavenDbAsyncSession()
                .AddIdentity<Usuario, Raven.Identity.IdentityRole>(x =>
                {

                    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    x.Lockout.MaxFailedAccessAttempts = 5;
                    x.Lockout.AllowedForNewUsers = true;

                })
                .AddRavenDbIdentityStores<Usuario>();

            return services;
        }
    }
}
