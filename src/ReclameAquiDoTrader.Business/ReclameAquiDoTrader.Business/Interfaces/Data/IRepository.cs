using ReclameAquiDoTrader.Business.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.Interfaces.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Remover(string id);
        IEnumerable<TEntity> Listar();
        TEntity ObterPorId(string id);
        void SalvarAlteracoes();
    }
}
