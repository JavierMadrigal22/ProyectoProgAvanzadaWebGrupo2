using AutoMapper;
using CapaObjetos.Modelos;
using CapaObjetos.ViewModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaObjetos
{
    public class MapeoClases : Profile
    {

        public MapeoClases()
        {
            // Mapeos para Usuario
            CreateMap<Usuario, UsuarioViewModelo>();
            CreateMap<UsuarioViewModelo, Usuario>();

            // Mapeos para Rol
            CreateMap<Rol, RolViewModelo>();
            CreateMap<RolViewModelo, Rol>();
        }

    }
}
