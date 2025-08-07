using System;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.ViewModelos
{
    public class AsientoViewModelo
    {
        public int AsientoId { get; set; }
        
        [Required(ErrorMessage = "El evento es requerido")]
        public int EventoId { get; set; }
        
        [Required(ErrorMessage = "La fila es requerida")]
        [Range(1, 10, ErrorMessage = "La fila debe estar entre 1 y 10")]
        [Display(Name = "Fila")]
        public int Fila { get; set; }
        
        [Required(ErrorMessage = "El número de asiento es requerido")]
        [Range(1, 10, ErrorMessage = "El número de asiento debe estar entre 1 y 10")]
        [Display(Name = "Número de Asiento")]
        public int Numero { get; set; }
        
        [Display(Name = "Está Ocupado")]
        public bool EstaOcupado { get; set; }
        
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
        
        public string NombreEvento { get; set; } = string.Empty;
        public string CssClass => EstaOcupado ? "asiento-ocupado" : "asiento-disponible";
        public string Identificador => $"F{Fila:D2}-A{Numero:D2}";
    }
}
