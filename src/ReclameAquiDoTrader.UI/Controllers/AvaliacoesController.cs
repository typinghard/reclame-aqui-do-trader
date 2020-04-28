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
        public AvaliacoesController(IAvaliacaoRepository avaliacaoRepository,
                                    INotificador notificador) : base(notificador)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
