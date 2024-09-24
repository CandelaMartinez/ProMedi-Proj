using Microsoft.AspNetCore.Mvc;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.AccesoDatos.Migrations;
using ProMedi.Models;

namespace ProMedi.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class AnaliticasOrinaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnaliticasOrinaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //muestra una vista
        [HttpGet]
        public IActionResult Index(int pacienteId)
        {
            var analiticasOrina = _unitOfWork.AnaliticaOrina.GetAll(a => a.Paciente.Id == pacienteId, includeProperties: "Paciente");
            var paciente = _unitOfWork.Paciente.GetFirstOrDefault(a => a.Id == pacienteId);
            ViewBag.PacienteId = pacienteId;
            ViewBag.Paciente = paciente;
            return View(analiticasOrina);
        }

        [HttpGet]
        public IActionResult Create(int pacienteId)
        {
            var analiticaOrina = new AnaliticaOrina { PacienteId = pacienteId };
            return View(analiticaOrina);
        }

        // Procesa el formulario de creación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AnaliticaOrina analiticaOrina)
        {
            if (analiticaOrina != null)
            {
                _unitOfWork.AnaliticaOrina.Add(analiticaOrina);
                _unitOfWork.Save();
                return RedirectToAction("Index", new { pacienteId = analiticaOrina.PacienteId });
            }
            return View(analiticaOrina);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var analiticaOrina = new AnaliticaOrina();
            if (id != null)
            {
                analiticaOrina = _unitOfWork.AnaliticaOrina.Get(id.GetValueOrDefault());
            }

            return View(analiticaOrina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AnaliticaOrina analiticaOrina)
        {
            if (analiticaOrina != null)
            {
                _unitOfWork.AnaliticaOrina.Update(analiticaOrina);
                _unitOfWork.Save();
                return RedirectToAction("Index", new { pacienteId = analiticaOrina.PacienteId });

            }
            return View(analiticaOrina);
        }


        [HttpGet]
        public IActionResult Archivar(int id, int pacienteId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var analiticaOrina = _unitOfWork.AnaliticaOrina.Get(id);
            if (analiticaOrina == null)
            {
                return NotFound();
            }
            analiticaOrina.IsArchived = true;

            _unitOfWork.AnaliticaOrina.Update(analiticaOrina);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index), new {pacienteId = pacienteId});
        }

        [HttpGet]
        public IActionResult Desarchivar(int id, int pacienteId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var analiticaOrina = _unitOfWork.AnaliticaOrina.Get(id);
            if (analiticaOrina == null)
            {
                return NotFound();
            }
            analiticaOrina.IsArchived = false;

            _unitOfWork.AnaliticaOrina.Update(analiticaOrina);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index), new { pacienteId = pacienteId });
        }
    }
}
