using Raven.Client.Documents.Session;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReclameAquiDoTrader.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IDocumentSession _session;
        protected readonly INotificador _notificador;

        public Repository(IDocumentSession session, INotificador notificador)
        {
            _notificador = notificador;
            _session = session;
        }

        public void Adicionar(TEntity entity)
        {
            _session.Store(entity);
        }

        public void Atualizar(TEntity entity)
        {
            _session.Store(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> Listar()
        {
            return _session
                 .Query<TEntity>()
                 .ToList();
        }

        public TEntity ObterPorId(string id)
        {
            return _session
                 .Query<TEntity>()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
        }

        public void Remover(string id)
        {
            var entity = ObterPorId(id);
            _session.Delete(entity);
        }

        public IDocumentSession Session { get { return _session; } }
        public void SalvarAlteracoes()
        {
            try
            {
                _session.SaveChanges();
            }
            catch
            {
                _notificador.Handle(new Notificacao("", "Houve um problema com a persistência dos dados"));
            }
        }
    }
}
