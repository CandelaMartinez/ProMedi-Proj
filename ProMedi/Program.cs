using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProMedi.AccesoDatos.Data;
using ProMedi.AccesoDatos.Data.Repository;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using ProMedi.Models;
using ProMedi.AccesoDatos.Data.Inicializador;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

//agrego unitOfWork al contenedor IoC de inyeccion de dependencias
//para poder usarlo
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//inicializador, siembra de datos, primer paso
builder.Services.AddScoped<IInitializadorBD, InicializadorBD>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

//siembra de datos, metodo que ejecuta 
SiembraDatos();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


void SiembraDatos()
{
    using (var scope = app.Services.CreateScope())
    {
        var inicializadorDB = scope.ServiceProvider.GetRequiredService<IInitializadorBD>();
        inicializadorDB.Inicializar();
    }
}