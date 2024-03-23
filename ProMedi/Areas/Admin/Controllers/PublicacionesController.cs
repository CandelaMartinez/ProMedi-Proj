using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models.ViewModels;

namespace ProMedi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublicacionesController : Controller
    {
        //accedo al UnitOfWork para poder acceder a los repositorios
        private readonly IUnitOfWork _unitOfWork;
        //acceder a las carpetas de root
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public PublicacionesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;   
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublicacionViewModel publicacionVm)
        {
          if(publicacionVm.Publicacion != null) 
          {
                string rutaInicial = _webHostEnvironment.WebRootPath;
                //accedo a los archivos del formulario
                var archivos = HttpContext.Request.Form.Files;
                //si el id es 0, quiere decir que es una entrada nueva
                if(publicacionVm.Publicacion.Id == 0 && archivos.Count()>0)
                {
                    //entero de 128 bits para un identificador unico
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaInicial, @"imagenes\publicaciones");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        //copio en memoria
                        archivos[0].CopyTo(fileStreams);
                    }
                    //guarda la ruta de la imagen en el campo urlImagen
                    publicacionVm.Publicacion.UrlImagen = @"\imagenes\publicaciones\" + nombreArchivo + extension;
                    publicacionVm.Publicacion.FechaCreacion = DateTime.Now.ToString();

                    //accedo a la unitOfWork para acceder al repositorio PublicacionRepository y poder guardar la nueva publicacion
                    _unitOfWork.Publicacion.Add(publicacionVm.Publicacion);
                    _unitOfWork.Save();

                    
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Agregar una imagen");
                }
          }
            publicacionVm.ListaCategorias = _unitOfWork.Categoria.GetListaCategorias();
            return View(publicacionVm);
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
