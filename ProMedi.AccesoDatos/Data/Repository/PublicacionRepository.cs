using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class PublicacionRepository : Repository<Publicacion>, IPublicacionRepository
    {
        private readonly ApplicationDbContext _context;
        public PublicacionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        //recibimos el objeto desde el formulario
        public void Update(Publicacion publicacion)
        {
            var objToUpdate = _context.Publicaciones.FirstOrDefault(s => s.Id == publicacion.Id);
            objToUpdate.Nombre = publicacion.Nombre;
            objToUpdate.Descripcion = publicacion.Descripcion;
            objToUpdate.UrlImagen = publicacion.UrlImagen;
            objToUpdate.CategoriaId = publicacion.CategoriaId;

           // _context.SaveChanges();

        }
       
    }
}
