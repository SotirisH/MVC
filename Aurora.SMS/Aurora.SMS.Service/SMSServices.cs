using Aurora.Core.Data;
using Aurora.Insurance.Services.DTO;
using Aurora.SMS.Providers;
using Aurora.SMS.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aurora.SMS.EFModel;

namespace Aurora.SMS.Service
{
    public interface ISMSServices
    {
        Guid SendBulkSMS(IEnumerable<DTO.SMSMessageDTO> messagesToSent, string providerName);
        IEnumerable<DTO.SMSMessageDTO> ConstructSMSMessages(IEnumerable<ContractDTO> recepients, int templateId);
        string GetAvailableCredits(string smsGateWayName);
        IEnumerable<EFModel.Provider> GetAllProviders();
    }


    public class SMSServices : UnitOfWorkService<SMSDb>, ISMSServices
    {
        // TODO:Create a generic repository
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
        /// <returns>Returns the session ID</returns>
        public Guid SendBulkSMS(IEnumerable<DTO.SMSMessageDTO> messagesToSent, string providerName)
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
                smsHistory.Status = Common.MessageStatus.Pending;
                smsHistory.SessionId = sessionId;
                smsHistory.SessionName = providerName + "|" + DateTime.Now;
                smsHistory.SendDateTime = DateTime.Now;
                if (string.IsNullOrWhiteSpace(msg.MobileNumber))
                {
                    smsHistory.ProviderFeedback = "The MobileNumber is not present!";
                }
                
                _smsHistoryRepository.Add(smsHistory);
            }
            _unitOfWork.Commit();

            List<Task> serverRequests = new List<Task>();
            // TODO:Need to abstract the ClientProviderFactory
            var smsProviderProxy = ClientProviderFactory.CreateClient(providerName, provider.UserName, provider.PassWord);
            //LINQ to Entities does not recognize the method 'Boolean IsNullOrWhiteSpace(System.String)'                  
            //http://stackoverflow.com/questions/9606979/string-isnullorwhitespace-in-linq-expression
            foreach (var historysms in _unitOfWork.DbContext.SMSHistoryRecords.Where(m=> (m.SessionId== sessionId) && (m.MobileNumber!=null) && m.MobileNumber.Trim()!=string.Empty).ToArray())
            {
                serverRequests.Add( SendSMSToProvider(smsProviderProxy, provider, historysms));
            }
            Task.WaitAll(serverRequests.ToArray());
            _unitOfWork.Commit();
            return sessionId;
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


        /// <summary>
        /// Sends a message to the SMS gateway
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="smsHistory"></param>
        /// <returns></returns>
        private async Task SendSMSToProvider(ISMSClientProvider smsClient,
                                                    EFModel.Provider provider,
                                                    EFModel.SMSHistory smsHistory)
        {
            // send the sms to the provider and return the control to the 
            var result = await smsClient.SendSMSAsync(smsHistory.Id,
                                                    smsHistory.MobileNumber,
                                                    smsHistory.Message,
                                                    null,
                                                    null).ConfigureAwait(false);
            smsHistory.ProviderFeedback = result.ReturnedMessage;
            smsHistory.ProviderFeedBackDateTime = result.TimeStamp;
            smsHistory.ProviderMsgId = result.ProviderId;
            if (result.MessageStatus == Common.MessageStatus.Delivered)
            {
                smsHistory.Status = Common.MessageStatus.Delivered;
            }
            else
            {
                smsHistory.Status = Common.MessageStatus.Error;
            }

           // _smsHistoryRepository.Update(smsHistory);
            return;
        }

        public string GetAvailableCredits(string smsGateWayName)
        {
            EFModel.Provider provider = _providerRepository.GetById(smsGateWayName, true);
            List<Task> serverRequests = new List<Task>();
            // TODO:Need to abstract the ClientProviderFactory
            var smsProviderProxy = ClientProviderFactory.CreateClient(smsGateWayName, provider.UserName, provider.PassWord);
            return smsProviderProxy.GetAvailableCreditsAsync().Result;
        }

        public IEnumerable<Provider> GetAllProviders()
        {
            return _providerRepository.GetAll();
        }
    }
}
