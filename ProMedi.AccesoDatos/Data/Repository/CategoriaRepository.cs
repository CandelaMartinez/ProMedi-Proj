using Microsoft.AspNetCore.Mvc.Rendering;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //por cada categoria que tengo en mi db, creo un nuevo objeto SelectedListItem 
        //para mi dropdown del create con mi lista de categorias
        //muestro en el dropdown el nombre pero envio en el form el id, ya que eso 
        //es lo que guardo en mi db
        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _context.Categorias.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objToUpdate = _context.Categorias.FirstOrDefault(s => s.Id == categoria.Id);
            objToUpdate.Name = categoria.Name;
            objToUpdate.Orden = categoria.Orden;

            //_context.SaveChanges();

        }
       
    }
}
