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
            HasKey(p => p.Name);
            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.Url).IsRequired().HasMaxLength(255);
            Property(p => p.UserName).HasMaxLength(50);
            Property(p => p.PassWord).HasMaxLength(50);
        }
    }

    public class SMSHistoryConfiguration : EntityTypeConfiguration<SMSHistory>
    {
        public SMSHistoryConfiguration()
        {
            Property(p => p.SessionName).IsRequired().HasMaxLength(255);
            Property(p => p.Message).IsRequired();
            Property(p => p.MobileNumber).HasMaxLength(50);
            Property(p => p.ProviderMsgId).HasMaxLength(255);
            Property(p => p.TemplateId).IsRequired();
            // Create ForeignKey using fluent API on Property 
            // http://www.entityframeworktutorial.net/code-first/configure-one-to-many-relationship-in-code-first.aspx 
            HasRequired(p => p.Provider)
                .WithMany(s => s.SMSHistory)
                .HasForeignKey(s => s.ProviderName);
        }
    }

    public class TemplateConfiguration : EntityTypeConfiguration<Template>
    {
        public TemplateConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(50)
                   .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.Text).IsRequired();
        }
    }

    public class TemplateFieldConfiguration : EntityTypeConfiguration<TemplateField>
    {
        public TemplateFieldConfiguration()
        {
            HasKey(t => t.Name);
            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.Description).HasMaxLength(255);
            Property(p => p.GroupName).HasMaxLength(50).IsRequired();
            Property(p => p.DataFormat).HasMaxLength(50);
        }
    }
}
