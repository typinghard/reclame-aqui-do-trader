using ReclameAquiDoTrader.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReclameAquiDoTrader.UI.ViewModels.PesquisaViewModels
{
    public class AvaliacaoParaPesquisaViewModel
    {
        public AvaliacaoParaPesquisaViewModel()
        {
            Avaliacoes = new List<AvaliacaoParaPesquisaItemViewModel>();
        }
        public List<AvaliacaoParaPesquisaItemViewModel> Avaliacoes { get; set; }
        public List<QuantidadePorMentorViewModel> AvaliacoesPorMentor
        {
            get
            {
                var mentores = new List<QuantidadePorMentorViewModel>();
                foreach (var mentor in Avaliacoes.GroupBy(x => x.Mentor.MentorId))
                {
                    mentores.Add(new QuantidadePorMentorViewModel()
                    {
                        Mentor = Avaliacoes.First(x => x.Mentor.MentorId == mentor.Key).Mentor,
                        QtdePositivas = Avaliacoes.Count(x => x.Mentor.MentorId == mentor.Key && x.Positivo),
                        QtdePositivasUltimos30Dias = Avaliacoes.Count(x => x.Mentor.MentorId == mentor.Key && x.CriadoAs >= DateTime.Now.AddMonths(-1)),
                        QtdeNegativas = Avaliacoes.Count(x => x.Mentor.MentorId == mentor.Key && !x.Positivo),
                    });
                }
                return mentores;
            }
        }
        public int QuantidadeDeMentores
        {
            get
            {
                return Avaliacoes.GroupBy(x => x.Mentor.MentorId).Count();
            }
        }
        public List<QuantidadePorMentorViewModel> TopMentores
        {
            get
            {
                return AvaliacoesPorMentor
                    .Where(x => x.QtdeNegativas == 0)
                    .OrderByDescending(x => x.QtdePositivas)
                    .ToList();
            }
        }
    }
    public class QuantidadePorMentorViewModel
    {
        public int QtdeNegativas { get; set; }
        public int QtdePositivas { get; set; }
        public int QtdePositivasUltimos30Dias { get; set; }
        public int Quantidade { get { return QtdePositivas + QtdeNegativas; } }
        public MentorViewModel Mentor { get; set; }

    }
    public class QuantidadePorViewModel
    {
        public int Quantidade { get; set; }
        public string Nome { get; set; }
        public string Id { get; set; }
    }
    public class AvaliacaoParaPesquisaItemViewModel
    {
        public AvaliacaoParaPesquisaItemViewModel()
        {
            RedesSociais = new List<RedeSocialViewModel>();
        }
        public MentorViewModel Mentor { get; set; }
        public string AvaliacaoId { get; set; }
        public string Texto { get; set; }
        public DateTime CriadoAs { get; set; }
        public bool Positivo { get; set; }
        public bool Respondido { get; set; }
        public List<RedeSocialViewModel> RedesSociais { get; set; }
    }

    public class MentorViewModel
    {
        public MentorViewModel()
        {
            RedesSociais = new List<RedeSocialViewModel>();
            AreasDeAtuacao = new List<string>();
        }
        public string MentorId { get; set; }
        public string Nome { get; set; }
        public List<RedeSocialViewModel> RedesSociais { get; set; }
        public List<string> AreasDeAtuacao { get; set; }
    }
    public class RedeSocialViewModel
    {
        public string Url { get; set; }

        public ERedeSocialTipo Tipo { get; set; }
        public string Usuario { get; set; }
    }
}
