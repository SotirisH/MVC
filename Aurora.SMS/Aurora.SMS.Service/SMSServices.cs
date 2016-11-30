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
        private readonly GenericRepository<EFModel.Provider, SMSDb> _providerRepository;
        private readonly GenericRepository<EFModel.SMSHistory, SMSDb> _smsHistoryRepository;

        public SMSServices(IUnitOfWork<SMSDb> unitOfWork):base(unitOfWork)
        {
            _templateRepository = new GenericRepository<EFModel.Template, SMSDb>(_unitOfWork.DbFactory);
            _templatefieldsRepository = new GenericRepository<EFModel.TemplateField, SMSDb>(_unitOfWork.DbFactory);
            _providerRepository = new GenericRepository<EFModel.Provider, SMSDb>(_unitOfWork.DbFactory);
            _smsHistoryRepository = new GenericRepository<EFModel.SMSHistory, SMSDb>(_unitOfWork.DbFactory);
        }

        /// <summary>
        /// Sends messages to the provider, creates a session in the history table and saves the messages as history
        /// under this session
        /// </summary>
        /// <param name="messagesToSent"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        public Guid SendSMS(IEnumerable<DTO.SMSMessageDTO> messagesToSent, string providerName)
        {
            // get the provider data
            EFModel.Provider provider = _providerRepository.GetById(providerName, true);
            Guid sessionId = Guid.NewGuid();
            // First job is to save the messages into the history table in order to populate the id
            foreach (var msg in messagesToSent)
            {
                EFModel.SMSHistory smsHistory = new EFModel.SMSHistory();
                smsHistory.ContractId = msg.ContractId;
                smsHistory.Message = msg.Message;
                smsHistory.MobileNumber = msg.MobileNumber;
                smsHistory.PersonId = msg.PersonId;
                smsHistory.ProviderName = providerName;
                smsHistory.TemplateId = msg.TemplateId;
                smsHistory.Status = EFModel.MessageStatus.Pending;
                smsHistory.SessionId = sessionId;
                smsHistory.SessionName = providerName + "|" + DateTime.Now;
                _smsHistoryRepository.Add(smsHistory);
            }
            _unitOfWork.Commit();

            var smsClient = Providers.ClientProviderFactory.CreateClient(providerName, provider.UserName, provider.PassWord);
            foreach (var historysms in _unitOfWork.DbContext.SMSHistoryRecords)
            {
                var result = await smsClient.SendSMS();
            }
        }


        /// <summary>
        /// Contrsucts an SMS Message for every Recepient
        /// </summary>
        /// <param name="recepients"></param>
        /// <param name="templateId">The templateId that will be used</param>
        /// <returns></returns>
        public IEnumerable<DTO.SMSMessageDTO> ConstructSMSMessages(IEnumerable<ContractDTO> recepients, int templateId)
        {
            var template = _templateRepository.GetById(templateId);
            var templateFields = _templatefieldsRepository.GetAll();

            var smsList = new List<DTO.SMSMessageDTO>();
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
                 * and i repace with a value that i extract from the recepient Object using reflection
                 * */
                foreach (var templateField in templateFields)
                {
                    smsText = Regex.Replace(smsText, "({" + templateField.Name + "})", GetFormattedValue(recepient, templateField));
                }
                smsList.Add(new DTO.SMSMessageDTO(){
                    ContractId= recepient.Contractid,
                    Message=smsText,
                    MobileNumber=recepient.MobileNumber,
                    PersonId=recepient.PersonId,
                    TemplateId= templateId
                });

            }
            return smsList;
        }


        private string GetFormattedValue(ContractDTO recepient,
                                            EFModel.TemplateField templateField)
        {
            return  string.Format("{0:" + (templateField.DataFormat ?? string.Empty) + "}", recepient.GetType().GetProperty(templateField.Name).GetValue(recepient, null));
        }
    }
}
