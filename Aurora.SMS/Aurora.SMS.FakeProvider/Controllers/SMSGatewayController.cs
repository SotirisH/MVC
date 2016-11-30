
using Aurora.SMS.FakeProvider.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Aurora.SMS.FakeProvider.Controllers
{
    /// <summary>
    /// The SMS GateWay.
    /// </summary>
    /// <remarks>
    /// For demostration purposes the gateway is slow and creates random errors
    /// </remarks>
    public class SMSGatewayController : ApiController
    {
      

        /// <summary>
        /// The credit decrease is created as public function in order to be able to Moq it
        /// </summary>
        public virtual void DecreaseCredit()
        {
            // Remove one credit from the application variable
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["Credits"] = (int)(System.Web.HttpContext.Current.Application["Credits"]) - 1;
            System.Web.HttpContext.Current.Application.UnLock();
        }

        public virtual void ApplyDelay()
        {
            Thread.Sleep(1500);
        }

        /// <summary>
        /// Test method
        /// http://localhost:8080/api/SMSGateway/EchoTest?echo=sdsd
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult EchoTest(string echo)
        {
            return Ok(echo);
        }

        /// <summary>
        /// Sends an SMS to a mobile phone
        /// </summary>
        /// <param name="username">Login user name</param>
        /// <param name="password"></param>
        /// <param name="message">The body of the message</param>
        /// <param name="mobileNumber">The mobile number that will be sent</param>
        /// <param name="messageExternalId">The external ID will be returned with the result. It is used to track the incoming message</param>
        /// <returns></returns>
        [HttpGet]
        public SMSResult SendSMS(string username,
                            string password,
                            string message,
                            string mobileNumber,
                            string messageExternalId)
        {
            
            Bogus.Faker faker = new Bogus.Faker();
            SMSResult result = new SMSResult() { Id=Guid.NewGuid(), ExternalId= messageExternalId };
            //try
            //{
                ApplyDelay();
                // simulate a random result behaviour
                // 50% to be ok
                if (faker.Random.Number(100) < 50)
                {
                    result.Status = MessageStatus.OK;
                }
                else
                {
                    // exclude ok
                    result.Status = faker.Random.Enum(MessageStatus.OK);
                }
                
                switch (result.Status)
                {
                    case MessageStatus.Error:
                        result.ReturnedMessage = faker.System.Exception().Message;
                        break;
                    case MessageStatus.InvalidCredentials:
                        result.ReturnedMessage = "Your credentials are invalid!";
                        break;
                    case MessageStatus.InvalidNumber:
                        result.ReturnedMessage = "This mobile number is invalid!";
                        break;
                    case MessageStatus.MessageTooLong:
                        result.ReturnedMessage = "This message is too long!";
                        break;
                    case MessageStatus.NotEnoughCredits:
                        break;
                    case MessageStatus.OK:
                        DecreaseCredit();
                        break;
                    case MessageStatus.Pending:
                        result.ReturnedMessage = "Pending....";
                        break;
                }
                return result;
            //}
            //catch (Exception ex)
            //{

            //    return InternalServerError(ex);
            //}
        }

        /// <summary>
        /// This method simulates resending an SMS message with a given Id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="messageId"></param>
        /// <param name="messageExternalId"></param>
        /// <returns></returns>
        [HttpGet]
        public SMSResult ReSendSMS(string username,
                            string password,
                            Guid messageId,
                            string messageExternalId)
        {
            // Suppose the SMS message is retrieved from the providers DB

            return SendSMS(username,
                password,
                "Retrieved OriginalText",
                "Retrieved MobileNumber",
                messageExternalId);
        }
        [HttpGet]
        public int GetAvailableCredits(string username,
                    string password)
        {
            ApplyDelay();
            // Remove one credit from the application variable
            return (int)(System.Web.HttpContext.Current.Application["Credits"]);
        }

        [HttpGet]
        public SMSResult GetMessageStatus(string username,
                                string password, 
                                Guid messageId,
                                string messageExternalId)
        {
            ApplyDelay();
            return SendSMS(username,
                         password,
                         "Retrieved OriginalText",
                         "Retrieved MobileNumber",
                         messageExternalId);
        }

    }



}
