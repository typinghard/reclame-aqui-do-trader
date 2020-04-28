using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Aggregates;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.UI.ViewModels.MentoresViewModels;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class MentoresController : MainController
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IMapper _mapper;

        public MentoresController(INotificador notificador,
                                  IMapper mapper,
                                  IMentorRepository mentorRepository) : base(notificador)
        {
            _mapper = mapper;
            _mentorRepository = mentorRepository;
        }

        [HttpPost]
        public IActionResult Criar(CriarMentorViewModel viewModel)
        {
            var mentor = _mapper.Map<Mentor>(viewModel);
            
            _mentorRepository.Adicionar(mentor);
            _mentorRepository.SalvarAlteracoes();

            return CustomResponse();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
