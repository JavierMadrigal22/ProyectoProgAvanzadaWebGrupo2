using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.ViewModelos
{
    public class ListaEventoViewModelo
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La ubicación es requerida")]
        public string Ubicacion { get; set; } = null!;

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "La capacidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor a 0")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "La fecha y hora son requeridas")]
        public DateTime FechaHora { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public bool Estado { get; set; }
    }
}
