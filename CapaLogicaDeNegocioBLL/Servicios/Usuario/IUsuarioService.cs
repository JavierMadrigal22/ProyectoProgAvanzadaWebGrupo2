using CapaObjetos.ViewModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Usuario
{
    public interface IUsuarioService
    {
        Task<List<CapaObjetos.ViewModelos.UsuarioViewModelo>> ObtenerUsuariosAsync();
        Task<CapaObjetos.ViewModelos.UsuarioViewModelo> ObtenerUsuarioPorIdAsync(int usuarioId);
        Task<CapaObjetos.ViewModelos.UsuarioViewModelo> CrearUsuarioAsync(CapaObjetos.ViewModelos.UsuarioViewModelo usuario);
        Task<CapaObjetos.ViewModelos.UsuarioViewModelo> ActualizarUsuarioAsync(CapaObjetos.ViewModelos.UsuarioViewModelo usuario);
        Task<bool> EliminarUsuarioAsync(int usuarioId);
        Task<UsuarioViewModelo> ObtenerUsuarioPorEmailAsync(string email);
        Task<bool> EmailExisteAsync(string email);
    }
}
