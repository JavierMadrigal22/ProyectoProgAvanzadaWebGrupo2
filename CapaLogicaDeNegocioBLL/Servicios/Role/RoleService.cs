using AutoMapper;
using CapaAccesoADatosDAL.Repositorios.Role;
using CapaObjetos.ViewModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaDeNegocioBLL.Servicios.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepositorio _repo;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<RolViewModelo>> ObtenerRolesAsync()
        {
            var roles = await _repo.ObtenerRolesAsync();
            return _mapper.Map<List<RolViewModelo>>(roles);
        }
    }
}
