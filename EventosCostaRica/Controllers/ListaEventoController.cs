using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CapaLogicaDeNegocioBLL.Servicios.ListaEventos;
using CapaObjetos.ViewModelos;
using CapaObjetos.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EventosCostaRica.Controllers
{
    public class ListaEventoController : Controller
    {
        private readonly IListaEventoServicio _eventoService;

        public ListaEventoController(IListaEventoServicio eventoService)
        {
            _eventoService = eventoService;
        }

        // GET: ListaEvento
        public async Task<IActionResult> Index()
        {
            var eventos = await _eventoService.ObtenerListaEventosAsync();
            return View(eventos);
        }

        // GET: ListaEvento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.ObtenerListaEventoPorIdAsync(id.Value);
            if (evento == null) return NotFound();

            return View(evento);
        }

        // GET: ListaEvento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListaEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListaEventoViewModelo evento)
        {
            if (ModelState.IsValid)
            {
                evento.Estado = true;

                await _eventoService.CrearListaEventoAsync(evento);
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: ListaEvento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.ObtenerListaEventoPorIdAsync(id.Value);
            if (evento == null) return NotFound();

            return View(evento);
        }

        // POST: ListaEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListaEventoViewModelo evento)
        {
            if (id != evento.EventoId) return BadRequest();

            if (ModelState.IsValid)
            {
                evento.FechaActualizacion = DateTime.Now;

                await _eventoService.ActualizarListaEventoAsync(evento);
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        // GET: ListaEvento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.ObtenerListaEventoPorIdAsync(id.Value);
            if (evento == null) return NotFound();

            return View(evento);
        }

        // POST: ListaEvento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventoService.EliminarListaEventoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
