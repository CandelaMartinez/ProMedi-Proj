using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Ingrese el nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el apellido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Ingrese el numero")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha")]
        public DateTime FechaNacimiento { get; set; }
        public bool IsArchived { get; set; }

    }
}
