using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Service.Data
{
    public class SMSDb : AuditableDbContext
    {
        public SMSDb() : base("SMSDb")
        {

        }

        public virtual DbSet<EFModel.Provider> Providers { get; set; }
        public virtual DbSet<EFModel.SMSHistory> SMSHistoryRecords { get; set; }
        public virtual DbSet<EFModel.Template> Templates { get; set; }
        public virtual DbSet<EFModel.TemplateField> TemplateFields { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ProviderConfiguration());
            modelBuilder.Configurations.Add(new TemplateConfiguration());
            modelBuilder.Configurations.Add(new TemplateFieldConfiguration());
            modelBuilder.Configurations.Add(new SMSHistoryConfiguration());
        }
    }
}
