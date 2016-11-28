using Aurora.Core.Data;
using Aurora.Insurance.Services.DTO;
using Aurora.SMS.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aurora.SMS.Service
{
    public interface ISMSServices
    {

    }


    public class SMSServices : UnitOfWorkService<SMSDb>
    {
        private readonly GenericRepository<EFModel.Template, SMSDb> _templateRepository;
        private readonly GenericRepository<EFModel.TemplateField, SMSDb> _templatefieldsRepository;

        public SMSServices(IUnitOfWork<SMSDb> unitOfWork):base(unitOfWork)
        {
            _templateRepository = new GenericRepository<EFModel.Template, SMSDb>(_unitOfWork.DbFactory);
            _templatefieldsRepository = new GenericRepository<EFModel.TemplateField, SMSDb>(_unitOfWork.DbFactory);
        }


        /// <summary>
        /// Contrsucts an SMS Message for every Recepient
        /// </summary>
        /// <param name="recepients"></param>
        /// <param name="templateId">The templateId that will be used</param>
        /// <returns></returns>
        public IEnumerable<EFModel.SMSHistory> ConstructSMSMessages(IEnumerable<ContractDTO> recepients, int templateId)
        {
            var template = _templateRepository.GetById(templateId);
            var templateFields = _templatefieldsRepository.GetAll();
            if (template == null)
            {
                throw new NullReferenceException(string.Format("The template with id:{0} cannot be found in the db!",templateId));
            }
            
            foreach (var recepient in recepients)
            {
                string smsText = template.Text;
                // God bless regular expressions :)
                // Nice tester:https://regex101.com/r/cU5lC2/1
                /* The rule is that the injected fields inside the template text have the format {templateField.Name}
                 * Here, i loop through all the templateFields(there are only few so iloop them all even if don't exist in thetemplate text )
                 * and i repace with a value that i extract from the recepient Object using reflexion
                 * */
                foreach (var templateField in templateFields)
                {
                    smsText = Regex.Replace(smsText, "({" + templateField.Name + "})", Convert.ToString(recepient.GetType().GetProperty(templateField.Name).GetValue(recepient, null)));
                }
            }
            return null;
        }
    }
}
