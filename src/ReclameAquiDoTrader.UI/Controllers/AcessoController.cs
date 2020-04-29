using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.UI.Identity.Models;
using ReclameAquiDoTrader.UI.ViewModels.AcessoViewModel;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReclameAquiDoTrader.UI.Controllers
{
    [AllowAnonymous]
    public class AcessoController : MainController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public AcessoController(
                                SignInManager<Usuario> signInManager,
                                UserManager<Usuario> userManager,
                                INotificador notificador) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        
        [HttpGet]
        public IActionResult Entrar()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Entrar(SignInViewModel viewmModel)
        {
            if (!ModelState.IsValid)
                return View(viewmModel);

            var signInResult = await _signInManager.PasswordSignInAsync(viewmModel.Email, viewmModel.Password, true, false);

            if (signInResult.Succeeded)
                return RedirectToAction("Index", "Home");

            var motivo = signInResult.IsLockedOut ? "Usuário temporariamente bloqueado por tentativas inválidas" :
                         signInResult.IsNotAllowed ? "Usuário não está autorizado para logar" :
                         signInResult.RequiresTwoFactor ? "Autenticação por 2 fatores requerido" :
                         "Usuário ou Senha incorretos";

            ViewData["Erro"] = motivo;

            return View(viewmModel);
        }

        
        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Entrar");
        }
    }
}
