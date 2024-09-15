using Microsoft.AspNetCore.Identity;
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
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure IdentityRole
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.NormalizedName)
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.ConcurrencyStamp)
                    .HasColumnType("longtext");

                // Index for normalized name to allow efficient lookups
                entity.HasIndex(e => e.NormalizedName)
                    .HasDatabaseName("RoleNameIndex")
                    .IsUnique();
            });

            // Configure ApplicationUser
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.NormalizedUserName)
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.NormalizedEmail)
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.PasswordHash)
                    .HasColumnType("longtext");

                entity.Property(e => e.SecurityStamp)
                    .HasColumnType("longtext");

                entity.Property(e => e.ConcurrencyStamp)
                    .HasColumnType("longtext");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("longtext");

                // Custom properties
                entity.Property(e => e.Pais)
                    .HasColumnType("longtext");

                entity.Property(e => e.Ciudad)
                    .HasColumnType("longtext");

                entity.Property(e => e.Direccion)
                    .HasColumnType("longtext");

                // Index for normalized username to allow efficient lookups
                entity.HasIndex(e => e.NormalizedUserName)
                    .HasDatabaseName("UserNameIndex")
                    .IsUnique();
            });

            // Configure IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(e => e.LoginProvider)
                    .HasMaxLength(128)
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.ProviderKey)
                    .HasMaxLength(128)
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.ProviderDisplayName)
                    .HasColumnType("longtext");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            // Configure IdentityUserToken
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LoginProvider)
                    .HasMaxLength(128)
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.Value)
                    .HasColumnType("longtext");

                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            // Configure IdentityUserClaim
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ClaimType)
                    .HasColumnType("longtext");

                entity.Property(e => e.ClaimValue)
                    .HasColumnType("longtext");
            });

            // Configure IdentityRoleClaim
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RoleId)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ClaimType)
                    .HasColumnType("longtext");

                entity.Property(e => e.ClaimValue)
                    .HasColumnType("longtext");
            });

            // Configure IdentityUserRole
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            // Configure your custom entities

            // Publicacion
            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasColumnType("longtext");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("longtext");

                entity.Property(e => e.UrlImagen)
                    .HasColumnType("longtext");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("longtext");
            });

            // Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnType("longtext");
            });

            // Carrousel
            modelBuilder.Entity<Carrousel>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasColumnType("longtext");

                entity.Property(e => e.UrlImagen)
                    .HasColumnType("longtext");

                entity.Property(e => e.Estado)
                    .HasColumnType("tinyint(1)")
                    .IsRequired();
            });

        }
    }
}
