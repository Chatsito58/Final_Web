using System;
using System.Collections.Generic;

namespace Proyecto_Final_Web.Models;

public partial class MetodosPago
{
    public int IdMetodoPago { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
