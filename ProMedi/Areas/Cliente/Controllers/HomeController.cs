using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using ProMedi.Models.ViewModels;
using System.Diagnostics;

namespace ProMedi.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        //obtengo los datos desde el contenedor de trabajo que encapsula los repositorios
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //instancio el viewModel que le pasare a la vista 
            HomeViewModel homeVM = new HomeViewModel()
            {
                Carrouseles = _unitOfWork.Carrousel.GetAll(),
                Publicaciones = _unitOfWork.Publicacion.GetAll()
            };

            //envio esta variable para indicar que estamos en la pagina Home
            //porque solo necesito mostrar el carrousel en la pagina Home
            //y como puse el carrousel en el layout principal, necesito especificarlo
            ViewBag.IsHome = true;

            return View(homeVM);
        }

        //metodo llamado desde home.cshtml para mostrar los detalles de la publicacion en la card
        //recibe el id de la publicacion a buscar 
        [HttpGet]
        public IActionResult Details(int id) 
        {
            var publicacionDesdeDB = _unitOfWork.Publicacion.Get(id);
            return View(publicacionDesdeDB);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
