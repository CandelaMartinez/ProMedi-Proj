using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class AnaliticaOrinaRepository : Repository<AnaliticaOrina>, IAnaliticaOrinaRepository
    {
        private readonly ApplicationDbContext _context;

        public AnaliticaOrinaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        //public IEnumerable<AnaliticaOrina> GetListaDeAnaliticasDeOrina()
        //{
        //    return _context.AnaliticasOrina.Select(x => new SelectListItem())
        //}

        public void Update(AnaliticaOrina analiticaOrina)
        {
            var objToUpdate = _context.AnaliticasOrina.FirstOrDefault(s => s.Id == analiticaOrina.Id);
            objToUpdate.Albumina = analiticaOrina.Albumina;
            objToUpdate.Leucositos = analiticaOrina.Leucositos;
            objToUpdate.Nitrito = analiticaOrina.Nitrito;
            objToUpdate.Hemoglobina = analiticaOrina.Hemoglobina;
            objToUpdate.Proteina = analiticaOrina.Proteina;
            objToUpdate.Creatinina = analiticaOrina.Creatinina;
        }
    }
}
