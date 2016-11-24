using Aurora.Core.Data;
using Aurora.Insurance.EFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.Services
{
    public class InsuranceDb : DbContext
    {
        public InsuranceDb():base("InsuranceDb")
        {

        }
        // Remember to setup the entities here or the tables will not be created!
        // Note that the DbSet properties on the context are marked as virtual. 
        //This will allow the mocking framework to derive from our context and overriding these properties with a mocked implementation.
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

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
