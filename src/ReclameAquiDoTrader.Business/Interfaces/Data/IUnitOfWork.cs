using System;

namespace ReclameAquiDoTrader.Business.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
