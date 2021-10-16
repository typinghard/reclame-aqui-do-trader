using AutoMapper;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.ValueObjects;
using ReclameAquiDoTrader.UI.ViewModels.AvaliacaoViewModels;
using ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels;

namespace ReclameAquiDoTrader.UI.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EmailViewModel, Email>()
                .ForMember(dest => dest.Endereco, i => i.MapFrom(m => m.Endereco));

            CreateMap<TelefoneViewModel, Telefone>()
                .ForMember(dest => dest.Numero, i => i.MapFrom(m => m.Numero));

            CreateMap<RedeSocialViewModel, RedeSocial>()
                .ForMember(dest => dest.Tipo, i => i.MapFrom(m => m.Tipo))
                .ForMember(dest => dest.Url, i => i.MapFrom(m => m.Url))
                .ForMember(dest => dest.Usuario, i => i.MapFrom(m => m.Usuario));

            CreateMap<CriarMentorViewModel, EmailList>()
                .ForMember(dest => dest.Emails, i => i.MapFrom(m => m.Emails));

            CreateMap<CriarMentorViewModel, TelefoneList>()
                .ForMember(dest => dest.Telefones, i => i.MapFrom(m => m.Telefones));

            CreateMap<CriarMentorViewModel, RedeSocialList>()
                    .ForMember(dest => dest.Lista, i => i.MapFrom(m => m.RedesSociais))
                    .ForMember(dest => dest._redeSocial, i => i.MapFrom(m => m.RedesSociais));


            CreateMap<CriarMentorViewModel, Mentor>()
                 .ForMember(x => x.Nome, opt => opt.MapFrom(model => model.Nome))
                 .ForMember(x => x.AreasDeAtuacao, opt => opt.MapFrom(model => model.AreasDeAtuacao))
                 .ForMember(x => x.Site, opt => opt.MapFrom(model => model.Site))
                 .ForMember(x => x.Emails, opt => opt.MapFrom(model => model.Emails))
                 .ForMember(x => x.Telefones, opt => opt.MapFrom(model => model.Telefones))
                 .ForMember(x => x.RedesSociais, opt => opt.MapFrom(model => model.RedesSociais));


            CreateMap<DetalhesMentorViewModels, Mentor>();
            CreateMap<DetalhesAvaliacaoViewModel, Avaliacao>();
        }
    }
}