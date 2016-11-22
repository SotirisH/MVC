using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{

    /// <summary>
    ///  The service layer will be responsible to send a Commit command to the database through a IUnitOfWork injected instance. 
    ///  For this to be done will use a pattern called UnitOfWork. Add the following two files into the Infrastructure folder.
    /// </summary>
    public interface IUnitOfWork
    {

        /// <summary>
        /// The user name that that will be audited for all object changes
        /// </summary>
        string UserName { get; }

        void Commit();
    }
}
