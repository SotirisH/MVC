using Aurora.Insurance.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web
{
    /// <summary>
    /// All the interaction with the session is done by this helper
    /// </summary>
    /// <remarks>
    /// This approach has several advantages:
    /// 1)it saves you from a lot of type-casting
    /// 2)you don't have to use hard-coded session keys throughout your application (e.g. Session["loginId"]
    /// 3)you can document your session items by adding XML doc comments on the properties of MySession
    /// 4)you can initialize your session variables with default values(e.g.assuring they are not null)
    /// </remarks>
    public class SessionHelper
    {
        /// <summary>
        /// The selected Template Id when we run the wizzard to send SMS
        /// </summary>
        public int SelectedTemplateId { get; set; }
        public QueryCriteriaDTO Criteria { get; set; }
        // private constructor
        private SessionHelper()
        {
            //Property1 = "default value";
        }

        // Gets the current session.
        public static SessionHelper Current
        {
            get
            {
                SessionHelper session = (SessionHelper)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }
        

    }
}