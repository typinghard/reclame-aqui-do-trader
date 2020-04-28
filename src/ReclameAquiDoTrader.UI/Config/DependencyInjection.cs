using Microsoft.Extensions.DependencyInjection;
using ReclameAquiDoTrader.Business.Core.Communication.Mediator;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.UI.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }
    }
}
