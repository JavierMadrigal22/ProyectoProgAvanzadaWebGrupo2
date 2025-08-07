using CapaObjetos.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaAccesoADatosDAL.Repositorios.Asiento
{
    public interface IAsientoRepositorio
    {
        Task<List<CapaObjetos.Modelos.Asiento>> ObtenerAsientosPorEventoAsync(int eventoId);
        Task<CapaObjetos.Modelos.Asiento?> ObtenerAsientoPorIdAsync(int asientoId);
        Task<CapaObjetos.Modelos.Asiento> CrearAsientoAsync(CapaObjetos.Modelos.Asiento asiento);
        Task<List<CapaObjetos.Modelos.Asiento>> CrearAsientosParaEventoAsync(int eventoId);
        Task<CapaObjetos.Modelos.Asiento> ActualizarAsientoAsync(CapaObjetos.Modelos.Asiento asiento);
        Task<bool> MarcarAsientoComoOcupadoAsync(int asientoId);
        Task<bool> MarcarAsientoComoDisponibleAsync(int asientoId);
        Task<bool> EliminarAsientoAsync(int asientoId);
        Task<bool> ValidarAsientoDisponibleAsync(int asientoId);
        Task<List<CapaObjetos.Modelos.Asiento>> ObtenerAsientosDisponiblesAsync(int eventoId);
    }
}
