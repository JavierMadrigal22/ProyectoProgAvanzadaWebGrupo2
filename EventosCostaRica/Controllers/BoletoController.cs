using AutoMapper;
using CapaLogicaDeNegocioBLL.Servicios.Asiento;
using CapaLogicaDeNegocioBLL.Servicios.Boleto;
using CapaLogicaDeNegocioBLL.Servicios.ListaEventos;
using CapaObjetos.ViewModelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventosCostaRica.Controllers
{
    [Authorize]
    public class BoletoController : Controller
    {
        private readonly IBoletoServicio _boletoServicio;
        private readonly IAsientoServicio _asientoServicio;
        private readonly IListaEventoServicio _eventoServicio;
        private readonly IMapper _mapper;

        public BoletoController(IBoletoServicio boletoServicio, IAsientoServicio asientoServicio, 
                               IListaEventoServicio eventoServicio, IMapper mapper)
        {
            _boletoServicio = boletoServicio;
            _asientoServicio = asientoServicio;
            _eventoServicio = eventoServicio;
            _mapper = mapper;
        }

        // GET: Boleto/MisBoletos
        public async Task<IActionResult> MisBoletos()
        {
            try
            {
                var usuarioId = GetCurrentUserId();
                var boletos = await _boletoServicio.ObtenerBoletosPorUsuarioAsync(usuarioId);
                return View(boletos);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar sus boletos: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Boleto/Comprar/5
        public async Task<IActionResult> Comprar(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID de evento no válido";
                return RedirectToAction("Index", "ListaEvento");
            }

            try
            {
                var eventoDetalle = await _eventoServicio.ObtenerDetalleEventoAsync(id.Value);
                if (eventoDetalle == null)
                {
                    TempData["Error"] = "Evento no encontrado";
                    return RedirectToAction("Index", "ListaEvento");
                }

                if (!eventoDetalle.PuedeComprar)
                {
                    TempData["Error"] = "Este evento no está disponible para compra";
                    return RedirectToAction("Details", "ListaEvento", new { id = id });
                }

                var modelo = new ComprarBoletoViewModelo
                {
                    EventoId = id.Value,
                    UsuarioId = GetCurrentUserId(),
                    Evento = eventoDetalle,
                    Asientos = eventoDetalle.Asientos
                };

                return View(modelo);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la página de compra: " + ex.Message;
                return RedirectToAction("Index", "ListaEvento");
            }
        }

        // POST: Boleto/Comprar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comprar(ComprarBoletosPostViewModelo postModelo)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var eventoDetalle = await _eventoServicio.ObtenerDetalleEventoAsync(postModelo.EventoId);
                    var modelo = new ComprarBoletoViewModelo
                    {
                        EventoId = postModelo.EventoId,
                        UsuarioId = GetCurrentUserId(),
                        Evento = eventoDetalle,
                        Asientos = eventoDetalle.Asientos,
                        AsientosSeleccionados = postModelo.AsientosSeleccionados ?? new List<int>()
                    };
                    return View(modelo);
                }
                catch
                {
                    return RedirectToAction("Index", "ListaEvento");
                }
            }
            
            try
            {
                var eventoDetalle = await _eventoServicio.ObtenerDetalleEventoAsync(postModelo.EventoId);
                
                if (eventoDetalle == null)
                {
                    TempData["Error"] = "No se pudo cargar la información del evento";
                    return RedirectToAction("Index", "ListaEvento");
                }
                
                if (!eventoDetalle.PuedeComprar)
                {
                    TempData["Error"] = "Este evento ya no está disponible para compra";
                    return RedirectToAction("Comprar", new { id = postModelo.EventoId });
                }
                
                if (postModelo.AsientosSeleccionados == null || !postModelo.AsientosSeleccionados.Any())
                {
                    TempData["Error"] = "Debe seleccionar al menos un asiento";
                    return RedirectToAction("Comprar", new { id = postModelo.EventoId });
                }
                
                foreach (var asientoId in postModelo.AsientosSeleccionados)
                {
                    var asiento = eventoDetalle.Asientos.FirstOrDefault(a => a.AsientoId == asientoId);
                    if (asiento == null || asiento.EstaOcupado)
                    {
                        TempData["Error"] = "Uno o más asientos seleccionados ya no están disponibles";
                        return RedirectToAction("Comprar", new { id = postModelo.EventoId });
                    }
                }

                var modelo = new ComprarBoletoViewModelo
                {
                    EventoId = postModelo.EventoId,
                    UsuarioId = GetCurrentUserId(),
                    Evento = eventoDetalle,
                    Asientos = eventoDetalle.Asientos,
                    AsientosSeleccionados = postModelo.AsientosSeleccionados
                };
                
                return View("ConfirmarCompra", modelo);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al procesar la selección: " + ex.Message;
                return RedirectToAction("Comprar", new { id = postModelo.EventoId });
            }
        }

        // GET: Boleto/ConfirmarCompra
        public async Task<IActionResult> ConfirmarCompra(ComprarBoletoViewModelo modelo)
        {
            try
            {
                // Revalidar datos del modelo
                if (modelo.AsientosSeleccionados == null || !modelo.AsientosSeleccionados.Any())
                {
                    TempData["Error"] = "No hay asientos seleccionados";
                    return RedirectToAction("Comprar", new { id = modelo.EventoId });
                }

                // Recargar información actualizada del evento
                modelo.Evento = await _eventoServicio.ObtenerDetalleEventoAsync(modelo.EventoId);
                modelo.Asientos = modelo.Evento.Asientos;
                modelo.UsuarioId = GetCurrentUserId();

                return View(modelo);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la confirmación: " + ex.Message;
                return RedirectToAction("Comprar", new { id = modelo.EventoId });
            }
        }

        // POST: Boleto/ProcesarPago
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarPago(ComprarBoletoViewModelo modelo)
        {
            try
            {
                modelo.Evento = await _eventoServicio.ObtenerDetalleEventoAsync(modelo.EventoId);
                modelo.Asientos = modelo.Evento.Asientos;
                modelo.UsuarioId = GetCurrentUserId();
                
                if (!modelo.Evento.PuedeComprar)
                {
                    TempData["Error"] = "Este evento ya no está disponible para compra";
                    return RedirectToAction("Comprar", new { id = modelo.EventoId });
                }
                
                if (modelo.AsientosSeleccionados == null || !modelo.AsientosSeleccionados.Any())
                {
                    TempData["Error"] = "Debe seleccionar al menos un asiento";
                    return RedirectToAction("Comprar", new { id = modelo.EventoId });
                }
                
                foreach (var asientoId in modelo.AsientosSeleccionados)
                {
                    var asiento = modelo.Asientos.FirstOrDefault(a => a.AsientoId == asientoId);
                    if (asiento == null || asiento.EstaOcupado)
                    {
                        TempData["Error"] = "Uno o más asientos seleccionados ya no están disponibles";
                        return RedirectToAction("Comprar", new { id = modelo.EventoId });
                    }
                }
                
                var boletos = await _boletoServicio.ComprarBoletosAsync(modelo);
                
                return View("PagoExitoso", boletos);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al procesar el pago: " + ex.Message;
                return View("ConfirmarCompra", modelo);
            }
        }

        // GET: Boleto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID de boleto no válido";
                return RedirectToAction(nameof(MisBoletos));
            }

            try
            {
                var boleto = await _boletoServicio.ObtenerBoletoPorIdAsync(id.Value);
                if (boleto == null)
                {
                    TempData["Error"] = "Boleto no encontrado";
                    return RedirectToAction(nameof(MisBoletos));
                }

                // Validar que el boleto pertenece al usuario actual
                var usuarioId = GetCurrentUserId();
                if (boleto.UsuarioId != usuarioId)
                {
                    TempData["Error"] = "No tiene permisos para ver este boleto";
                    return RedirectToAction(nameof(MisBoletos));
                }

                return View(boleto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el boleto: " + ex.Message;
                return RedirectToAction(nameof(MisBoletos));
            }
        }

        // GET: Boleto/EventoBoletos/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EventoBoletos(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "ID de evento no válido";
                return RedirectToAction("Index", "ListaEvento");
            }

            try
            {
                var boletos = await _boletoServicio.ObtenerBoletosPorEventoAsync(id.Value);
                var evento = await _eventoServicio.ObtenerListaEventoPorIdAsync(id.Value);
                
                ViewBag.Evento = evento;
                return View(boletos);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los boletos del evento: " + ex.Message;
                return RedirectToAction("Index", "ListaEvento");
            }
        }

        // Método privado para obtener el ID del usuario actual
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("Usuario no autenticado");
        }
    }
}
