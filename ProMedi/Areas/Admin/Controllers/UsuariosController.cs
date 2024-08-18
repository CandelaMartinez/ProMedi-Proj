using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using System.Security.Claims;

namespace ProMedi.Areas.Admin.Controllers
{
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
        public IActionResult Index()
        {
            //opcion 1: obtener todos los usuarios
            //return View(_unitOfWork.Usuario.GetAll());

            //opcion 2: obtener todos los users menos el que esta logueado
            var claimsIndentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIndentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_unitOfWork.Usuario.GetAll(u => u.Id != usuarioActual.Value));
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
