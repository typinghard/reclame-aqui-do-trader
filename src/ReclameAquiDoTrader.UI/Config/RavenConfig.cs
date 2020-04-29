using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using System;
using System.Net;
using ReclameAquiDoTrader.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Raven.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using ReclameAquiDoTrader.UI.Identity.Models;
using Raven.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ReclameAquiDoTrader.UI.Config
{
    public static class RavenConfig
    {
        public static IApplicationBuilder UseRavenDbConfig(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var docStore = serviceProvider.GetService<IDocumentStore>();
            docStore.Inicializar();
            return app;
        }
        public static IServiceCollection AddRavenDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            DownloadCertificate(configuration);

            services
                .AddRavenDbDocStore(options =>
                    options.Certificate = new X509Certificate2(configuration["RavenSettings:CertFilePath"],
                                                               configuration["RavenSettings:CertPassword"],
                                                           X509KeyStorageFlags.MachineKeySet))
                .AddRavenDbSession()
                .AddRavenDbAsyncSession()
                .AddIdentity<Usuario, Raven.Identity.IdentityRole>()    
                .AddRavenDbIdentityStores<Usuario>();

            return services;
        }

        private static void DownloadCertificate(IConfiguration configuration)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(configuration["RavebDBConnectionConfigs:Certificate:DownloadPath"],
                                    configuration["RavenSettings:CertFilePath"]);
            }
        }
    }
}
