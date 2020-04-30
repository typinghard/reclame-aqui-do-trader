using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.Core.Communication.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string chave, string mensagem)
        {
            Chave = chave;
            Mensagem = mensagem;
        }

        public string Mensagem { get; }
        public string Chave { get; }
    }
}
