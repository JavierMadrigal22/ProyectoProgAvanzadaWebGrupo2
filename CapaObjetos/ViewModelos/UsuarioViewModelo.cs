using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace CapaObjetos.ViewModelos;

public partial class UsuarioViewModelo
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Debe seleccionar un rol.")]
    public int Rol { get; set; }

    public string? Telefono { get; set; }

    public bool Estado { get; set; }

    public IEnumerable<SelectListItem> RolesLista { get; set; } = new List<SelectListItem>();

}