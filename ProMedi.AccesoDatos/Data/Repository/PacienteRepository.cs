using Microsoft.AspNetCore.Mvc.Rendering;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.AccesoDatos.Migrations;
using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    
 

        //public IEnumerable<SelectListItem> GetListaPacientes()
        //{
        //    return _context.Pacientes.Select(x => new SelectListItem()
        //    {
        //        Text = x.Numero,
        //        Value = x.Id.ToString()
        //    });
        //}

        public void Update(Paciente paciente)
        {
            var objToUpdate = _context.Pacientes.FirstOrDefault(s => s.Id == paciente.Id);
            objToUpdate.Nombre = paciente.Nombre;
            objToUpdate.Apellidos = paciente.Apellidos;
            objToUpdate.Numero = paciente.Numero;
            objToUpdate.FechaNacimiento = paciente.FechaNacimiento;
        }

    }
}
