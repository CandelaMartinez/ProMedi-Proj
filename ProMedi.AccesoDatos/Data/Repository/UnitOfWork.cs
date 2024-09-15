using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            Categoria = new CategoriaRepository(_context);
            Publicacion = new PublicacionRepository(_context);
            Carrousel = new CarrouselRepository(_context);
            Usuario = new UsuarioRepository(_context, _userManager);
            Paciente = new PacienteRepository(_context);
            AnaliticaOrina = new AnaliticaOrinaRepository(_context);
        }

        public ICategoriaRepository Categoria { get; private set; }
        public IPublicacionRepository Publicacion { get; private set; }

        public ICarrouselRepository Carrousel { get; private set; }

        public IUsuarioRepository Usuario { get; private set; }
        public IPacienteRepository Paciente { get; private set; }

        public IAnaliticaOrinaRepository AnaliticaOrina { get; private set; }


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
