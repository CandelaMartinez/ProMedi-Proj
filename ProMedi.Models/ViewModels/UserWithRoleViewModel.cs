using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.Models.ViewModels
{
    public class UserWithRoleViewModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Para almacenar el nombre del rol
        public DateTimeOffset? LockoutEnd { get; set; } // Agregar esta propiedad
    }
}
