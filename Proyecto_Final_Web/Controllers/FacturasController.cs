using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final_Web.Models;
using System.Security.Claims;

namespace Proyecto_Final_Web.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class FacturasController : Controller
    {
        private readonly BDCanchasContext _context;

        public FacturasController(BDCanchasContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            IQueryable<Factura> query = _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdEstadoFacturaNavigation)
                .Include(f => f.IdMetodoPagoNavigation)
                .Include(f => f.IdReservaNavigation);

            // Clientes solo ven sus propias facturas
            if (userRole == 4) // Cliente
            {
                query = query.Where(f => f.IdCliente == userId);
            }

            return View(await query.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            var factura = await _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdEstadoFacturaNavigation)
                .Include(f => f.IdMetodoPagoNavigation)
                .Include(f => f.IdReservaNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            // Clientes solo pueden ver sus propias facturas
            if (userRole == 4 && factura.IdCliente != userId)
            {
                return Forbid();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto");
            ViewData["IdEstadoFactura"] = new SelectList(_context.EstadosFacturas, "IdEstadoFactura", "Nombre");
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodosPagos, "IdMetodoPago", "Nombre");
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva");
            return View();
        }

        // POST: Facturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Create([Bind("IdFactura,IdCliente,IdReserva,IdEstadoFactura,IdMetodoPago,Subtotal,Impuestos,Total,FechaEmision")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto", factura.IdCliente);
            ViewData["IdEstadoFactura"] = new SelectList(_context.EstadosFacturas, "IdEstadoFactura", "Nombre", factura.IdEstadoFactura);
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodosPagos, "IdMetodoPago", "Nombre", factura.IdMetodoPago);
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", factura.IdReserva);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto", factura.IdCliente);
            ViewData["IdEstadoFactura"] = new SelectList(_context.EstadosFacturas, "IdEstadoFactura", "Nombre", factura.IdEstadoFactura);
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodosPagos, "IdMetodoPago", "Nombre", factura.IdMetodoPago);
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", factura.IdReserva);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,IdCliente,IdReserva,IdEstadoFactura,IdMetodoPago,Subtotal,Impuestos,Total,FechaEmision")] Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.IdFactura))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto", factura.IdCliente);
            ViewData["IdEstadoFactura"] = new SelectList(_context.EstadosFacturas, "IdEstadoFactura", "Nombre", factura.IdEstadoFactura);
            ViewData["IdMetodoPago"] = new SelectList(_context.MetodosPagos, "IdMetodoPago", "Nombre", factura.IdMetodoPago);
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", factura.IdReserva);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.IdEstadoFacturaNavigation)
                .Include(f => f.IdMetodoPagoNavigation)
                .Include(f => f.IdReservaNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.IdFactura == id);
        }
    }
}
