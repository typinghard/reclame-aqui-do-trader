using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.UI.Config;
using ReclameAquiDoTrader.UI.Identity.Models;
using ReclameAquiDoTrader.UI.ViewModels.AcessoViewModel;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : MainController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly GoogleReCaptchaService _googleReCaptchaService;
        private readonly IConfiguration _configuration;

        public AccountController(
                                IConfiguration configuration,
                                SignInManager<Usuario> signInManager,
                                GoogleReCaptchaService googleReCaptchaService,
                                INotificador notificador) : base(notificador)
        {
            _signInManager = signInManager;
            _googleReCaptchaService = googleReCaptchaService;
            _configuration = configuration;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInViewModel viewModel)
        {
            ValidarReCaptcha(viewModel.Token, viewModel.TokenValido);

            if (!OperacaoValida())
                return CustomResponse(new
                {
                    tokenValido = false
                });

            var signInResult = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, true, lockoutOnFailure: true);

            if (signInResult.Succeeded)
                return CustomResponse(new
                {
                    url = Url.Action("Index", "Dashboard")
                });

            var motivo = signInResult.IsLockedOut ? "Usuário temporariamente bloqueado por tentativas inválidas" :
                         signInResult.IsNotAllowed ? "Usuário não está autorizado para logar" :
                         signInResult.RequiresTwoFactor ? "Autenticação por 2 fatores requerido" :
                         "Usuário ou Senha incorretos";

            NotificarErro("Erro", motivo);

            return CustomResponse(new
            {
                tokenValido = true
            });
        }

        private void ValidarReCaptcha(string token, bool tokenValido)
        {
            if (_configuration["Ambiente"] != "Prod")
                return;

            if (tokenValido)
                return;

            var reCaptchaResponse = _googleReCaptchaService.VerificaReCaptcha(token).Result;
            if (!reCaptchaResponse.Sucesso && reCaptchaResponse.Score < 0.5)
                NotificarErro("Erro", "ReCaptcha inválido, atualize a página e tente novamente!");
        }

        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
