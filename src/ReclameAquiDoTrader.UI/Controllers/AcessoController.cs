using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Repository;
using ReclameAquiDoTrader.UI.Identity.Models;
using ReclameAquiDoTrader.UI.ViewModels.AcessoViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.UI.Controllers
{
    public class AcessoController : MainController
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AcessoController(SignInManager<AppUser> signInManager,
                                INotificador notificador) : base(notificador)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Entrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(SignInViewModel model)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (signInResult.Succeeded)
                return RedirectToAction("Index", "Home");

            var reason = signInResult.IsLockedOut ? "Seu usuário está temporariamente bloqueado" :
                signInResult.IsNotAllowed ? "Seu usuário não está autorizado para logar" :
                signInResult.RequiresTwoFactor ? "Autenticação por 2 fatores é requerido" :
                "Usuário ou senha inválidos";

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
