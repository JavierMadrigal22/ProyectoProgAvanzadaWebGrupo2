using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaObjetos.ViewModelos
{
    public class RegisterViewModel
    {
        [Required] public string Nombre { get; set; } = null!;
        public string? Apellido { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = null!;

        public string? Telefono { get; set; }
    }
}
