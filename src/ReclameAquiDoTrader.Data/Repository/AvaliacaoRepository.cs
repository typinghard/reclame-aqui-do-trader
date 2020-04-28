using Raven.Client.Documents.Session;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Data.Repository
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        public AvaliacaoRepository(IDocumentSession session, INotificador notificador) : base(session, notificador)
        {
        }
    }
}
