using System;
using System.Collections.Generic;

namespace Proyecto_Final_Web.Models;

public partial class Cancha
{
    public int IdCancha { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoCancha { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string? Descripcion { get; set; }

    public bool Activa { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Tarifa> Tarifas { get; set; } = new List<Tarifa>();
}
