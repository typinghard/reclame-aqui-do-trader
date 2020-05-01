using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReclameAquiDoTrader.Business.Core.Communication.Mediator;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Identity;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.Data.Repositories;
using ReclameAquiDoTrader.UI.Extensions;

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuarioIdentity, UsuarioLogado>();

            return services;
        }
    }
}
