using System;

namespace ReclameAquiDoTrader.Business.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            CriadoAs = AtualizadoAs = DateTime.UtcNow;
        }


        public Entity(string id)
        {
            Id = id;
            CriadoAs = AtualizadoAs = DateTime.UtcNow;
        }

        public string Id { get; protected set; }
        public DateTime CriadoAs { get; set; }
        public DateTime AtualizadoAs { get; set; }
        public bool Inativo { get; protected set; }

        public void DefinirDataAtualizacao()
        {
            AtualizadoAs = DateTime.UtcNow;
        }
        public void Inativar()
        {
            DefinirDataAtualizacao();
            Inativo = true;
        }
        public void Ativar()
        {
            DefinirDataAtualizacao();
            Inativo = false;
        }
    }
}
