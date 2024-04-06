using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;

namespace ProMedi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarrouselesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarrouselesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //muestra una vista
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
