using Raven.Client.Documents.Session;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _session;
        protected readonly INotificador _notificador;

        public UnitOfWork(IDocumentSession session, INotificador notificador)
        {
            _notificador = notificador;
            _session = session;
        }

        public void Commit()
        {
            try
            {
                _session.SaveChanges();
            }
            catch (Exception ex)
            {
                _notificador.Handle(new Notificacao("Houve um problema com a persistência dos dados"));
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
