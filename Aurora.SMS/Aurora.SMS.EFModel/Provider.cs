using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.EFModel
{
    /// <summary>
    /// A registered SMS Provider
    /// </summary>
    public class Provider:EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyVersion { get; set; }
    }
}
