using ProMedi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository.IRepository
{
    public interface IPublicacionRepository : IRepository<Publicacion>
    {
        void Update(Publicacion publicacion);
        

    }
}
