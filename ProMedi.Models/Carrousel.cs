using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.Models
{
    public class Carrousel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del carrousel")]
        [Display(Name = "Nombre carrousel")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Ingrese el Estado del carrousel")]
        public bool Estado { get; set; }



        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }
    }
}
