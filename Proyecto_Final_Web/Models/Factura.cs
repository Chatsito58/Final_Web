using System;
using System.Collections.Generic;

namespace Proyecto_Final_Web.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int IdReserva { get; set; }

    public int IdCliente { get; set; }

    public int IdEstadoFactura { get; set; }

    public int? IdMetodoPago { get; set; }

    public decimal Total { get; set; }

    public DateTime FechaEmision { get; set; } = DateTime.Now;

    public virtual Usuario IdClienteNavigation { get; set; } = null!;

    public virtual EstadosFactura IdEstadoFacturaNavigation { get; set; } = null!;

    public virtual MetodosPago? IdMetodoPagoNavigation { get; set; }

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
