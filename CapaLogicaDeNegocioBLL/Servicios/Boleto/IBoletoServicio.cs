using CapaObjetos.ViewModelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Boleto
{
    public interface IBoletoServicio
    {
        Task<List<BoletoViewModelo>> ObtenerBoletosPorUsuarioAsync(int usuarioId);
        Task<List<BoletoViewModelo>> ObtenerBoletosPorEventoAsync(int eventoId);
        Task<BoletoViewModelo> ObtenerBoletoPorIdAsync(int boletoId);
        Task<BoletoViewModelo> CrearBoletoAsync(BoletoViewModelo boleto);
        Task<List<BoletoViewModelo>> ComprarBoletosAsync(ComprarBoletoViewModelo compra);
        Task<BoletoViewModelo> ActualizarBoletoAsync(BoletoViewModelo boleto);
        Task<bool> EliminarBoletoAsync(int boletoId);
        Task<bool> ValidarBoletoAsync(int boletoId, int usuarioId);
        Task<int> ContarBoletosPorEventoAsync(int eventoId);
    }
}
