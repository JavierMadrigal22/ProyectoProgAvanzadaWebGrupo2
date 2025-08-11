using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.ViewModelos
{
    public class ListaEventoViewModelo
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre del Evento")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La ubicación es requerida")]
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "La capacidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor a 0")]
        [Display(Name = "Capacidad")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "La fecha y hora son requeridas")]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }
        
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
        
        [Display(Name = "Fecha de Actualización")]
        public DateTime FechaActualizacion { get; set; }

        [Display(Name = "Estado")]
        public bool? Estado { get; set; }

        [Display(Name = "Banner")]
        public string? Banner { get; set; }

        [Required(ErrorMessage = "El precio base es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        [Display(Name = "Precio Base")]
        public decimal PrecioBase { get; set; }
    }
}
