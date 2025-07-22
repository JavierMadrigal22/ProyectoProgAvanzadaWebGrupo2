using AutoMapper;
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
        private readonly IMapper _mapper;

        public ListaEventoService(IListaEventoRepositorio listaEventoRepositorio, IMapper mapper)
        {
            _listaEventoRepositorio = listaEventoRepositorio;
            _mapper = mapper;
        }

        public async Task<ListaEventoViewModelo> CrearListaEventoAsync(ListaEventoViewModelo listaEventoViewModelo)
        {
            var listaEvento = _mapper.Map<ListaEvento>(listaEventoViewModelo);
            listaEvento.FechaCreacion = DateTime.Now;
            listaEvento.FechaActualizacion = DateTime.Now;

            var resultado = await _listaEventoRepositorio.CrearEventoAsync(listaEvento);
            return _mapper.Map<ListaEventoViewModelo>(resultado);
        }

        public async Task<ListaEventoViewModelo> ActualizarListaEventoAsync(ListaEventoViewModelo listaEventoViewModelo)
        {
            var listaEvento = _mapper.Map<ListaEvento>(listaEventoViewModelo);
            listaEvento.FechaActualizacion = DateTime.Now;

            var resultado = await _listaEventoRepositorio.ActualizarEventoAsync(listaEvento);
            return _mapper.Map<ListaEventoViewModelo>(resultado);
        }

        public async Task<bool> EliminarListaEventoAsync(int eventoId)
        {
            return await _listaEventoRepositorio.EliminarEventoAsync(eventoId);
        }
        public async Task<List<ListaEventoViewModelo>> ObtenerListaEventosAsync()
        {
            var eventos = await _listaEventoRepositorio.ObtenerListaEventoAsync();
            return _mapper.Map<List<ListaEventoViewModelo>>(eventos);
        }

        public async Task<ListaEventoViewModelo> ObtenerListaEventoPorIdAsync(int eventoId)
        {
            var evento = await _listaEventoRepositorio.ObtenerListaEventoPorIdAsync(eventoId);
            return _mapper.Map<ListaEventoViewModelo>(evento);
        }
    }
}