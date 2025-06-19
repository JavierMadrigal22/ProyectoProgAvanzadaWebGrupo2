using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoADatosDAL.Repositorios.Usuario
{
    public interface IUsuarioRepositorio
    {
        Task<List<CapaObjetos.Modelos.Usuario>> ObtenerUsuarioAsync();
        Task<CapaObjetos.Modelos.Usuario> ObtenerUsuarioPorIdAsync(int usuarioId);
        Task <CapaObjetos.Modelos.Usuario> CrearUsuarioAsync(CapaObjetos.Modelos.Usuario usuario);
        Task<CapaObjetos.Modelos.Usuario> ActualizarUsuarioAsync(CapaObjetos.Modelos.Usuario usuario);
        Task<bool> EliminarUsuarioAsync(int usuarioId);
        Task<CapaObjetos.Modelos.Usuario> ObtenerUsuarioPorEmailAsync(string email);
        Task<bool> EmailExisteAsync(string email);


    }
}
