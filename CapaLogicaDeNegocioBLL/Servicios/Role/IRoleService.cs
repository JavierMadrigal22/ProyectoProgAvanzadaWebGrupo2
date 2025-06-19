using CapaObjetos.ViewModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Role
{
    public interface IRoleService
    {
        Task<List<RolViewModelo>> ObtenerRolesAsync();
    }
}
