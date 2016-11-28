using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Base Repository that provides all basic CRUD functionality for all EntityBase object
    /// </summary>
    /// <typeparam name="T">The EntityBase object that the CRUD acctions will be performed</typeparam>
    /// <typeparam name="DB">The DbContext that will perform all DB actions</typeparam>
    public class GenericRepository<T,DB>:IRepository<T>   where T : EntityBase
                                                       where DB :DbContext,IAuditableDBContext ,new()
    {
       
        private readonly IDbSet<T> dbSet;
        protected DbFactory<DB> DbFactory
        {
            get;
            private set;
        }

        protected DB DbContext
        {
            get { return DbFactory.DBContext(); }
        }


        public GenericRepository(DbFactory<DB> dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        // Implementation
        public virtual void Add(T entity)
        {
            entity.CreatedOn= DateTime.Now;
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var objToDelete= dbSet.Find(id);
            if (objToDelete!=null)
            {
                Delete(objToDelete);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        /// <summary>
        /// Returns the first item that complies with the criteria
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IQueryable<T> GetAsQueryable()
        {
            return dbSet.AsQueryable();
        }

    }
}
