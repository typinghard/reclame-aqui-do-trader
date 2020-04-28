using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
