using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
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
        //envia vista inicial
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //crea un viewModel con la lista de categorias para el desplegable y lo envia a la vista
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
        //recibe los datos del form de creacion
        //obtengo los datos que no recibo del form, datos calculados
        //los guardo en el repositorio desde unitOfWork
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

        //recibo el id desde el form de la publicacion a editar
        //busco esa publicacion en la base de datos
        //la guardo en el viewModel que envio a la vista asi la muestra
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            PublicacionViewModel publicacionVM = new PublicacionViewModel()
            {
                Publicacion = new ProMedi.Models.Publicacion(),
                ListaCategorias = _unitOfWork.Categoria.GetListaCategorias()
            };

            if (id != null)
            {
                publicacionVM.Publicacion = _unitOfWork.Publicacion.Get(id.GetValueOrDefault());
            }

            return View(publicacionVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PublicacionViewModel publicacionVM)
        {
            if (publicacionVM.Publicacion != null)
            {
                string rutaPrincipal = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                //obtengo el objecto de la base de datos
                var publicacionDesdeBd = _unitOfWork.Publicacion.Get(publicacionVM.Publicacion.Id);

                //case donde quiero reemplazar la imagen de la publicacion
                if (archivos.Count() > 0)
                {
                    //nueva imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\publicaciones");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    //quitaos la contrabarra a la ruta
                    var rutaImagen = Path.Combine(rutaPrincipal, publicacionDesdeBd.UrlImagen.TrimStart('\\'));
                    //valido que el archivo existe en la ruta
                    //si existe, la borro porque es la ruta antigua, asi no ocupa espacio en el server
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //subimos el nuevo archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    publicacionVM.Publicacion.UrlImagen = @"\imagenes\publicaciones\" + nombreArchivo + extension;
                    publicacionVM.Publicacion.FechaCreacion = DateTime.Now.ToString();

                    _unitOfWork.Publicacion.Update(publicacionVM.Publicacion);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //case: cuando no quiero reemplazar la imagen, uso la anterior
                    publicacionVM.Publicacion.UrlImagen = publicacionDesdeBd.UrlImagen;
                }

                _unitOfWork.Publicacion.Update(publicacionVM.Publicacion);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }

            publicacionVM.ListaCategorias = _unitOfWork.Categoria.GetListaCategorias();
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
