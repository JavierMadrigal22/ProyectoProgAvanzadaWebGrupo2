using AutoMapper;
using CapaAccesoADatosDAL.Repositorios.Asiento;
using CapaObjetos.ViewModelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Asiento
{
    public class AsientoServicio : IAsientoServicio
    {
        private readonly IAsientoRepositorio _asientoRepositorio;
        private readonly IMapper _mapper;

        public AsientoServicio(IAsientoRepositorio asientoRepositorio, IMapper mapper)
        {
            _asientoRepositorio = asientoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<AsientoViewModelo>> ObtenerAsientosPorEventoAsync(int eventoId)
        {
            try
            {
                var asientos = await _asientoRepositorio.ObtenerAsientosPorEventoAsync(eventoId);
                return _mapper.Map<List<AsientoViewModelo>>(asientos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener asientos del evento {eventoId}: {ex.Message}", ex);
            }
        }

        public async Task<AsientoViewModelo> ObtenerAsientoPorIdAsync(int asientoId)
        {
            try
            {
                var asiento = await _asientoRepositorio.ObtenerAsientoPorIdAsync(asientoId);
                return _mapper.Map<AsientoViewModelo>(asiento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el asiento {asientoId}: {ex.Message}", ex);
            }
        }

        public async Task<AsientoViewModelo> CrearAsientoAsync(AsientoViewModelo asientoViewModel)
        {
            try
            {
                var asiento = _mapper.Map<CapaObjetos.Modelos.Asiento>(asientoViewModel);
                
                var asientosEvento = await _asientoRepositorio.ObtenerAsientosPorEventoAsync(asiento.EventoId);
                var existeAsiento = asientosEvento.Any(a => a.Fila == asiento.Fila && a.Numero == asiento.Numero);
                
                if (existeAsiento)
                {
                    throw new InvalidOperationException($"Ya existe un asiento en la fila {asiento.Fila}, número {asiento.Numero}");
                }

                var asientoCreado = await _asientoRepositorio.CrearAsientoAsync(asiento);
                return _mapper.Map<AsientoViewModelo>(asientoCreado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el asiento: {ex.Message}", ex);
            }
        }

        public async Task<List<AsientoViewModelo>> CrearAsientosParaEventoAsync(int eventoId)
        {
            try
            {
                // Verificar si ya existen asientos para este evento
                var asientosExistentes = await _asientoRepositorio.ObtenerAsientosPorEventoAsync(eventoId);
                if (asientosExistentes.Any())
                {
                    throw new InvalidOperationException("Ya existen asientos creados para este evento");
                }

                var asientos = await _asientoRepositorio.CrearAsientosParaEventoAsync(eventoId);
                return _mapper.Map<List<AsientoViewModelo>>(asientos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear asientos para el evento {eventoId}: {ex.Message}", ex);
            }
        }

        public async Task<AsientoViewModelo> ActualizarAsientoAsync(AsientoViewModelo asientoViewModel)
        {
            try
            {
                var asiento = _mapper.Map<CapaObjetos.Modelos.Asiento>(asientoViewModel);
                var asientoActualizado = await _asientoRepositorio.ActualizarAsientoAsync(asiento);
                return _mapper.Map<AsientoViewModelo>(asientoActualizado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el asiento: {ex.Message}", ex);
            }
        }

        public async Task<bool> MarcarAsientoComoOcupadoAsync(int asientoId)
        {
            try
            {
                return await _asientoRepositorio.MarcarAsientoComoOcupadoAsync(asientoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al marcar asiento {asientoId} como ocupado: {ex.Message}", ex);
            }
        }

        public async Task<bool> MarcarAsientoComoDisponibleAsync(int asientoId)
        {
            try
            {
                return await _asientoRepositorio.MarcarAsientoComoDisponibleAsync(asientoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al marcar asiento {asientoId} como disponible: {ex.Message}", ex);
            }
        }

        public async Task<bool> EliminarAsientoAsync(int asientoId)
        {
            try
            {
                return await _asientoRepositorio.EliminarAsientoAsync(asientoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el asiento {asientoId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidarAsientoDisponibleAsync(int asientoId)
        {
            try
            {
                return await _asientoRepositorio.ValidarAsientoDisponibleAsync(asientoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar disponibilidad del asiento {asientoId}: {ex.Message}", ex);
            }
        }

        public async Task<List<AsientoViewModelo>> ObtenerAsientosDisponiblesAsync(int eventoId)
        {
            try
            {
                var asientos = await _asientoRepositorio.ObtenerAsientosDisponiblesAsync(eventoId);
                return _mapper.Map<List<AsientoViewModelo>>(asientos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener asientos disponibles del evento {eventoId}: {ex.Message}", ex);
            }
        }
    }
}
