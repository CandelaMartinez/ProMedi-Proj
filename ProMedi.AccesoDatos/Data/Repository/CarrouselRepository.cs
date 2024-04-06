using Microsoft.EntityFrameworkCore;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class CarrouselRepository : Repository<Carrousel>, ICarrouselRepository
    {
        private readonly ApplicationDbContext _context;
        public CarrouselRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //recibimos el objeto desde el formulario
        public void Update(Carrousel carrousel)
        {
            throw new NotImplementedException();
        }
    }
}
