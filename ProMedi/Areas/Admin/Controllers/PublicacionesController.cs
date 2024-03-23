using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models.ViewModels;

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

        [HttpGet]
        public IActionResult Create()
        {
            PublicacionViewModel publicacionVM = new PublicacionViewModel()
            {
                Publicacion = new ProMedi.Models.Publicacion(),
                ListaCategorias = _unitOfWork.Categoria.GetListaCategorias()
            };

            return View(publicacionVM);
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
