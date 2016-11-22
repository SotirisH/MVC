
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Factory responsible for managing the instance of DbContext
    /// </summary>
    public abstract class DbFactory<T> : Disposable where T : DbContext , IAuditableDBContext ,new()
    {
        internal T dbContext;
        /// <summary>
        /// Returns the active DBContext Or Initializes a new instance
        /// </summary>
        /// <returns></returns>
        /// <remarks> The DBContext life time should be request, so in the very first time the object is created and
        /// then the same instance is used during the request(Kind of signleton pattern)
        /// </remarks>
        public T DBContext()
        {
            return dbContext ?? (dbContext = Init());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        protected virtual T Init()
        {
            return new T();
        }
    }
}
