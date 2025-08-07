using System;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.Modelos
{
    public partial class Boleto
    {
        public int BoletoId { get; set; }
        
        [Required]
        public int EventoId { get; set; }
        
        [Required]
        public int UsuarioId { get; set; }
        
        [Required]
        public int AsientoId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }
        
        public DateTime FechaCompra { get; set; }
        
        public bool Estado { get; set; }
        
        public virtual ListaEvento Evento { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Asiento Asiento { get; set; } = null!;
    }
}
