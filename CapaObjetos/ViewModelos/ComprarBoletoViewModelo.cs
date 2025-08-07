using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CapaObjetos.ViewModelos
{
    public class ComprarBoletoViewModelo
    {
        [Required(ErrorMessage = "El evento es requerido")]
        public int EventoId { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar al menos un asiento")]
        public List<int> AsientosSeleccionados { get; set; } = new List<int>();
        
        [Required(ErrorMessage = "El usuario es requerido")]
        public int UsuarioId { get; set; }
        
        [BindNever]
        public EventoDetalleViewModelo Evento { get; set; } = new EventoDetalleViewModelo();
        
        [BindNever]
        public List<AsientoViewModelo> Asientos { get; set; } = new List<AsientoViewModelo>();
        
        public decimal PrecioTotal => AsientosSeleccionados.Count * Evento.PrecioBase;
        public int CantidadAsientos => AsientosSeleccionados.Count;
        
        public bool TieneAsientosSeleccionados => AsientosSeleccionados.Count > 0;
        public bool ExcedeCapacidad => AsientosSeleccionados.Count > Evento.AsientosDisponibles;
    }
}
