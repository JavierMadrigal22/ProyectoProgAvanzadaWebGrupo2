using AutoMapper;
using CapaAccesoADatosDAL.Repositorios.Asiento;
using CapaAccesoADatosDAL.Repositorios.Boleto;
using CapaObjetos.ViewModelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Boleto
{
    public class BoletoServicio : IBoletoServicio
    {
        private readonly IBoletoRepositorio _boletoRepositorio;
        private readonly IAsientoRepositorio _asientoRepositorio;
        private readonly IMapper _mapper;

        public BoletoServicio(IBoletoRepositorio boletoRepositorio, IAsientoRepositorio asientoRepositorio, IMapper mapper)
        {
            _boletoRepositorio = boletoRepositorio;
            _asientoRepositorio = asientoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<BoletoViewModelo>> ObtenerBoletosPorUsuarioAsync(int usuarioId)
        {
            try
            {
                var boletos = await _boletoRepositorio.ObtenerBoletosPorUsuarioAsync(usuarioId);
                return _mapper.Map<List<BoletoViewModelo>>(boletos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener boletos del usuario {usuarioId}: {ex.Message}", ex);
            }
        }

        public async Task<List<BoletoViewModelo>> ObtenerBoletosPorEventoAsync(int eventoId)
        {
            try
            {
                var boletos = await _boletoRepositorio.ObtenerBoletosPorEventoAsync(eventoId);
                return _mapper.Map<List<BoletoViewModelo>>(boletos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener boletos del evento {eventoId}: {ex.Message}", ex);
            }
        }

        public async Task<BoletoViewModelo> ObtenerBoletoPorIdAsync(int boletoId)
        {
            try
            {
                var boleto = await _boletoRepositorio.ObtenerBoletoPorIdAsync(boletoId);
                return _mapper.Map<BoletoViewModelo>(boleto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el boleto {boletoId}: {ex.Message}", ex);
            }
        }

        public async Task<BoletoViewModelo> CrearBoletoAsync(BoletoViewModelo boletoViewModel)
        {
            try
            {
                var asientoDisponible = await _asientoRepositorio.ValidarAsientoDisponibleAsync(boletoViewModel.AsientoId);
                if (!asientoDisponible)
                {
                    throw new InvalidOperationException("El asiento seleccionado no está disponible");
                }

                var boleto = _mapper.Map<CapaObjetos.Modelos.Boleto>(boletoViewModel);
                var boletoCreado = await _boletoRepositorio.CrearBoletoAsync(boleto);

                await _asientoRepositorio.MarcarAsientoComoOcupadoAsync(boletoViewModel.AsientoId);

                return _mapper.Map<BoletoViewModelo>(boletoCreado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el boleto: {ex.Message}", ex);
            }
        }

        public async Task<List<BoletoViewModelo>> ComprarBoletosAsync(ComprarBoletoViewModelo compra)
        {
            try
            {   
                foreach (var asientoId in compra.AsientosSeleccionados)
                {
                    var asientoDisponible = await _asientoRepositorio.ValidarAsientoDisponibleAsync(asientoId);
                    if (!asientoDisponible)
                    {
                        var asiento = await _asientoRepositorio.ObtenerAsientoPorIdAsync(asientoId);
                        throw new InvalidOperationException($"El asiento Fila {asiento?.Fila} - Número {asiento?.Numero} no está disponible");
                    }
                }

                var boletos = new List<CapaObjetos.Modelos.Boleto>();
                foreach (var asientoId in compra.AsientosSeleccionados)
                {
                    var boleto = new CapaObjetos.Modelos.Boleto
                    {
                        EventoId = compra.EventoId,
                        UsuarioId = compra.UsuarioId,
                        AsientoId = asientoId,
                        Precio = compra.Evento.PrecioBase,
                        Estado = true
                    };
                    boletos.Add(boleto);
                }

                var boletosCreados = await _boletoRepositorio.CrearBoletosAsync(boletos);

                foreach (var asientoId in compra.AsientosSeleccionados)
                {
                    await _asientoRepositorio.MarcarAsientoComoOcupadoAsync(asientoId);
                }

                return _mapper.Map<List<BoletoViewModelo>>(boletosCreados);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al comprar boletos: {ex.Message}", ex);
            }
        }

        public async Task<BoletoViewModelo> ActualizarBoletoAsync(BoletoViewModelo boletoViewModel)
        {
            try
            {
                var boleto = _mapper.Map<CapaObjetos.Modelos.Boleto>(boletoViewModel);
                var boletoActualizado = await _boletoRepositorio.ActualizarBoletoAsync(boleto);
                return _mapper.Map<BoletoViewModelo>(boletoActualizado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el boleto: {ex.Message}", ex);
            }
        }

        public async Task<bool> EliminarBoletoAsync(int boletoId)
        {
            try
            {
                var boleto = await _boletoRepositorio.ObtenerBoletoPorIdAsync(boletoId);
                if (boleto == null)
                    return false;

                var eliminado = await _boletoRepositorio.EliminarBoletoAsync(boletoId);
                
                if (eliminado)
                {
                    await _asientoRepositorio.MarcarAsientoComoDisponibleAsync(boleto.AsientoId);
                }

                return eliminado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el boleto {boletoId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidarBoletoAsync(int boletoId, int usuarioId)
        {
            try
            {
                return await _boletoRepositorio.ValidarBoletoAsync(boletoId, usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar el boleto {boletoId}: {ex.Message}", ex);
            }
        }

        public async Task<int> ContarBoletosPorEventoAsync(int eventoId)
        {
            try
            {
                return await _boletoRepositorio.ContarBoletosPorEventoAsync(eventoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al contar boletos del evento {eventoId}: {ex.Message}", ex);
            }
        }
    }
}
