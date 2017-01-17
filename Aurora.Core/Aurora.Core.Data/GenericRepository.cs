using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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
            // if the entities have been loaded, i cannot attach the modified entity
            // https://cmatskas.com/an-object-with-the-same-key-already-exists-in-the-objectstatemanager-entity-frawework/

            var entry = DbContext.Entry(entity);

            var key = GetPrimaryKey(entry);
            if (entry.State == EntityState.Detached)
            {
                var currentEntry = dbSet.Find(key);
                if (currentEntry != null)
                {
                    var attachedEntry = DbContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
        }
        public void AddOrUpdate(T entity)
        {
            if (entity.RowVersion == null || entity.RowVersion.Length == 0)
            {
                Add(entity);
            }
            else
            {
                Update(entity);
            }
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

        public virtual T GetById(object id, 
                                bool throwExceptionIfNotFound=false)
        {
            var result = dbSet.Find(id);
            if (throwExceptionIfNotFound && result==null)
            {
                // TODO:Implement a custom exception
                throw new Exception(string.Format("The Entity of type {0}  with id:{1} could not be found in the database!", dbSet.GetType().Name, id));
            }
            return result;
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

        
        private object[] GetPrimaryKey(DbEntityEntry entry)
        {
            //http://stackoverflow.com/questions/15893141/how-do-you-get-a-dbentityentry-entitykey-object-with-unknown-name
            var oc = ((IObjectContextAdapter)DbContext).ObjectContext;
            var objSet = oc.CreateObjectSet<T>();
            var entityKey = oc.CreateEntityKey(objSet.EntitySet.Name, entry.Entity);
            var keyValues = new List<object>();
            foreach (var item in entityKey.EntityKeyValues)
            {
                keyValues.Add(item.Value);
            }
            // it took me long to realize that i have to return an  object[]
            return keyValues.ToArray();


            //var objectStateEntry = ((IObjectContextAdapter)DbContext).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            //return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
            ////EntitySetBase setBase = ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity).EntitySet;
            ////string[] keyNames = setBase.ElementType.KeyMembers.Select(k => k.Name).ToArray();

            //var myObject = entry.Entity;
            //var property =
            //    myObject.GetType()
            //        .GetProperties()
            //        .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            //return property.GetValue(myObject, null);
        }

    }
}
