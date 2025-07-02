using System;
using System.Collections.Generic;

namespace Proyecto_Final_Web.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdCliente { get; set; }

    public int IdCancha { get; set; }

    public DateTime FechaHoraInicio { get; set; }

    public DateTime FechaHoraFin { get; set; }

    public int IdEstadoReserva { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public decimal Valor { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual Cancha IdCanchaNavigation { get; set; } = null!;

    public virtual Usuario IdClienteNavigation { get; set; } = null!;

    public virtual EstadosReserva IdEstadoReservaNavigation { get; set; } = null!;
}
