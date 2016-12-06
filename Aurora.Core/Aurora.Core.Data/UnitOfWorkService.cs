using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Abstract service that supports a Unit of work
    /// </summary>
    public  abstract class UnitOfWorkService<DB> where DB : DbContext, IAuditableDBContext, new()
    {
        protected readonly IUnitOfWork<DB> _unitOfWork;
        /// <summary>
        /// Reference to the encapsulated DbContext Object of the UnitOfWork
        /// </summary>
        protected DB DbContext { get { return _unitOfWork.DbContext; } }

        protected UnitOfWorkService(IUnitOfWork<DB> unitOfWork)
        {
            if (unitOfWork==null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            _unitOfWork = unitOfWork;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }





    }
}
