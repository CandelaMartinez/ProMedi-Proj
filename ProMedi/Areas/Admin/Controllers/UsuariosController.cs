using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models.ViewModels;
using System.Security.Claims;

namespace ProMedi.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsuariosController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //opcion 1: obtener todos los usuarios
            //return View(_unitOfWork.Usuario.GetAll());

            //opcion 2: obtener todos los users menos el que esta logueado
            var claimsIndentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIndentity.FindFirst(ClaimTypes.NameIdentifier);
            // Recupera todos los usuarios excepto el que está logueado
            var usuarios = _unitOfWork.Usuario.GetAll(u => u.Id != usuarioActual.Value).ToList();

            // Crea una lista para almacenar los usuarios junto con sus roles
            var userRoleList = new List<UserWithRoleViewModel>();

            foreach (var user in usuarios)
            {

                // Obtén los roles del usuario de manera independiente para cada usuario
                var roles = await _unitOfWork.Usuario.GetUserRolesAsync(user);

                // Combina los roles en una cadena separada por comas
                var roleName = roles.Any() ? string.Join(", ", roles) : "No Roles";

                // Crea un nuevo UserWithRoleViewModel para el usuario actual
                var userRoleViewModel = new UserWithRoleViewModel
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Role = roleName, // Asigna los roles del usuario
                    LockoutEnd = user.LockoutEnd // Asigna LockoutEnd
                };

                // Agrega el usuario y su rol a la lista de usuarios
                userRoleList.Add(userRoleViewModel);
            }

            // Pasa la lista de usuarios con roles a la vista
            return View(userRoleList);
        }

        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            _unitOfWork.Usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _unitOfWork.Usuario.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
