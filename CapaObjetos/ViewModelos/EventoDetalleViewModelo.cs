using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.ViewModelos
{
    public class EventoDetalleViewModelo
    {
        public int EventoId { get; set; }
        
        [Display(Name = "Nombre del Evento")]
        public string Nombre { get; set; } = null!;
        
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; } = null!;
        
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = null!;
        
        [Display(Name = "Capacidad")]
        public int Capacidad { get; set; }
        
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }
        
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
        
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        
        [Display(Name = "Banner")]
        public string Banner { get; set; } = string.Empty;
        
        [Display(Name = "Precio Base")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioBase { get; set; }
        
        public int AsientosOcupados { get; set; }
        public int AsientosDisponibles => Math.Max(0, Capacidad - AsientosOcupados);
        public bool EstaVendido => AsientosDisponibles <= 0;
        public bool EsProximoEvento => FechaHora > DateTime.Now;
        
        public List<AsientoViewModelo> Asientos { get; set; } = new List<AsientoViewModelo>();
        
        public bool PuedeComprar => EsProximoEvento && !EstaVendido;
    }
}
