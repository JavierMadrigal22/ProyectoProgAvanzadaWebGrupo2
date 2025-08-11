using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaObjetos.Modelos;

namespace CapaAccesoADatosDAL.Repositorios.ListaEvento
{
    public class ListaEventoRepositorio : IListaEventoRepositorio
    {
        private readonly EventoscostaricaContext _context;

        public ListaEventoRepositorio(EventoscostaricaContext context)
        {
            _context = context;
        }

        public async Task<CapaObjetos.Modelos.ListaEvento> ActualizarEventoAsync(CapaObjetos.Modelos.ListaEvento listaevento)
        {
            var eventoExistente = await _context.ListaEventos.FindAsync(listaevento.EventoId);
            if (eventoExistente == null)
                return null!;

            _context.Entry(eventoExistente).CurrentValues.SetValues(listaevento);
            _context.Entry(eventoExistente).Property(e => e.FechaCreacion).IsModified = false;
            await _context.SaveChangesAsync();
            return eventoExistente;
        }

        public async Task<CapaObjetos.Modelos.ListaEvento> CrearEventoAsync(CapaObjetos.Modelos.ListaEvento listaevento)
        {
            _context.ListaEventos.Add(listaevento);
            await _context.SaveChangesAsync();
            return listaevento;
        }

        public async Task<bool> EliminarEventoAsync(int listaeventoId)
        {
            var evento = await _context.ListaEventos.FindAsync(listaeventoId);
            if (evento == null)
                return false;

            _context.ListaEventos.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CapaObjetos.Modelos.ListaEvento>> ObtenerListaEventoAsync()
        {
            return await _context.ListaEventos.ToListAsync();
        }

        public async Task<CapaObjetos.Modelos.ListaEvento> ObtenerListaEventoPorIdAsync(int listaeventoId)
        {
            return await _context.ListaEventos.FindAsync(listaeventoId);
        }
    }
}