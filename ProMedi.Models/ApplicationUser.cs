using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProMedi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Nombre Obligatorio")]
        public string  Nombre { get; set; }

        [Required(ErrorMessage = "Direccion Obligatorio")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ciudad Obligatorio")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Pais Obligatorio")]
        public string Pais { get; set; }
    }
}
