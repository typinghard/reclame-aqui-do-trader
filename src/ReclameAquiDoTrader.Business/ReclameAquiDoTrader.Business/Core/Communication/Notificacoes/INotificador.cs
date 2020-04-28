using System;
using System.Collections.Generic;
using System.Text;

namespace ReclameAquiDoTrader.Business.Core.Communication.Notificacoes
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
