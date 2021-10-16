using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReclameAquiDoTrader.Business.Core.Communication.Mediator;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Data;
using ReclameAquiDoTrader.Business.Interfaces.Identity;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.Data;
using ReclameAquiDoTrader.Data.Repositories;
using ReclameAquiDoTrader.Data.Storage;
using ReclameAquiDoTrader.UI.Extensions;
using ReclameAquiDoTrader.UI.Interfaces;
using ReclameAquiDoTrader.UI.Services;

namespace ReclameAquiDoTrader.UI.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IAssinanteRepository, AssinanteRepository>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IMentorRepository, MentorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAzureStorageService, AzureStorageService>();
            services.AddScoped<IPublicacaoService, ArquivosPublicacaoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUsuarioIdentity, UsuarioLogado>();


            return services;
        }
    }
}
