using Raven.Client.Documents.Session;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;

namespace ReclameAquiDoTrader.Data.Repositories
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        public AvaliacaoRepository(IDocumentSession session,
                                   INotificador notificador) : base(session, notificador)
        {
        }
    }
}
