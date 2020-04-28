
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ReclameAquiDoTrader.Business.Core.Communication.Mediator;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.Data.Repositories;
using ReclameAquiDoTrader.UI.Identity.Models;

namespace ReclameAquiDoTrader.UI.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IMentorRepository, MentorRepository>();

            return services;
        }
    }
}
