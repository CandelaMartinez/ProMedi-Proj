using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.Models.ViewModels
{
    public class HomeViewModel
    {
        //necesito crear un viewModel porque le paso a la vista data de dos modelos diferentes
        public IEnumerable<Carrousel> Carrouseles { get; set;}
        public IEnumerable<Publicacion> Publicaciones { get; set;}
        public List<Categoria> Categorias { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
    }
}
