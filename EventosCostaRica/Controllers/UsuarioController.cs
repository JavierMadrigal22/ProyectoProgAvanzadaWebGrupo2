using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CapaLogicaDeNegocioBLL.Servicios.Role;
using CapaLogicaDeNegocioBLL.Servicios.Usuario;
using CapaObjetos.ViewModelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventosCostaRica.Controllers
{
    [Authorize(Roles = "1")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRoleService _roleService;
        private readonly IPasswordHasher<UsuarioViewModelo> _passwordHasher;

        public UsuarioController(
            IUsuarioService usuarioService,
            IRoleService roleService,
            IPasswordHasher<UsuarioViewModelo> passwordHasher)
        {
            _usuarioService = usuarioService;
            _roleService = roleService;
            _passwordHasher = passwordHasher;
        }

        // Helper para cargar la lista de roles
        private async Task CargarRolesAsync(UsuarioViewModelo vm)
        {
            var roles = await _roleService.ObtenerRolesAsync();
            vm.RolesLista = roles.Select(r => new SelectListItem
            {
                Value = r.RolID.ToString(),
                Text = r.Nombre
            }).ToList();
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id.Value);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // GET: Usuarios/Create
        public async Task<IActionResult> Create()
        {
            var vm = new UsuarioViewModelo();
            await CargarRolesAsync(vm);
            return View(vm);
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nombre,Apellido,Email,Password,Rol,Telefono,Estado")] UsuarioViewModelo usuario)
        {
            if (ModelState.IsValid)
            {
                // Hash de la contraseña antes de guardar
                usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password);

                await _usuarioService.CrearUsuarioAsync(usuario);
                return RedirectToAction(nameof(Index));
            }

            await CargarRolesAsync(usuario);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vm = await _usuarioService.ObtenerUsuarioPorIdAsync(id.Value);
            if (vm == null) return NotFound();

            await CargarRolesAsync(vm);
            return View(vm);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,Apellido,Email,Password,Rol,Telefono,Estado,FechaCreacion,FechaActualizacion")] UsuarioViewModelo usuario)
        {
            if (id != usuario.UsuarioId) return BadRequest();

            if (ModelState.IsValid)
            {
                // Opcional: si la contraseña cambió, rehasearla
                // Aquí asumimos que siempre viene la contraseña en claro
                usuario.Password = _passwordHasher.HashPassword(usuario, usuario.Password);

                await _usuarioService.ActualizarUsuarioAsync(usuario);
                return RedirectToAction(nameof(Index));
            }

            await CargarRolesAsync(usuario);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id.Value);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuarioService.EliminarUsuarioAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
