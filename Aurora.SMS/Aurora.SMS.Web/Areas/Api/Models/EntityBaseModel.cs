using System;

namespace Aurora.SMS.Web.Areas.Api.Models
{
    public abstract class EntityBaseModel
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string RowVersion { get; set; }

    }
}