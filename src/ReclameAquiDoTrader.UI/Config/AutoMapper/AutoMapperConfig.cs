using AutoMapper;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels;
using ReclameAquiDoTrader.UI.ViewModels.PublicacaoViewModels;

namespace ReclameAquiDoTrader.UI.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Mentor, CriarMentorViewModel>().ReverseMap();
            CreateMap<Mentor, AlterarMentorViewModel>().ReverseMap();
            CreateMap<Mentor, RemoverMentorViewModel>().ReverseMap();

            CreateMap<Avaliacao, CriarAvaliacaoViewModel>().ReverseMap();
            CreateMap<Avaliacao, AlterarAvaliacaoViewModel>().ReverseMap();
            CreateMap<Avaliacao, RemoverAvaliacaoViewModel>().ReverseMap();
        }
    }
}