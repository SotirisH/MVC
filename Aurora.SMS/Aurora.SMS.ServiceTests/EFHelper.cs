using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.ServiceTests
{
    /// <summary>
    /// Helper funtions for quick set up Moqs the EF
    /// </summary>
    /// <remarks>
    /// Mocking EF https://msdn.microsoft.com/en-us/library/dn314429(v=vs.113).aspx
    /// Mocking Querable Entities
    /// </remarks>
    public static class EFHelper
    {

        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            // Caution!: mockTemplateSet is not real implementation of DbSet but a mock which means 
            // it's fake and it needs to be setup for all methods you need. 
            // The Add is not exception so it needs to be set up to do what you need otherwise it does nothing.
            // http://stackoverflow.com/questions/31349351/how-to-add-an-item-to-a-mock-dbset-using-moq
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
