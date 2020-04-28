using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.UI.Models;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class AvaliacoesController : MainController
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IMapper _mapper;

        protected AvaliacoesController(IAvaliacaoRepository avaliacaoRepository,
                                       IMapper mapper,
                                       INotificador notificador) : base(notificador)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
