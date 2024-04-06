using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository.IRepository
{
    public interface ICarrouselRepository : IRepository<Carrousel>
    {
        void Update(Carrousel carrousel);
    }
}
