using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProMedi.Models;
using ProMedi.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Inicializador
{
    public class InicializadorBD : IInitializadorBD
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InicializadorBD(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }

        public void Inicializar()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0) { 
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex) { 
            }

            //si hay algun rol ya creado, que lo devuelva
            if (_context.Roles.Any(ro => ro.Name == Constantes.Administrador)) return;

            //creacion de roles
            _roleManager.CreateAsync(new IdentityRole(Constantes.Administrador)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constantes.Medico)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Constantes.NoRegistrado)).GetAwaiter().GetResult();

            //creacion del usuario inicial administrador
            _userManager.CreateAsync(new ApplicationUser 
            {
                UserName = "candelaAdmin@gmail.com",
                Email = "candelaAdmin@gmail.com",
                EmailConfirmed = false,
                Nombre = "CandelaAdmin",
                Ciudad = "Admin",
                Direccion = "Admin",
                Pais = "Admin",
                LockoutEnabled = true,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false

            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUser usuario = _context.ApplicationUser.Where(us => us.Email == "candelaAdmin@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(usuario, Constantes.Administrador).GetAwaiter().GetResult();


        }
    }
}
