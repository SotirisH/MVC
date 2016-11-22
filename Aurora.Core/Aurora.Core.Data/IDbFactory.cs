
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.MedicalRecords.Audit.Services.Infrastructure
{
    public interface IDbFactory<T> : IDisposable where T :DbContext
    {
        /// <summary>
        /// Returns a DbContext Object. The object lifetime is per request
        /// </summary>
        /// <returns></returns>
        T Init();
    }
}
