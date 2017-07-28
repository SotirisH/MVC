using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    public interface IUnitOfWork
    {
        void Commit();
    }
    /// <summary>
    ///  The service layer will be responsible to send a Commit command to the database through a IUnitOfWork injected instance. 
    ///  For this to be done will use a pattern called UnitOfWork. Add the following two files into the Infrastructure folder.
    /// </summary>
    public interface IUnitOfWork<DB>: IUnitOfWork where DB : DbContext, IAuditableDBContext, new()
    {
        DB DbContext {get;}
        DbFactory<DB> DbFactory { get; }
        /// <summary>
        /// The user name that that will be audited for all object changes
        /// </summary>
        string UserName { get; }
    }
}
