using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //acceso a los datos
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categoria = new CategoriaRepository(context);
            Publicacion = new PublicacionRepository(context);
            Carrousel = new CarrouselRepository(context);
            Usuario = new UsuarioRepository(context);
        }

        public ICategoriaRepository Categoria { get; private set; }
        public IPublicacionRepository Publicacion { get; private set; }

        public ICarrouselRepository Carrousel { get; private set; }

        public IUsuarioRepository Usuario { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
