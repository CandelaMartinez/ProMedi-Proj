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
        public void Update(Categoria categoria)
        {
            var objToUpdate = _context.Categorias.FirstOrDefault(s => s.Id == categoria.Id);
            objToUpdate.Name = categoria.Name;
            objToUpdate.Orden = categoria.Orden;

            _context.SaveChanges();

        }
       
    }
}
