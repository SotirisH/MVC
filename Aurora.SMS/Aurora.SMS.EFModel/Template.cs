using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.EFModel
{
    /// <summary>
    /// Represents an SMS Template
    /// </summary>
    public class Template:EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// The SMS template body
        /// </summary>
        public string Text { get; set; }
    }
}
