using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    
    /// <summary>
    /// The unit of work guarantees that all the repositories will use the same context(during a request)
    /// </summary>
    /// <typeparam name="DB"></typeparam>
    public class UnitOfWork<DB> : IUnitOfWork<DB> where DB : DbContext, IAuditableDBContext, new() 
    {
        /// <summary>
        /// The user name that modifies the object
        /// </summary>
        private readonly ICurrentUserService _currentUserService;
        
        private readonly DbFactory<DB> _dbFactory;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">The factory that will provide the DBContext object</param>
        /// <param name="currentUserService">A service that will return the name of the user that will be used to audit
        /// all the objects of type 'EntityBase'. Usually the implementation should be HttpContext.Current.User.Identity.Name </param>
        public UnitOfWork(DbFactory<DB> dbFactory,
                        ICurrentUserService currentUserService)
        {
            if (dbFactory == null)
            {
                throw new ArgumentNullException("dbFactory", "The 'dbFactory' parameter cannot be null!");
            }
            if (currentUserService==null)
            {
                throw new ArgumentNullException("currentUserService", "The 'currentUserService' parameter cannot be null!");
            }

            _currentUserService = currentUserService;
            _dbFactory = dbFactory;
        }

        public DB DbContext
        {
            get { return _dbFactory.DBContext(); }
        }
   
        public string UserName
        {
            get
            {
                return _currentUserService.GetCurrentUser();
            }
        }

        internal DbFactory<DB> DbFactory
        {
            get { return _dbFactory; }
        }

        public void Commit()
        {
            DbContext.SaveChanges(UserName);
        }

        public Task<int> CommitAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        /// <summary>
        /// Returns a generic repository from the dictionary and if does not exits then it creates one
        /// </summary>
        public GenericRepository<TEntity, DB> GetGenericRepositoryOf<TEntity>() where TEntity : EntityBase
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as GenericRepository<TEntity, DB>;
            }
            IRepository<TEntity> r = new GenericRepository<TEntity,DB>(_dbFactory);
            repositories.Add(typeof(TEntity), r);
            return r as GenericRepository<TEntity, DB>;
        }


    }
}
