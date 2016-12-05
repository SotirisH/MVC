using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Models
{
    public class SelectedTemplateViewModel
    {
        public int SelectedTemplateId { get; set; }
        public IEnumerable<Aurora.SMS.EFModel.Template> Templates { get; set; }
    }
}