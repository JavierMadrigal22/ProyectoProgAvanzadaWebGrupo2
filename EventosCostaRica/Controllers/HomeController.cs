using System.Diagnostics;
using CapaObjetos.ViewModelos;
using Microsoft.AspNetCore.Mvc;
using CapaLogicaDeNegocioBLL.Servicios.ListaEventos;

namespace EventosCostaRica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IListaEventoServicio _eventoServicio;

        public HomeController(
             ILogger<HomeController> logger,
             IListaEventoServicio eventoServicio)
        {
            _logger = logger;
            _eventoServicio = eventoServicio;
        }

        public async Task<IActionResult> Index()
        {
            var proximosEventos = await _eventoServicio.ObtenerProximosEventosAsync();
            return View(proximosEventos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}