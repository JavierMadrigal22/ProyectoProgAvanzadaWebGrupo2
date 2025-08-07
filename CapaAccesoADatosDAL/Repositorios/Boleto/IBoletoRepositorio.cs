using CapaObjetos.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaAccesoADatosDAL.Repositorios.Boleto
{
    public interface IBoletoRepositorio
    {
        Task<List<CapaObjetos.Modelos.Boleto>> ObtenerBoletosPorUsuarioAsync(int usuarioId);
        Task<List<CapaObjetos.Modelos.Boleto>> ObtenerBoletosPorEventoAsync(int eventoId);
        Task<CapaObjetos.Modelos.Boleto> ObtenerBoletoPorIdAsync(int boletoId);
        Task<CapaObjetos.Modelos.Boleto> CrearBoletoAsync(CapaObjetos.Modelos.Boleto boleto);
        Task<List<CapaObjetos.Modelos.Boleto>> CrearBoletosAsync(List<CapaObjetos.Modelos.Boleto> boletos);
        Task<CapaObjetos.Modelos.Boleto> ActualizarBoletoAsync(CapaObjetos.Modelos.Boleto boleto);
        Task<bool> EliminarBoletoAsync(int boletoId);
        Task<bool> ValidarBoletoAsync(int boletoId, int usuarioId);
        Task<int> ContarBoletosPorEventoAsync(int eventoId);
    }
}
