using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.Models
{
    public class AnaliticaOrina
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int? Leucositos { get; set; }
        public int? Nitrito { get; set; }
        public int? Hemoglobina { get; set; }
        public int? Proteina { get; set; }
        public int? Creatinina { get; set; }
        public int? Albumina { get; set; }

        [Required(ErrorMessage = "El paciente es obligatorio")]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

    }
}
