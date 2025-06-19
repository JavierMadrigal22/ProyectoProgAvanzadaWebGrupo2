using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaObjetos.Modelos;

namespace CapaAccesoADatosDAL.Repositorios.Role
{
    public interface IRoleRepositorio
    {
        Task<List<Rol>> ObtenerRolesAsync();
    }
}

