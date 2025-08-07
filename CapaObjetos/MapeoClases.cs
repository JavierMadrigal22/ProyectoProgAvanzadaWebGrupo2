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
            CreateMap<Usuario, UsuarioViewModelo>();
            CreateMap<UsuarioViewModelo, Usuario>();

            CreateMap<Rol, RolViewModelo>();
            CreateMap<RolViewModelo, Rol>();

            CreateMap<ListaEvento, ListaEventoViewModelo>();
            CreateMap<ListaEventoViewModelo, ListaEvento>();
            
            CreateMap<ListaEvento, EventoDetalleViewModelo>()
                .ForMember(dest => dest.AsientosOcupados, opt => opt.MapFrom(src => src.Asientos.Count(a => a.EstaOcupado)))
                .ForMember(dest => dest.Asientos, opt => opt.MapFrom(src => src.Asientos));
        
            CreateMap<Asiento, AsientoViewModelo>();
            CreateMap<AsientoViewModelo, Asiento>();
            
            CreateMap<Boleto, BoletoViewModelo>()
                .ForMember(dest => dest.NombreEvento, opt => opt.MapFrom(src => src.Evento.Nombre))
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ForMember(dest => dest.InformacionAsiento, opt => opt.MapFrom(src => $"Fila {src.Asiento.Fila} - Asiento {src.Asiento.Numero}"))
                .ForMember(dest => dest.FilaAsiento, opt => opt.MapFrom(src => src.Asiento.Fila))
                .ForMember(dest => dest.NumeroAsiento, opt => opt.MapFrom(src => src.Asiento.Numero));
            CreateMap<BoletoViewModelo, Boleto>();
            
            CreateMap<EventoDetalleViewModelo, ComprarBoletoViewModelo>()
                .ForMember(dest => dest.Evento, opt => opt.MapFrom(src => src));
        }

    }
}
