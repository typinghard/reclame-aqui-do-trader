using ReclameAquiDoTrader.Business.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.UI.Extensions
{
    public static class ListExtension
    {
        public static List<Avaliacao> AvaliacoesEmAndamento(this IList<Avaliacao> avaliacoes)
        {
            return avaliacoes.Where(a => !a.Publicado
                                    && !a.Respondido
                                    && a.DataExpiracao > DateTime.UtcNow)
                              .OrderByDescending(a => a.CriadoAs).ToList();
        }

        public static List<Avaliacao> AvaliacoesPendentesDePublicacao(this IList<Avaliacao> avaliacoes)
        {
            return avaliacoes.Where(a => !a.Publicado
                                    && (a.Expirado || (a.Respondido && !a.Expirado)))
                             .OrderByDescending(a => a.CriadoAs).ToList();
        }

        public static List<Avaliacao> AvaliacoesPublicadas(this IList<Avaliacao> avaliacoes)
        {
            return avaliacoes.Where(a => a.Publicado)
                             .OrderByDescending(a => a.CriadoAs).ToList();
        }

        public static List<string> AreasAtuacaoDistinct(this IList<Mentor> mentores)
        {
            List<string> retorno = new List<string>();
            try
            {
                var areasAtuacao = mentores.Select(x => x._areasAtuacao).ToList();
                areasAtuacao.ForEach(x => x.ForEach(a => { if (!retorno.Contains(a)) { retorno.Add(a); } }));
                return retorno.OrderBy(area => area).ToList();
            }
            catch (Exception)
            {
            }

            return retorno;
        }
    }
}
