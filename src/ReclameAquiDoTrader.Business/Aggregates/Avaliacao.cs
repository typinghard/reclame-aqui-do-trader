using ReclameAquiDoTrader.Business.Core.DomainObjects;
using ReclameAquiDoTrader.Business.Statics;
using ReclameAquiDoTrader.Business.ValueObjects;
using System;
using System.Collections.Generic;

namespace ReclameAquiDoTrader.Business.Aggregates
{
    public class Avaliacao : Entity, IAggregateRoot
    {
        private Avaliacao() { }
        public Avaliacao(string mentorId, string texto) : base(Guid.NewGuid().ToString())
        {
            MentorId = mentorId;
            Texto = texto;
            HorasParaResposta = AvaliacaoStatics.TEMPO_EM_HORAS_PARA_EXPIRACAO;

            RedesSociais = new RedeSocialList();
            Comprovantes = new List<Arquivo>();
            Publicacoes = new List<Arquivo>();
        }

        public string MentorId { get; private set; }
        public string Texto { get; private set; }
        public void NovoTexto(string texto)
        {
            Texto = texto;
        }

        public string TextoResposta { get; private set; }
        public void RemoverResposta()
        {
            TextoResposta = string.Empty;
            DataResposta = null;
        }
        public int HorasParaResposta { get; private set; }
        public bool Positivo { get; private set; }
        public void Positivar()
        {
            Positivo = true;
        }
        public void Negativar()
        {
            Positivo = false;
        }
        public DateTime? DataResposta { get; private set; }
        public bool Respondido { get { return DataResposta.HasValue; } }
        public DateTime? DataPublicacao { get; private set; }
        public bool Publicado { get { return DataPublicacao.HasValue; } }
        public void Responder(string textoResposta)
        {
            TextoResposta = textoResposta;
            DataResposta = DateTime.UtcNow;
        }

        public RedeSocialList RedesSociais { get; private set; }
        public bool Expirado { get { return DataExpiracao < DateTime.UtcNow; } }
        public DateTime DataExpiracao { get { return CriadoAs.AddHours(HorasParaResposta); } }
        public List<Arquivo> Comprovantes { get; set; }
        public void Publicar()
        {
            DataPublicacao = DateTime.UtcNow;
        }
        public void RemoverPublicacao()
        {
            DataPublicacao = null;
        }

        public List<Arquivo> Publicacoes { get; set; }
    }
}
