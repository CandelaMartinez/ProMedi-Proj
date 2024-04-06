using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;

namespace ProMedi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarrouselesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarrouselesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        //muestra una vista
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //metodo create llamado desde el boton de la vista Index
        //devuelve la vista con el formulario para ingresar los valores para crear un carrousel
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //metodo al que llego luego de haber completado el formulario de create y apretar el boton crear
        //logica para acceder a los archivos subidos en el Formulario
        //agregarlos al proyecto
        //guardar el objeto nuevo en la base de datos a traves de la UnitOfWork y volver a Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Carrousel carrousel)
        {
            if (carrousel != null)
            {
                string rutaPrincipal = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (archivos.Count() > 0)
                {
                    //Nuevo carrousel
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\carrouseles");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    carrousel.UrlImagen = @"\imagenes\carrouseles\" + nombreArchivo + extension;

                    _unitOfWork.Carrousel.Add(carrousel);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }

            }

            return View(carrousel);
        }

        //metodo al que llego al hacer click en el boton editar en la vista index
        //recibe el id del registro a editar (enviado desde javascript)
        //por UnitOfWork obtengo el registro por id
        //lo devuelvo a en la vista Edit.cshtml para poder ser editado
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id != null)
            {
                var carrousel = _unitOfWork.Carrousel.Get(id.GetValueOrDefault());
                return View(carrousel);
            }

            return View();
        }

        //llego a este metodo cuando doy click al formulario edit.cshtml
        //recibo el objeto editado para insertar en la base de datos
        //primero obtengo los archivos del form
        //luego obtengo el objeto a editar de la base de datos
        //logica para subir la imagen en la carpeta del proyecto y extraer la url, adjudicar esa url al objeto
        //uso UnitOfWork para hacer un update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Carrousel carrousel)
        {
            if (carrousel !=null)
            {
                string rutaPrincipal = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var carrouselDdesdeBd = _unitOfWork.Carrousel.Get(carrousel.Id);

                if (archivos.Count() > 0)
                {
                    //Nuevo imagen carrousel
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\carrouseles");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    //var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, carrouselDdesdeBd.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }


                    carrousel.UrlImagen = @"\imagenes\carrouseles\" + nombreArchivo + extension;

                    _unitOfWork.Carrousel.Update(carrousel);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aquí sería cuando la imagen ya existe y se conserva
                    carrousel.UrlImagen = carrouselDdesdeBd.UrlImagen;
                }

                _unitOfWork.Carrousel.Update(carrousel);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));

            }

            return View(carrousel);
        }

        
        #region Llamadas a la API

        //llamada al metodo GetAll del repositorio a traves de UnitOfWork
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Carrousel.GetAll() });
        }

        //este metodo es llamada por ajax en el archivo javascript al presionar el boton delete de index
        //recibe el id de la entidad a borrar de la base de datos, la obtiene
        //logica para obtener la ruta de la imagen el proyecto y borrarla
        //devuelve un json para poder uso del mismo en la llamada ajax para el toatr
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var carrouselDesdeBd = _unitOfWork.Carrousel.Get(id);
            string rutaDirectorioPrincipal = _webHostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, carrouselDesdeBd.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }


            if (carrouselDesdeBd == null)
            {
                return Json(new { success = false, message = "Error borrando carrousel" });
            }

            _unitOfWork.Carrousel.Remove(carrouselDesdeBd);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Carrousel Borrado Correctamente" });
        }
        #endregion

    }
}
