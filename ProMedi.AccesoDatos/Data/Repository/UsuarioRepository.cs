using Microsoft.EntityFrameworkCore;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _context.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDB.LockoutEnd = DateTime.Now.AddDays(1);
            _context.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDB = _context.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDB.LockoutEnd = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
