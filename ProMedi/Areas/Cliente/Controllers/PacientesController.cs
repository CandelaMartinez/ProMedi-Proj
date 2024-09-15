using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;

namespace ProMedi.Areas.Cliente.Controllers
{
    //[Authorize(Roles = "Medico")]
    [Area("Cliente")]
    public class PacientesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PacientesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //muestra una vista
        [HttpGet]
        public IActionResult Index()
        {
            var pacientes = _unitOfWork.Paciente.GetAll();
            return View(pacientes);
        }

        //GET: muestra vista con formulario para crear nuevo paciente
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PacienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Paciente paciente)
        {
            if(paciente != null)
            {
                _unitOfWork.Paciente.Add(paciente);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var paciente = new Paciente();
            if(id != null)
            {
                paciente = _unitOfWork.Paciente.Get(id.GetValueOrDefault());
            }

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Paciente.Update(paciente);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(paciente);
        }

        [HttpGet]
        public IActionResult Archivar(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var paciente = _unitOfWork.Paciente.Get(id);
            if(paciente == null)
            {
                return NotFound();
            }
            paciente.IsArchived = true;

            _unitOfWork.Paciente.Update(paciente);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Desarchivar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paciente = _unitOfWork.Paciente.Get(id);
            if (paciente == null)
            {
                return NotFound();
            }
            paciente.IsArchived = false;

            _unitOfWork.Paciente.Update(paciente);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
