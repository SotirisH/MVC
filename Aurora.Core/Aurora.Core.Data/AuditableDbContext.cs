using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    public abstract class AuditableDbContext: DbContext, IAuditableDBContext
    {
        protected AuditableDbContext(string dbConnection):base(dbConnection)
        {
        }

        protected AuditableDbContext(DbConnection dbConnection,bool contextOwnsConnection) : base(dbConnection, contextOwnsConnection)
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

            // http://stackoverflow.com/questions/15820505/dbentityvalidationexception-how-can-i-easily-tell-what-caused-the-error
            try
            {
                return base.SaveChanges();
            }

            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

       
    }
}
