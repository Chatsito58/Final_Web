using System;
using System.Collections.Generic;

namespace Proyecto_Final_Web.Models;

public partial class Tarifa
{
    public int IdTarifa { get; set; }

    public int? IdCancha { get; set; }

    public string DiaSemana { get; set; } = null!;

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public decimal Valor { get; set; }

    public string? Descripcion { get; set; }

    public bool Activa { get; set; }

    public virtual Cancha? IdCanchaNavigation { get; set; }
}
