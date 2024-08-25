using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
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

        // Método para obtener los roles de un usuario
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
