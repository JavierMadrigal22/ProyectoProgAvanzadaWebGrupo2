using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.Modelos
{
    public partial class Asiento
    {
        public int AsientoId { get; set; }
        
        [Required]
        public int EventoId { get; set; }
        
        [Required]
        [Range(1, 10, ErrorMessage = "La fila debe estar entre 1 y 10")]
        public int Fila { get; set; }
        
        [Required]
        [Range(1, 10, ErrorMessage = "El número de asiento debe estar entre 1 y 10")]
        public int Numero { get; set; }
        
        public bool EstaOcupado { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        
        public virtual ListaEvento Evento { get; set; } = null!;
        public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
    }
}
