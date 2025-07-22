using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.ListaEventos
{
    public interface IListaEventoServicio
    {
        Task<List<CapaObjetos.ViewModelos.ListaEventoViewModelo>> ObtenerListaEventosAsync();
        Task<CapaObjetos.ViewModelos.ListaEventoViewModelo> ObtenerListaEventoPorIdAsync(int eventoId);
        Task<CapaObjetos.ViewModelos.ListaEventoViewModelo> CrearListaEventoAsync(CapaObjetos.ViewModelos.ListaEventoViewModelo listaEvento);
        Task<CapaObjetos.ViewModelos.ListaEventoViewModelo> ActualizarListaEventoAsync(CapaObjetos.ViewModelos.ListaEventoViewModelo listaEvento);
        Task<bool> EliminarListaEventoAsync(int eventoId);
    }
}
