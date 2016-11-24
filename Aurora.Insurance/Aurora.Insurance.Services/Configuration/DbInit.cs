using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.Services.Configuration
{
    /// <summary>
    /// Initialization of the database
    /// </summary>
    public class DbInit: DropCreateDatabaseIfModelChanges<InsuranceDb>
    {
        /// <summary>
        /// This class initializes the DB with data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(InsuranceDb context)
        {
           /* GetCategories().ForEach(c => context.Categories.Add(c));
            GetGadgets().ForEach(g => context.Gadgets.Add(g));

            context.Commit();*/
        }
    }
}
