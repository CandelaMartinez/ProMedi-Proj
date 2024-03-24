using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProMedi.Models.ViewModels
{
    public class PublicacionViewModel
    {
        public Publicacion Publicacion { get; set; }

        //me trae la lista de categorias para el dropdown en la creacion de publicaciones
        public  IEnumerable<SelectListItem> ListaCategorias { get; set; }

    }
}
