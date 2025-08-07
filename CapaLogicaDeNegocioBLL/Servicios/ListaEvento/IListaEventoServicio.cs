using CapaObjetos.ViewModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.ListaEventos
{
    public interface IListaEventoServicio
    {
        Task<List<ListaEventoViewModelo>> ObtenerListaEventosAsync();
        Task<ListaEventoViewModelo> ObtenerListaEventoPorIdAsync(int eventoId);
        Task<EventoDetalleViewModelo> ObtenerDetalleEventoAsync(int eventoId);
        Task<ListaEventoViewModelo> CrearListaEventoAsync(ListaEventoViewModelo listaEvento);
        Task<ListaEventoViewModelo> ActualizarListaEventoAsync(ListaEventoViewModelo listaEvento);
        Task<bool> EliminarListaEventoAsync(int eventoId);
        Task<List<ListaEventoViewModelo>> ObtenerProximosEventosAsync();
        Task<bool> EventoTieneAsientosAsync(int eventoId);
    }
}
