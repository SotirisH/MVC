using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Service.DTO
{
    /// <summary>
    /// Represents an SMS Template DTO
    /// </summary>
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// The SMS template body
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The template object is semi-immutable.
        /// When a template is modified and been used in the SMS history
        /// then a new template record is created and the current marked as inactive
        /// </summary>
        public bool IsInactive { get; set; }
    }
}
