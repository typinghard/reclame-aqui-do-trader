using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.Core.DomainObjects
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
            CriadoAs = AtualizadoAs = DateTime.UtcNow;
        }

        public string Id { get; protected set; }
        public DateTime CriadoAs { get; set; }
        public DateTime AtualizadoAs { get; set; }
    }
}
