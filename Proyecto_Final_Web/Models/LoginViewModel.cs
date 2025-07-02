using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final_Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

        [Display(Name = "Recordar mis datos")]
        public bool Recordarme { get; set; }
    }
}