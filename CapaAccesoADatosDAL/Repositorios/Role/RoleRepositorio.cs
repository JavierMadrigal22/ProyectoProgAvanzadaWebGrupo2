using CapaObjetos.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoADatosDAL.Repositorios.Role
{
    public class RoleRepositorio : IRoleRepositorio
    {
        private readonly EventoscostaricaContext _context;
        public RoleRepositorio(EventoscostaricaContext context)
            => _context = context;

        public Task<List<Rol>> ObtenerRolesAsync()
            => _context.Roles.ToListAsync();
    }
}
