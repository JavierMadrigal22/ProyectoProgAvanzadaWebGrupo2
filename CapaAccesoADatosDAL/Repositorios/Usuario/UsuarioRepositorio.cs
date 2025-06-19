using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoADatosDAL.Repositorios.Usuario
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly EventoscostaricaContext _context;

        public UsuarioRepositorio(EventoscostaricaContext context)
        {
            _context = context;
        }

        public async Task<CapaObjetos.Modelos.Usuario> ActualizarUsuarioAsync(CapaObjetos.Modelos.Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.UsuarioId);
            if (usuarioExistente == null)
                return null!;

            _context.Entry(usuarioExistente).CurrentValues.SetValues(usuario);
            _context.Entry(usuarioExistente).Property(u => u.FechaCreacion).IsModified = false;
            await _context.SaveChangesAsync();
            return usuarioExistente;
        }

        public async Task <CapaObjetos.Modelos.Usuario> CrearUsuarioAsync(CapaObjetos.Modelos.Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> EliminarUsuarioAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CapaObjetos.Modelos.Usuario>> ObtenerUsuarioAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<CapaObjetos.Modelos.Usuario> ObtenerUsuarioPorIdAsync(int usuarioId)
        {
            return await _context.Usuarios.FindAsync(usuarioId);
        }

        public async Task<CapaObjetos.Modelos.Usuario> ObtenerUsuarioPorEmailAsync(string email)
          => await _context.Usuarios
        .SingleOrDefaultAsync(u => u.Email == email);

        public async Task<bool> EmailExisteAsync(string email)
            => await _context.Usuarios
                .AnyAsync(u => u.Email == email);

    }
}
