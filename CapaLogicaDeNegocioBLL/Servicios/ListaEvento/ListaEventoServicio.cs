using AutoMapper;
using CapaAccesoADatosDAL.Repositorios.Asiento;
using CapaAccesoADatosDAL.Repositorios.ListaEvento;
using CapaObjetos.Modelos;
using CapaObjetos.ViewModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.ListaEventos
{
    public class ListaEventoService : IListaEventoServicio
    {
        private readonly IListaEventoRepositorio _listaEventoRepositorio;
        private readonly IAsientoRepositorio _asientoRepositorio;
        private readonly IMapper _mapper;

        public ListaEventoService(IListaEventoRepositorio listaEventoRepositorio, IAsientoRepositorio asientoRepositorio, IMapper mapper)
        {
            _listaEventoRepositorio = listaEventoRepositorio;
            _asientoRepositorio = asientoRepositorio;
            _mapper = mapper;
        }

        public async Task<ListaEventoViewModelo> CrearListaEventoAsync(ListaEventoViewModelo listaEventoViewModelo)
        {
            try
            {
                var listaEvento = _mapper.Map<ListaEvento>(listaEventoViewModelo);
                listaEvento.FechaCreacion = DateTime.Now;
                listaEvento.FechaActualizacion = DateTime.Now;
                listaEvento.Estado = true;

                var resultado = await _listaEventoRepositorio.CrearEventoAsync(listaEvento);
                
                if (resultado != null)
                {
                    await _asientoRepositorio.CrearAsientosParaEventoAsync(resultado.EventoId);
                }

                return _mapper.Map<ListaEventoViewModelo>(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el evento: {ex.Message}", ex);
            }
        }

        public async Task<ListaEventoViewModelo> ActualizarListaEventoAsync(ListaEventoViewModelo listaEventoViewModelo)
        {
            try
            {
                var listaEvento = _mapper.Map<ListaEvento>(listaEventoViewModelo);
                listaEvento.FechaActualizacion = DateTime.Now;

                var resultado = await _listaEventoRepositorio.ActualizarEventoAsync(listaEvento);
                return _mapper.Map<ListaEventoViewModelo>(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el evento: {ex.Message}", ex);
            }
        }

        public async Task<bool> EliminarListaEventoAsync(int eventoId)
        {
            try
            {
                return await _listaEventoRepositorio.EliminarEventoAsync(eventoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el evento {eventoId}: {ex.Message}", ex);
            }
        }

        public async Task<List<ListaEventoViewModelo>> ObtenerListaEventosAsync()
        {
            try
            {
                var eventos = await _listaEventoRepositorio.ObtenerListaEventoAsync();
                return _mapper.Map<List<ListaEventoViewModelo>>(eventos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la lista de eventos: {ex.Message}", ex);
            }
        }

        public async Task<ListaEventoViewModelo> ObtenerListaEventoPorIdAsync(int eventoId)
        {
            try
            {
                var evento = await _listaEventoRepositorio.ObtenerListaEventoPorIdAsync(eventoId);
                return _mapper.Map<ListaEventoViewModelo>(evento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el evento {eventoId}: {ex.Message}", ex);
            }
        }

        public async Task<EventoDetalleViewModelo> ObtenerDetalleEventoAsync(int eventoId)
        {
            try
            {
                var evento = await _listaEventoRepositorio.ObtenerListaEventoPorIdAsync(eventoId);
                if (evento == null)
                    return null;

                var asientos = await _asientoRepositorio.ObtenerAsientosPorEventoAsync(eventoId);
                
                var eventoDetalle = _mapper.Map<EventoDetalleViewModelo>(evento);
                eventoDetalle.Asientos = _mapper.Map<List<AsientoViewModelo>>(asientos);
                eventoDetalle.AsientosOcupados = asientos.Count(a => a.EstaOcupado);

                return eventoDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el detalle del evento {eventoId}: {ex.Message}", ex);
            }
        }

        public async Task<List<ListaEventoViewModelo>> ObtenerProximosEventosAsync()
        {
            try
            {
                var eventos = await _listaEventoRepositorio.ObtenerListaEventoAsync();
                var proximosEventos = eventos.Where(e => e.FechaHora > DateTime.Now && e.Estado).ToList();
                return _mapper.Map<List<ListaEventoViewModelo>>(proximosEventos);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener próximos eventos: {ex.Message}", ex);
            }
        }

        public async Task<bool> EventoTieneAsientosAsync(int eventoId)
        {
            try
            {
                var asientos = await _asientoRepositorio.ObtenerAsientosPorEventoAsync(eventoId);
                return asientos.Any();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar asientos del evento {eventoId}: {ex.Message}", ex);
            }
        }
    }
}