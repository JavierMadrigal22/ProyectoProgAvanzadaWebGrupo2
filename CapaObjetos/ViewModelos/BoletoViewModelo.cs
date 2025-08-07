using System;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.ViewModelos
{
    public class BoletoViewModelo
    {
        public int BoletoId { get; set; }
        
        [Required(ErrorMessage = "El evento es requerido")]
        public int EventoId { get; set; }
        
        [Required(ErrorMessage = "El usuario es requerido")]
        public int UsuarioId { get; set; }
        
        [Required(ErrorMessage = "El asiento es requerido")]
        public int AsientoId { get; set; }
        
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }
        
        [Display(Name = "Fecha de Compra")]
        public DateTime FechaCompra { get; set; }
        
        public bool Estado { get; set; }
        
        public string NombreEvento { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string InformacionAsiento { get; set; } = string.Empty;
        
        public int FilaAsiento { get; set; }
        public int NumeroAsiento { get; set; }
    }
}
