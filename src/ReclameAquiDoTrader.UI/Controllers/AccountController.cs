﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReclameAquiDoTrader.Business.Core.Communication.Notificacoes;
using ReclameAquiDoTrader.Business.Interfaces.Identity;
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
    public class AccountController : MainController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public AccountController(
                                SignInManager<Usuario> signInManager,
                                UserManager<Usuario> userManager,
                                INotificador notificador,
                                IUsuarioIdentity usuarioIdentity) : base(usuarioIdentity,notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var signInResult = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, true, false);

            if (signInResult.Succeeded)
                return RedirectToAction("Index", "Home");

            var motivo = signInResult.IsLockedOut ? "Usuário temporariamente bloqueado por tentativas inválidas" :
                         signInResult.IsNotAllowed ? "Usuário não está autorizado para logar" :
                         signInResult.RequiresTwoFactor ? "Autenticação por 2 fatores requerido" :
                         "Usuário ou Senha incorretos";

            NotificarErro(motivo);

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
