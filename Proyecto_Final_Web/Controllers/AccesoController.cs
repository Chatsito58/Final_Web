using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Web.Logica;
using Proyecto_Final_Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Proyecto_Final_Web.Controllers
{
    public class AccesoController : Controller
    {
        private readonly Logica_Usuarios _logicaUsuarios;

        public AccesoController(Logica_Usuarios logicaUsuarios)
        {
            _logicaUsuarios = logicaUsuarios;
        }

        public IActionResult Index()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Mensaje"] = "Por favor complete todos los campos requeridos";
                return View(model);
            }

            var usuarioAutenticado = await _logicaUsuarios.AutenticarUsuario(model.Correo, model.Contrasena);

            if (usuarioAutenticado == null)
            {
                ViewData["Mensaje"] = "Credenciales incorrectas";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioAutenticado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuarioAutenticado.NombreCompleto),
                new Claim(ClaimTypes.Email, usuarioAutenticado.Correo),
                new Claim("Telefono", usuarioAutenticado.Telefono ?? string.Empty),
                new Claim("IdRol", usuarioAutenticado.IdRol.ToString())
            };

            // Agregar claim de rol basado en el nombre del rol
            if (usuarioAutenticado.IdRolNavigation != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, usuarioAutenticado.IdRolNavigation.Nombre));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = model.Recordarme // Opción "Recordarme"
                });

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
        }
    }
}