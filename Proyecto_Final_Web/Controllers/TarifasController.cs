using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final_Web.Models;

namespace Proyecto_Final_Web.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class TarifasController : Controller
    {
        private readonly BDCanchasContext _context;

        public TarifasController(BDCanchasContext context)
        {
            _context = context;
        }

        // GET: Tarifas
        public async Task<IActionResult> Index()
        {
            var bDCanchasContext = _context.Tarifas.Include(t => t.IdCanchaNavigation);
            return View(await bDCanchasContext.ToListAsync());
        }

        // GET: Tarifas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas
                .Include(t => t.IdCanchaNavigation)
                .FirstOrDefaultAsync(m => m.IdTarifa == id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        // GET: Tarifas/Create
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public IActionResult Create()
        {
            ViewData["IdCancha"] = new SelectList(_context.Canchas, "IdCancha", "Nombre");
            return View();
        }

        // POST: Tarifas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Create([Bind("IdTarifa,IdCancha,PrecioPorHora,Descripcion,Activa")] Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarifa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCancha"] = new SelectList(_context.Canchas, "IdCancha", "Nombre", tarifa.IdCancha);
            return View(tarifa);
        }

        // GET: Tarifas/Edit/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
            {
                return NotFound();
            }
            ViewData["IdCancha"] = new SelectList(_context.Canchas, "IdCancha", "Nombre", tarifa.IdCancha);
            return View(tarifa);
        }

        // POST: Tarifas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarifa,IdCancha,PrecioPorHora,Descripcion,Activa")] Tarifa tarifa)
        {
            if (id != tarifa.IdTarifa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarifa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifaExists(tarifa.IdTarifa))
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
            ViewData["IdCancha"] = new SelectList(_context.Canchas, "IdCancha", "Nombre", tarifa.IdCancha);
            return View(tarifa);
        }

        // GET: Tarifas/Delete/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas
                .Include(t => t.IdCanchaNavigation)
                .FirstOrDefaultAsync(m => m.IdTarifa == id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        // POST: Tarifas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa != null)
            {
                _context.Tarifas.Remove(tarifa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifaExists(int id)
        {
            return _context.Tarifas.Any(e => e.IdTarifa == id);
        }
    }
}
