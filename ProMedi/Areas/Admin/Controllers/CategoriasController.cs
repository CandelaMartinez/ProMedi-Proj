using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;

namespace ProMedi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //muestra una vista
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //POST: trae informacion de un formulario
        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = _unitOfWork.Categoria.GetAll()});
        }
        #endregion
    }
}
