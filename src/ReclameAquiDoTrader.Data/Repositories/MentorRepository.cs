using Raven.Client.Documents.Session;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;

namespace ReclameAquiDoTrader.Data.Repositories
{
    public class MentorRepository : Repository<Mentor>, IMentorRepository
    {
        public MentorRepository(INotificador notificador,
                                IDocumentSession session) : base(session, notificador)
        {
        }
    }
}
