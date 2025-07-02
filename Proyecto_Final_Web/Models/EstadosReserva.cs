using System;
using System.Collections.Generic;

namespace Proyecto_Final_Web.Models;

public partial class EstadosReserva
{
    public int IdEstadoReserva { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
