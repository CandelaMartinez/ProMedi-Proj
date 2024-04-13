using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProMedi.Models;

namespace ProMedi.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //agregar todos los modelos para que se creen la tablas con las migraciones
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }

        public DbSet<Carrousel> Carrouseles { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
