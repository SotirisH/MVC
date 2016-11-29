using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.FakeProvider.Models
{
    public enum MessageStatus
    {
        OK,
        Pending,
        InvalidNumber,
        InvalidCredentials,
        NotEnoughCredits,
        MessageTooLong,
        Error
    }

    public class Result
    {
        public Guid Id { get; set; }
        public string ReturnedMessage { get; set; }
        public DateTime TimeStamp { get; set; }
        public MessageStatus Status { get; set; }
        public string ExternalId { get; set; }
    }
}