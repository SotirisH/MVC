/* Data configuration of all 
 * EF models using Fluent API.
 * Why?: Everything what you can configure with DataAnnotations is also possible with the Fluent API. 
 * The reverse is not true. So, from the viewpoint of configuration options and flexibility the Fluent API is "better".
 * Also the validation rules are out of the POCO class
 * Help:https://msdn.microsoft.com/en-us/data/jj591617.aspx#PropertyIndex
 */

using Aurora.SMS.EFModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Aurora.SMS.Service.Data
{
    
    public class ProviderConfiguration: EntityTypeConfiguration<Provider>
    {
        public ProviderConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(50)
                    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Url).IsRequired().HasMaxLength(255);
            Property(p => p.UserName).HasMaxLength(50);
            Property(p => p.PassWord).HasMaxLength(50);
        }
    }

    public class SMSHistoryConfiguration : EntityTypeConfiguration<SMSHistory>
    {
        public SMSHistoryConfiguration()
        {
            Property(p => p.Message).IsRequired().HasMaxLength(255);
            Property(p => p.MobileNumber).IsRequired().HasMaxLength(50);
            Property(p => p.ProviderMsgId).HasMaxLength(255);
            Property(p => p.TemplateId).IsRequired();
            Property(p => p.ProviderId).IsRequired();
        }
    }

    public class TemplateConfiguration : EntityTypeConfiguration<Template>
    {
        public TemplateConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(50)
                   .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.Text).IsRequired().HasMaxLength(255);
        }
    }

    public class TemplateFieldConfiguration : EntityTypeConfiguration<TemplateField>
    {
        public TemplateFieldConfiguration()
        {
            HasKey(t => t.Name);
            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.GroupName).HasMaxLength(50);
        }
    }
}
