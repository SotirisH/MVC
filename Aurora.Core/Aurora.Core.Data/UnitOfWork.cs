using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    public class UnitOfWork<DB> : IUnitOfWork where DB : DbContext, IAuditableDBContext, new() 
    {
        /// <summary>
        /// The user name that modifies the object
        /// </summary>
        private readonly string _userName;
        
        private readonly DbFactory<DB> _dbFactory;
      
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">The factory that will provide the DBContext object</param>
        /// <param name="userName">The name of the user that will be used to audit
        /// all the objects of type 'EntityBase' </param>
        public UnitOfWork(DbFactory<DB> dbFactory, 
                        string userName)
        {
            if (dbFactory == null)
            {
                throw new ArgumentNullException("dbFactory", "The 'dbFactory' parameter cannot be null!");
            }
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("userName", "The 'userName' parameter cannot be null or empty!");
            }

            _userName = userName;
            _dbFactory = dbFactory;
        }

        public DB DbContext
        {
            get { return _dbFactory.DBContext(); }
        }

        string IUnitOfWork.UserName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Commit()
        {
            DbContext.SaveChanges(_userName);
        }

        public Task<int> CommitAsync()
        {
            return DbContext.SaveChangesAsync();
        }

    }
}
