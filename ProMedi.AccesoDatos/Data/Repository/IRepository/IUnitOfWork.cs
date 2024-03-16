using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository.IRepository
{
    //esta clase implementara metodo dispose para poder ser capaz de liberar recursos
    public interface IUnitOfWork : IDisposable
    {
        //agregamos todos los repositorios
        ICategoriaRepository Categoria { get; }

        //guarda los cambios que se realizen el a unidad de trabajo
        void Save();

    }
}
