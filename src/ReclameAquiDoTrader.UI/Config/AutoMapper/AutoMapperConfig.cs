using AutoMapper;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels;

namespace ReclameAquiDoTrader.UI.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Mentor, CriarMentorViewModel>().ReverseMap();
        }
    }
}