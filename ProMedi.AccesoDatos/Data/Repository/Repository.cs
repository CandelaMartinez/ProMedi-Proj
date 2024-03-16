using Microsoft.EntityFrameworkCore;
using ProMedi.AccesoDatos.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProMedi.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            //creo una consulta Iquerable desde el dbset del contexto
            IQueryable<T> query = dbSet;

            //aplico filtro si lo tengo como parametro
            if(filter != null)
            {
                query = query.Where(filter);
            }

            //si recibo como parametro includeProperties, lo uso para traer los datos de las tablas relacionadas si es pedido
            if(includeProperties != null)
            {
                var includePropertiesWithoutEmptyEntries = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var includeProperty in includePropertiesWithoutEmptyEntries)
                {
                    query = query.Include(includeProperty);
                }
            }

            //si recibo parametros de ordenacion los aplico
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            //creo una consulta Iquerable desde el dbset del contexto
            IQueryable<T> query = dbSet;

            //aplico filtro si lo tengo como parametro
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //si recibo como parametro includeProperties, lo uso para traer los datos de las tablas relacionadas si es pedido
            if (includeProperties != null)
            {
                var includePropertiesWithoutEmptyEntries = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var includeProperty in includePropertiesWithoutEmptyEntries)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);

        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

    }
}
