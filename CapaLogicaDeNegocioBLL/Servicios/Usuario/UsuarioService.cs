using AutoMapper;
using CapaAccesoADatosDAL.Repositorios.Usuario;
using CapaObjetos.ViewModelos;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;   
        }

        public async Task<UsuarioViewModelo> ActualizarUsuarioAsync(UsuarioViewModelo usuarioViewModelo)
        {
            var usuario = _mapper.Map<CapaObjetos.Modelos.Usuario>(usuarioViewModelo);
            var resultado = await _usuarioRepositorio.ActualizarUsuarioAsync(usuario);
            return _mapper.Map<UsuarioViewModelo>(resultado);
        }

        public async Task<UsuarioViewModelo> CrearUsuarioAsync(UsuarioViewModelo usuarioViewModelo)
        {
            var usuario = _mapper.Map<CapaObjetos.Modelos.Usuario>(usuarioViewModelo);
            usuario.FechaCreacion = DateTime.Now;
            var resultado = await _usuarioRepositorio.CrearUsuarioAsync(usuario);
            return _mapper.Map<UsuarioViewModelo>(resultado);
        }

        public async Task<bool> EliminarUsuarioAsync(int usuarioId)
        {
            return await _usuarioRepositorio.EliminarUsuarioAsync(usuarioId);
        }

        public async Task<UsuarioViewModelo> ObtenerUsuarioPorIdAsync(int usuarioId)
        {
            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorIdAsync(usuarioId);
            return _mapper.Map<UsuarioViewModelo>(usuario);   
        }

        public async Task<List<UsuarioViewModelo>> ObtenerUsuariosAsync()
        {
            var usuarios = await _usuarioRepositorio.ObtenerUsuarioAsync();
            return _mapper.Map<List<UsuarioViewModelo>>(usuarios);
        }

        public async Task<UsuarioViewModelo> ObtenerUsuarioPorEmailAsync(string email)
        {
            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorEmailAsync(email);
            return usuario == null ? null : _mapper.Map<UsuarioViewModelo>(usuario);
        }

        public async Task<bool> EmailExisteAsync(string email)
            => await _usuarioRepositorio.EmailExisteAsync(email);

    }
}
