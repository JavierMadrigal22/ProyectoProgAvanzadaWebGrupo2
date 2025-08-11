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
            try
            {
                var eventos = await _eventoService.ObtenerListaEventosAsync();
                return View(eventos);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los eventos: " + ex.Message;
                return View(new List<ListaEventoViewModelo>());
            }
        }

        // GET: ListaEvento/ProximosEventos
        public async Task<IActionResult> ProximosEventos()
        {
            try
            {
                var eventos = await _eventoService.ObtenerProximosEventosAsync();
                return View(eventos);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los próximos eventos: " + ex.Message;
                return View(new List<ListaEventoViewModelo>());
            }
        }

        // GET: ListaEvento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID de evento no válido";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var eventoDetalle = await _eventoService.ObtenerDetalleEventoAsync(id.Value);
                if (eventoDetalle == null)
                {
                    TempData["Error"] = "Evento no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                return View(eventoDetalle);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el detalle del evento: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ListaEvento/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListaEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(ListaEventoViewModelo evento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    evento.FechaCreacion = DateTime.Now;
                    evento.FechaActualizacion = DateTime.Now;

                    await _eventoService.CrearListaEventoAsync(evento);
                    TempData["Success"] = "Evento creado exitosamente con asientos generados automáticamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear el evento: " + ex.Message;
                }
            }
            return View(evento);
        }

        // GET: ListaEvento/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID de evento no válido";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var evento = await _eventoService.ObtenerListaEventoPorIdAsync(id.Value);
                if (evento == null)
                {
                    TempData["Error"] = "Evento no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                return View(evento);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el evento: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ListaEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, ListaEventoViewModelo evento)
        {
            if (id != evento.EventoId)
            {
                TempData["Error"] = "ID de evento no coincide";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    evento.FechaActualizacion = DateTime.Now;
                    await _eventoService.ActualizarListaEventoAsync(evento);
                    TempData["Success"] = "Evento actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al actualizar el evento: " + ex.Message;
                }
            }

            return View(evento);
        }

        // GET: ListaEvento/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID de evento no válido";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var evento = await _eventoService.ObtenerListaEventoPorIdAsync(id.Value);
                if (evento == null)
                {
                    TempData["Error"] = "Evento no encontrado";
                    return RedirectToAction(nameof(Index));
                }

                return View(evento);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el evento: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ListaEvento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _eventoService.EliminarListaEventoAsync(id);
                TempData["Success"] = "Evento eliminado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el evento: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
