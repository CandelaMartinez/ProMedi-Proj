using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;

namespace ProMedi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublicacionesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //accedo al UnitOfWork para poder acceder a los repositorios
        public PublicacionesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Publicacion.GetAll(includeProperties: "Categoria") });
        }
        
        #endregion
    }
}
