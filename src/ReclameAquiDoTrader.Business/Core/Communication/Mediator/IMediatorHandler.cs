﻿using ReclameAquiDoTrader.Business.Core.Messages;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.Business.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
