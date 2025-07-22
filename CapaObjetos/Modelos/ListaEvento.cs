using System;
using System.Collections.Generic;

namespace CapaObjetos.Modelos
{
    public partial class ListaEvento
    {
        public int EventoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Ubicacion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Capacidad { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Estado { get; set; }

    }
}

