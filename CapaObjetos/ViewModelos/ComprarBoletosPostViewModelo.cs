using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaObjetos.ViewModelos
{
    public class ComprarBoletosPostViewModelo
    {
        [Required(ErrorMessage = "El evento es requerido")]
        public int EventoId { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar al menos un asiento")]
        public List<int> AsientosSeleccionados { get; set; } = new List<int>();
        
        public int UsuarioId { get; set; }
    }
} 