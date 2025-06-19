using System;
using System.Collections.Generic;

namespace CapaObjetos.Modelos;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Rol { get; set; }

    public string? Telefono { get; set; }

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual Rol RolNavigation { get; set; } = null!;
}
