using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    public abstract class AuditableDbContext: DbContext
    {
        protected AuditableDbContext(string dbConnection):base(dbConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove the plural names
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        /// <summary>
        /// All entities are being audited.
        /// This overload should be used instead of the original one
        /// </summary>
        /// <param name="userName">The name of the user that perform the actions</param>
        /// <returns></returns>
        public int SaveChanges(string userName)
        {
            var changeSet = ChangeTracker.Entries<EntityBase>();

            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                {
                    entry.Entity.CreatedOn = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                }
                foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified))
                {
                    entry.Entity.ModifiedOn = DateTime.Now;
                    entry.Entity.ModifiedBy = userName;
                }
            }
            return base.SaveChanges();
        }
    }
}
