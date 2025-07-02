using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final_Web.Models;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;

namespace Proyecto_Final_Web.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class UsuariosController : Controller
    {
        private readonly BDCanchasContext _context;

        public UsuariosController(BDCanchasContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Index()
        {
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            IQueryable<Usuario> query = _context.Usuarios.Include(u => u.IdRolNavigation);

            // Empleados solo ven clientes
            if (userRole == 3) // Empleado
            {
                query = query.Where(u => u.IdRol == 4); // Solo clientes
            }
            // Administradores ven empleados y clientes
            else if (userRole == 2) // Administrador
            {
                query = query.Where(u => u.IdRol >= 3); // Empleados y clientes
            }
            // Gerentes ven todo

            return View(await query.ToListAsync());
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verificar permisos según rol
            if (userRole == 3 && usuario.IdRol != 4) // Empleado solo puede ver clientes
            {
                return Forbid();
            }
            else if (userRole == 2 && usuario.IdRol < 3) // Administrador no puede ver gerentes
            {
                return Forbid();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public IActionResult Create()
        {
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            // Filtrar roles según el nivel del usuario
            IQueryable<Role> rolesQuery = _context.Roles;
            
            if (userRole == 3) // Empleado solo puede crear clientes
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol == 4);
            }
            else if (userRole == 2) // Administrador puede crear empleados y clientes
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol >= 3);
            }
            // Gerente puede crear cualquier rol

            ViewData["IdRol"] = new SelectList(rolesQuery, "IdRol", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Create([Bind("IdRol,NombreCompleto,Correo,Contrasena,Telefono")] Usuario usuario)
        {
            ModelState.Remove("IdRolNavigation");
            
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            // Verificar permisos para crear el rol especificado
            if (userRole == 3 && usuario.IdRol != 4) // Empleado solo puede crear clientes
            {
                return Forbid();
            }
            else if (userRole == 2 && usuario.IdRol < 3) // Administrador no puede crear gerentes
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                // Hashear la contraseña con SHA256
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(usuario.Contrasena));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    usuario.Contrasena = builder.ToString();
                }
                usuario.FechaRegistro = DateTime.Now; // Asigna la fecha actual
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            // Recrear la lista de roles para la vista
            IQueryable<Role> rolesQuery = _context.Roles;
            if (userRole == 3)
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol == 4);
            }
            else if (userRole == 2)
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol >= 3);
            }
            
            ViewData["IdRol"] = new SelectList(rolesQuery, "IdRol", "Nombre", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verificar permisos para editar
            if (userRole == 3 && usuario.IdRol != 4) // Empleado solo puede editar clientes
            {
                return Forbid();
            }
            else if (userRole == 2 && usuario.IdRol < 3) // Administrador no puede editar gerentes
            {
                return Forbid();
            }

            // Filtrar roles según el nivel del usuario
            IQueryable<Role> rolesQuery = _context.Roles;
            if (userRole == 3)
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol == 4);
            }
            else if (userRole == 2)
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol >= 3);
            }

            ViewData["IdRol"] = new SelectList(rolesQuery, "IdRol", "Nombre");
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,IdRol,NombreCompleto,Correo,Contrasena,Telefono,FechaRegistro")] Usuario usuario)
        {
            ModelState.Remove("IdRolNavigation");
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            // Verificar permisos para editar
            if (userRole == 3 && usuario.IdRol != 4) // Empleado solo puede editar clientes
            {
                return Forbid();
            }
            else if (userRole == 2 && usuario.IdRol < 3) // Administrador no puede editar gerentes
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
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
            
            // Recrear la lista de roles para la vista
            IQueryable<Role> rolesQuery = _context.Roles;
            if (userRole == 3)
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol == 4);
            }
            else if (userRole == 2)
            {
                rolesQuery = rolesQuery.Where(r => r.IdRol >= 3);
            }
            
            ViewData["IdRol"] = new SelectList(rolesQuery, "IdRol", "Nombre");
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verificar permisos para eliminar
            if (userRole == 3 && usuario.IdRol != 4) // Empleado solo puede eliminar clientes
            {
                return Forbid();
            }
            else if (userRole == 2 && usuario.IdRol < 3) // Administrador no puede eliminar gerentes
            {
                return Forbid();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado,Administrador,Gerente")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRole = int.Parse(User.FindFirst("IdRol")?.Value ?? "0");

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                // Verificar permisos para eliminar
                if (userRole == 3 && usuario.IdRol != 4) // Empleado solo puede eliminar clientes
                {
                    return Forbid();
                }
                else if (userRole == 2 && usuario.IdRol < 3) // Administrador no puede eliminar gerentes
                {
                    return Forbid();
                }

                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
