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
    public class ReservasController : Controller
    {
        private readonly BDCanchasContext _context;

        public ReservasController(BDCanchasContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            IQueryable<Reserva> query = _context.Reservas
                .Include(r => r.IdCanchaNavigation)
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEstadoReservaNavigation);

            // Clientes solo ven sus propias reservas
            if (userRole == 4) // Cliente
            {
                query = query.Where(r => r.IdCliente == userId);
            }

            return View(await query.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            var reserva = await _context.Reservas
                .Include(r => r.IdCanchaNavigation)
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEstadoReservaNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            // Clientes solo pueden ver sus propias reservas
            if (userRole == 4 && reserva.IdCliente != userId)
            {
                return Forbid();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        [Authorize(Roles = "Cliente,Empleado,Administrador,Gerente")]
        public IActionResult Create()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            ViewData["IdCancha"] = new SelectList(_context.Canchas
                .Where(c => c.Activa), "IdCancha", "Nombre");
            
            // Solo empleados y superiores pueden seleccionar clientes
            if (userRole >= 3) // Empleado o superior
            {
                ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto");
            }
            else
            {
                // Clientes solo pueden crear reservas para sí mismos
                ViewData["IdCliente"] = new SelectList(_context.Usuarios.Where(u => u.IdUsuario == userId), "IdUsuario", "NombreCompleto");
            }
            
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente,Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Create([Bind("IdReserva,IdCliente,IdCancha,FechaHoraInicio,FechaHoraFin,IdEstadoReserva,Valor")] Reserva reserva)
        {
            ModelState.Remove("IdCanchaNavigation");
            ModelState.Remove("IdClienteNavigation");
            ModelState.Remove("IdEstadoReservaNavigation");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            // Clientes solo pueden crear reservas para sí mismos
            if (userRole == 4 && reserva.IdCliente != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                reserva.FechaCreacion = DateTime.Now;
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCancha"] = new SelectList(_context.Canchas, "IdCancha", "Nombre", reserva.IdCancha);
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto", reserva.IdCliente);
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "Nombre", reserva.IdEstadoReserva);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdCancha"] = new SelectList(_context.Canchas, "IdCancha", "Nombre");
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto");
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "Nombre");
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdCliente,IdCancha,FechaHoraInicio,FechaHoraFin,IdEstadoReserva,Valor")] Reserva reserva)
        {
            ModelState.Remove("IdCanchaNavigation");
            ModelState.Remove("IdClienteNavigation");
            ModelState.Remove("IdEstadoReservaNavigation");

            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            // Obtener la reserva existente para preservar la FechaCreacion original
            var reservaExistente = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(r => r.IdReserva == id);
            if (reservaExistente != null)
            {
                reserva.FechaCreacion = reservaExistente.FechaCreacion;
            }
            else
            {
                reserva.FechaCreacion = DateTime.Now;
            }

            // Validar las fechas
            if (reserva.FechaHoraInicio < new DateTime(1753, 1, 1) ||
                reserva.FechaHoraFin < new DateTime(1753, 1, 1))
            {
                ModelState.AddModelError("", "Las fechas deben ser posteriores al 1/1/1753");
                return View(reserva);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["IdCancha"] = new SelectList(_context.Canchas, "IdCancha", "Nombre", reserva.IdCancha);
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreCompleto", reserva.IdCliente);
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "Nombre", reserva.IdEstadoReserva);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdCanchaNavigation)
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEstadoReservaNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}
