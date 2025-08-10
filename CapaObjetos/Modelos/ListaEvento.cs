using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.Modelos
{
    public partial class ListaEvento
    {
        public ListaEvento()
        {
            Estado = true;
        }

        public int EventoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Ubicacion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Capacidad { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Estado { get; set; }

        public string? Banner { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioBase { get; set; }
        
        public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();
        public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
    }
}

