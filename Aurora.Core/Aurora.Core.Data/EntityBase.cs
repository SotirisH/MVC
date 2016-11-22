using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Base class for all EF Class with audit tracking fields & Timestamp
    /// </summary>
    /// <remarks>
    /// </remarks>
    public abstract class EntityBase
    {
        /// <summary>
        /// User name either from the AD or the application
        /// </summary>
        public string CreatedBy { get; set; }
        // http://stackoverflow.com/questions/27038524/sql-column-default-value-with-entity-framework
        // https://andy.mehalick.com/2014/02/06/ef6-adding-a-created-datetime-column-automatically-with-code-first-migrations/
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
       
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
