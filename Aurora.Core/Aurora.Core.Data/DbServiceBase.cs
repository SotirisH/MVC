using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Abstract service that encapsulates a DB factoryObject.
    /// Any service that inherits this class has direct access to the EF and can perform queries directly
    /// </summary>
    public  abstract class DbServiceBase<DB> where DB : DbContext, IAuditableDBContext, new()
    {
        protected readonly DbFactory<DB> DBFactory;
        protected readonly IUnitOfWork<DB> UoW;

        /// <summary>
        /// Reference to the encapsulated DbContext Object of the UnitOfWork
        /// </summary>
        protected DB DbContext { get { return DBFactory.dbContext; } }


        protected DbServiceBase(DbFactory<DB> dbFactory)
        {
            DBFactory = dbFactory?? throw new ArgumentNullException("dbFactory");
        }
        /// <summary>
        /// Alternate constructor where the service can utilize a uoW
        /// </summary>
        /// <param name="uoW"></param>
        protected DbServiceBase(IUnitOfWork<DB> uoW)
        {
            UoW = uoW ?? throw new ArgumentNullException("uoW");
            DBFactory = uoW.DbFactory ?? throw new ArgumentNullException("DbFactory");
        }

    }
}
