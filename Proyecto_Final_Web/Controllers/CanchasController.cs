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
    public class CanchasController : Controller
    {
        private readonly BDCanchasContext _context;

        public CanchasController(BDCanchasContext context)
        {
            _context = context;
        }

        // GET: Canchas
        public async Task<IActionResult> Index()
        {
            var bDCanchasContext = _context.Canchas;
            return View(await bDCanchasContext.ToListAsync());
        }

        // GET: Canchas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancha = await _context.Canchas
                .FirstOrDefaultAsync(m => m.IdCancha == id);
            if (cancha == null)
            {
                return NotFound();
            }

            return View(cancha);
        }

        // GET: Canchas/Create
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Canchas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Create([Bind("IdCancha,Nombre,Descripcion,Estado")] Cancha cancha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cancha);
        }

        // GET: Canchas/Edit/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancha = await _context.Canchas.FindAsync(id);
            if (cancha == null)
            {
                return NotFound();
            }
            return View(cancha);
        }

        // POST: Canchas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int id, [Bind("IdCancha,Nombre,Descripcion,Estado")] Cancha cancha)
        {
            if (id != cancha.IdCancha)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanchaExists(cancha.IdCancha))
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
            return View(cancha);
        }

        // GET: Canchas/Delete/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancha = await _context.Canchas
                .FirstOrDefaultAsync(m => m.IdCancha == id);
            if (cancha == null)
            {
                return NotFound();
            }

            return View(cancha);
        }

        // POST: Canchas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancha = await _context.Canchas.FindAsync(id);
            if (cancha != null)
            {
                _context.Canchas.Remove(cancha);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CanchaExists(int id)
        {
            return _context.Canchas.Any(e => e.IdCancha == id);
        }
    }
}
