using Raven.Client.Documents.Session;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;

namespace ReclameAquiDoTrader.Data.Repositories
{
    public class AssinanteRepository : Repository<Assinante>, IAssinanteRepository
    {
        public AssinanteRepository(IDocumentSession session,
                                   IAsyncDocumentSession asyncSession,
                                   INotificador notificador) : base(session, asyncSession, notificador)
        {
        }
    }
}
