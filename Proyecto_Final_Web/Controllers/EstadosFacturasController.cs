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
    [Authorize(Roles = "Empleado,Administrador,Gerente")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class EstadosFacturasController : Controller
    {
        private readonly BDCanchasContext _context;

        public EstadosFacturasController(BDCanchasContext context)
        {
            _context = context;
        }

        // GET: EstadosFacturas
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadosFacturas.ToListAsync());
        }

        // GET: EstadosFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosFactura = await _context.EstadosFacturas
                .FirstOrDefaultAsync(m => m.IdEstadoFactura == id);
            if (estadosFactura == null)
            {
                return NotFound();
            }

            return View(estadosFactura);
        }

        // GET: EstadosFacturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadosFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoFactura,Nombre,Descripcion")] EstadosFactura estadosFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadosFactura);
        }

        // GET: EstadosFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosFactura = await _context.EstadosFacturas.FindAsync(id);
            if (estadosFactura == null)
            {
                return NotFound();
            }
            return View(estadosFactura);
        }

        // POST: EstadosFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoFactura,Nombre,Descripcion")] EstadosFactura estadosFactura)
        {
            if (id != estadosFactura.IdEstadoFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadosFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosFacturaExists(estadosFactura.IdEstadoFactura))
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
            return View(estadosFactura);
        }

        // GET: EstadosFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosFactura = await _context.EstadosFacturas
                .FirstOrDefaultAsync(m => m.IdEstadoFactura == id);
            if (estadosFactura == null)
            {
                return NotFound();
            }

            return View(estadosFactura);
        }

        // POST: EstadosFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosFactura = await _context.EstadosFacturas.FindAsync(id);
            if (estadosFactura != null)
            {
                _context.EstadosFacturas.Remove(estadosFactura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosFacturaExists(int id)
        {
            return _context.EstadosFacturas.Any(e => e.IdEstadoFactura == id);
        }
    }
}
