using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ReclameAquiDoTrader.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Raven.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

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
                .AddRavenDbSession();

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
