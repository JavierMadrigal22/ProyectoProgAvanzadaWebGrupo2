using CapaObjetos.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaAccesoADatosDAL.Repositorios.Asiento
{
    public class AsientoRepositorio : IAsientoRepositorio
    {
        private readonly EventoscostaricaContext _context;

        public AsientoRepositorio(EventoscostaricaContext context)
        {
            _context = context;
        }

        public async Task<List<CapaObjetos.Modelos.Asiento>> ObtenerAsientosPorEventoAsync(int eventoId)
        {
            return await _context.Asientos
                .Where(a => a.EventoId == eventoId)
                .OrderBy(a => a.Fila)
                .ThenBy(a => a.Numero)
                .ToListAsync();
        }

        public async Task<CapaObjetos.Modelos.Asiento?> ObtenerAsientoPorIdAsync(int asientoId)
        {
            return await _context.Asientos
                .Include(a => a.Evento)
                .FirstOrDefaultAsync(a => a.AsientoId == asientoId);
        }

        public async Task<CapaObjetos.Modelos.Asiento> CrearAsientoAsync(CapaObjetos.Modelos.Asiento asiento)
        {
            asiento.FechaCreacion = DateTime.Now;
            _context.Asientos.Add(asiento);
            await _context.SaveChangesAsync();
            return asiento;
        }

        public async Task<List<CapaObjetos.Modelos.Asiento>> CrearAsientosParaEventoAsync(int eventoId)
        {
            var asientos = new List<CapaObjetos.Modelos.Asiento>();
            
            // Crear 10 filas de 10 asientos cada una (100 asientos total)
            for (int fila = 1; fila <= 10; fila++)
            {
                for (int numero = 1; numero <= 10; numero++)
                {
                    var asiento = new CapaObjetos.Modelos.Asiento
                    {
                        EventoId = eventoId,
                        Fila = fila,
                        Numero = numero,
                        EstaOcupado = false,
                        FechaCreacion = DateTime.Now
                    };
                    asientos.Add(asiento);
                }
            }

            _context.Asientos.AddRange(asientos);
            await _context.SaveChangesAsync();
            return asientos;
        }

        public async Task<CapaObjetos.Modelos.Asiento> ActualizarAsientoAsync(CapaObjetos.Modelos.Asiento asiento)
        {
            _context.Asientos.Update(asiento);
            await _context.SaveChangesAsync();
            return asiento;
        }

        public async Task<bool> MarcarAsientoComoOcupadoAsync(int asientoId)
        {
            var asiento = await _context.Asientos.FindAsync(asientoId);
            if (asiento == null || asiento.EstaOcupado)
                return false;

            asiento.EstaOcupado = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarcarAsientoComoDisponibleAsync(int asientoId)
        {
            var asiento = await _context.Asientos.FindAsync(asientoId);
            if (asiento == null)
                return false;

            asiento.EstaOcupado = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsientoAsync(int asientoId)
        {
            var asiento = await _context.Asientos.FindAsync(asientoId);
            if (asiento == null)
                return false;

            _context.Asientos.Remove(asiento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidarAsientoDisponibleAsync(int asientoId)
        {
            var asiento = await _context.Asientos.FindAsync(asientoId);
            return asiento != null && !asiento.EstaOcupado;
        }

        public async Task<List<CapaObjetos.Modelos.Asiento>> ObtenerAsientosDisponiblesAsync(int eventoId)
        {
            return await _context.Asientos
                .Where(a => a.EventoId == eventoId && !a.EstaOcupado)
                .OrderBy(a => a.Fila)
                .ThenBy(a => a.Numero)
                .ToListAsync();
        }
    }
}
