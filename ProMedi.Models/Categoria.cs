using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Ingrese el nombre de la categoria")]
        [Display(Name ="Nombre de la categoria")]
        public string Name { get; set; }
        [Range(1,100, ErrorMessage = "El valor debe ser entre 1 y 100")]
        public int? Orden { get; set;}

        //una categoria puede tener muchas publicaciones
        public ICollection<Publicacion> Publicacions { get; set; }
    }
}
