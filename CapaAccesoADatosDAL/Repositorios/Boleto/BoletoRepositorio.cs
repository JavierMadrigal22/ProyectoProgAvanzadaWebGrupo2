using CapaObjetos.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaAccesoADatosDAL.Repositorios.Boleto
{
    public class BoletoRepositorio : IBoletoRepositorio
    {
        private readonly EventoscostaricaContext _context;

        public BoletoRepositorio(EventoscostaricaContext context)
        {
            _context = context;
        }

        public async Task<List<CapaObjetos.Modelos.Boleto>> ObtenerBoletosPorUsuarioAsync(int usuarioId)
        {
            return await _context.Boletos
                .Include(b => b.Evento)
                .Include(b => b.Asiento)
                .Include(b => b.Usuario)
                .Where(b => b.UsuarioId == usuarioId)
                .OrderByDescending(b => b.FechaCompra)
                .ToListAsync();
        }

        public async Task<List<CapaObjetos.Modelos.Boleto>> ObtenerBoletosPorEventoAsync(int eventoId)
        {
            return await _context.Boletos
                .Include(b => b.Usuario)
                .Include(b => b.Asiento)
                .Where(b => b.EventoId == eventoId)
                .OrderBy(b => b.Asiento.Fila)
                .ThenBy(b => b.Asiento.Numero)
                .ToListAsync();
        }

        public async Task<CapaObjetos.Modelos.Boleto> ObtenerBoletoPorIdAsync(int boletoId)
        {
            return await _context.Boletos
                .Include(b => b.Evento)
                .Include(b => b.Asiento)
                .Include(b => b.Usuario)
                .FirstOrDefaultAsync(b => b.BoletoId == boletoId);
        }

        public async Task<CapaObjetos.Modelos.Boleto> CrearBoletoAsync(CapaObjetos.Modelos.Boleto boleto)
        {
            boleto.FechaCompra = DateTime.Now;
            boleto.Estado = true;
            
            _context.Boletos.Add(boleto);
            await _context.SaveChangesAsync();
            return boleto;
        }

        public async Task<List<CapaObjetos.Modelos.Boleto>> CrearBoletosAsync(List<CapaObjetos.Modelos.Boleto> boletos)
        {
            foreach (var boleto in boletos)
            {
                boleto.FechaCompra = DateTime.Now;
                boleto.Estado = true;
            }

            _context.Boletos.AddRange(boletos);
            await _context.SaveChangesAsync();
            return boletos;
        }

        public async Task<CapaObjetos.Modelos.Boleto> ActualizarBoletoAsync(CapaObjetos.Modelos.Boleto boleto)
        {
            _context.Boletos.Update(boleto);
            await _context.SaveChangesAsync();
            return boleto;
        }

        public async Task<bool> EliminarBoletoAsync(int boletoId)
        {
            var boleto = await _context.Boletos.FindAsync(boletoId);
            if (boleto == null)
                return false;

            _context.Boletos.Remove(boleto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidarBoletoAsync(int boletoId, int usuarioId)
        {
            var boleto = await _context.Boletos
                .FirstOrDefaultAsync(b => b.BoletoId == boletoId && b.UsuarioId == usuarioId);
            
            return boleto != null && boleto.Estado;
        }

        public async Task<int> ContarBoletosPorEventoAsync(int eventoId)
        {
            return await _context.Boletos
                .Where(b => b.EventoId == eventoId && b.Estado)
                .CountAsync();
        }
    }
}
