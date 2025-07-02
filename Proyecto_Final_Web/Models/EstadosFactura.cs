using System;
using System.Collections.Generic;

namespace Proyecto_Final_Web.Models;

public partial class EstadosFactura
{
    public int IdEstadoFactura { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
