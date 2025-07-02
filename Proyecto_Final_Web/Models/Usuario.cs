using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final_Web.Models;

public partial class Usuario
{
    [Required]
    public int IdUsuario { get; set; }

    [Required]
    [Display(Name = "Rol")]
    public int IdRol { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreCompleto { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Correo { get; set; } = null!;

    [Required]
    [StringLength(255)]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = null!;

    [StringLength(20)]
    [Phone]
    public string? Telefono { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
