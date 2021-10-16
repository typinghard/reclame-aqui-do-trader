using AutoMapper;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.ValueObjects;
using ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels;
using ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels;

namespace ReclameAquiDoTrader.UI.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<RedeSocialList, CriarMentorViewModel>();
            CreateMap<RedeSocialList, Mentor>()
                .ForMember(dest => dest.RedesSociais, input => input.MapFrom(m => m.Lista));



            CreateMap<Mentor, DetalhesMentorViewModels>();
            CreateMap<Avaliacao, DetalhesMentorViewModels>();
            CreateMap<Avaliacao, IdAvaliacaoViewModel>();

            CreateMap<dynamic, AvaliacoesRespostasGraficoViewModel>();
        }
    }
}