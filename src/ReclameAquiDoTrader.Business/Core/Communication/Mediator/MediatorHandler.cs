﻿using MediatR;
using ReclameAquiDoTrader.Business.Core.Messages;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.Business.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
