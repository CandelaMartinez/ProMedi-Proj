using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.Models
{
    public class Publicacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Debes agregar un nombre")]
        [Display(Name ="Nombre de la Publicacion")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debes agregar la descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de publicacion")]
        public string FechaCreacion { get; set; }
        
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }


        //Entity Framework code first: relacion entre categoria y publicacion
        //la publicacion debe enviar siempre un id de categoria
        [Required(ErrorMessage = "La categoria es obligatoria")]
        public int CategoriaId { get; set; }

        //cual es el campo de Publicacion que va a estar relacionado con el modelo Categoria
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }


}

