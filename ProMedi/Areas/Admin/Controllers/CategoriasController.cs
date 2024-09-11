using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProMedi.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        //GET: muestra vista con formulario para crear nueva categoria
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST: trae informacion de un formulario para crear nueva categoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (categoria != null)
            {
                //logica que guarda categoria en la base de datos
                _unitOfWork.Categoria.Add(categoria);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(categoria);
        }

        //GET: muestra vista con formulario para editar nueva categoria
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _unitOfWork.Categoria.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        //POST: trae informacion de un formulario para editar categoria
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                //logica que guardar categoria editada en la base de datos
                _unitOfWork.Categoria.Update(categoria);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(categoria);
        }


        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = _unitOfWork.Categoria.GetAll()});
        }
        //parte de la implementacion de delete esta en javascript categoria.js
        [HttpDelete]
        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.Categoria.Get(id);
            if(objFromDb == null)
            {
                //tengo que devolcer success = false para usar el plugin de sweet alters
                return Json(new { success = false, message = "Error al intentar borrar la categoria" });
            }

            _unitOfWork.Categoria.Remove(objFromDb);
            _unitOfWork.Save();
            //tengo que devolcer success = true para usar el plugin de sweet alters
            return Json(new { success = true, message = "Categoria borrada correctamente" });
        }
        #endregion
    }
}
