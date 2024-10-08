using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using ProMedi.Models.ViewModels;
using System.Diagnostics;
using System.Drawing.Printing;

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

        //pagina de inicio sin paginacion
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    //instancio el viewModel que le pasare a la vista 
        //    HomeViewModel homeVM = new HomeViewModel()
        //    {
        //        Carrouseles = _unitOfWork.Carrousel.GetAll(),
        //        Publicaciones = _unitOfWork.Publicacion.GetAll()
        //    };

        //    //envio esta variable para indicar que estamos en la pagina Home
        //    //porque solo necesito mostrar el carrousel en la pagina Home
        //    //y como puse el carrousel en el layout principal, necesito especificarlo
        //    ViewBag.IsHome = true;

        //    return View(homeVM);
        //}

        //pagina de inicio con paginacion
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            var publicacions = _unitOfWork.Publicacion.AsQueryable();


            // Paginar los resultados
            var paginatedEntries = publicacions.Skip((page - 1) * pageSize).Take(pageSize);

            var categorias = _unitOfWork.Categoria.GetAll().ToList();

            //instancio el viewModel que le pasare a la vista 
            HomeViewModel homeVM = new HomeViewModel()
            {
                Carrouseles = _unitOfWork.Carrousel.GetAll(),
                Publicaciones = paginatedEntries.ToList(),
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(publicacions.Count()/(double)pageSize),
                Categorias = categorias
            };

            //envio esta variable para indicar que estamos en la pagina Home
            //porque solo necesito mostrar el carrousel en la pagina Home
            //y como puse el carrousel en el layout principal, necesito especificarlo
            ViewBag.IsHome = true;

            return View(homeVM);
        }




        //Para buscador
        [HttpGet]
        public IActionResult ResultadoBusqueda(string searchString, int page = 1, int pageSize = 3)
        {
            var publicacions = _unitOfWork.Publicacion.AsQueryable();

            // Filtrar por t�tulo si hay un t�rmino de b�squeda
            if (!string.IsNullOrEmpty(searchString))
            {
                publicacions = publicacions.Where(e => e.Nombre.Contains(searchString));
            }

            // Paginar los resultados
            var paginatedEntries = publicacions.Skip((page - 1) * pageSize).Take(pageSize);

            // Crear el modelo para la vista
            var model = new ListaPaginada<Publicacion>(paginatedEntries.ToList(), publicacions.Count(), page, pageSize, searchString);
            return View(model);
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

        [HttpGet]
        public JsonResult ObtenerPublicaciones(int id)
        {
            var publicaciones = _unitOfWork.Publicacion.GetAll().Where(p => p.CategoriaId == id).ToList();
            return Json(publicaciones);
        }
    }
}
