using CapaObjetos.ViewModelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Asiento
{
    public interface IAsientoServicio
    {
        Task<List<AsientoViewModelo>> ObtenerAsientosPorEventoAsync(int eventoId);
        Task<AsientoViewModelo> ObtenerAsientoPorIdAsync(int asientoId);
        Task<AsientoViewModelo> CrearAsientoAsync(AsientoViewModelo asiento);
        Task<List<AsientoViewModelo>> CrearAsientosParaEventoAsync(int eventoId);
        Task<AsientoViewModelo> ActualizarAsientoAsync(AsientoViewModelo asiento);
        Task<bool> MarcarAsientoComoOcupadoAsync(int asientoId);
        Task<bool> MarcarAsientoComoDisponibleAsync(int asientoId);
        Task<bool> EliminarAsientoAsync(int asientoId);
        Task<bool> ValidarAsientoDisponibleAsync(int asientoId);
        Task<List<AsientoViewModelo>> ObtenerAsientosDisponiblesAsync(int eventoId);
    }
}
