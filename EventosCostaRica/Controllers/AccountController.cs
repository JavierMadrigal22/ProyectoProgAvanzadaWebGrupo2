using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CapaLogicaDeNegocioBLL.Servicios.Usuario;
using CapaObjetos.ViewModelos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventosCostaRica.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<UsuarioViewModelo> _passwordHasher;

        public AccountController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<UsuarioViewModelo>();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var userVm = await _usuarioService.ObtenerUsuarioPorEmailAsync(vm.Email);
            if (userVm != null)
            {
                var verifyResult = _passwordHasher.VerifyHashedPassword(userVm, userVm.Password, vm.Password);
                if (verifyResult == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userVm.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Name, userVm.Nombre),
                        new Claim(ClaimTypes.Role, userVm.Rol.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { IsPersistent = vm.RememberMe }
                    );

                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
            }

            ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (await _usuarioService.EmailExisteAsync(vm.Email))
            {
                ModelState.AddModelError("Email", "El correo ya está registrado.");
                return View(vm);
            }

            var nuevo = new UsuarioViewModelo
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Email = vm.Email,
                Rol = 2,
                Telefono = vm.Telefono,
                Estado = true
            };
            // Hashear la contraseña
            nuevo.Password = _passwordHasher.HashPassword(nuevo, vm.Password);

            await _usuarioService.CrearUsuarioAsync(nuevo);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
            => View();
    }
}
