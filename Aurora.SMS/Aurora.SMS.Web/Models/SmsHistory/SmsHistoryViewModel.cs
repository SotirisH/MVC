using Aurora.SMS.EFModel;
using Aurora.SMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Models.SmsHistory
{
    public class SmsHistoryViewModel
    {
        public SmsHistoryCriteriaDTO Criteria { get; set; }
        public IEnumerable<SMSHistory> HistoryResults { get; set; }
        public int NumberOfResults
        {
            get
            {
                return HistoryResults == null ? 0 : HistoryResults.Count();
            }
        }
    }
}