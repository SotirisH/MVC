﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Providers
{
    /// <summary>
    /// Factory for creating HttpClients to communicate with the Http  SMS providers
    /// </summary>
    public class ClientProviderFactory
    {
       public static ISMSClientProvider CreateClient(string providerName, 
                                                        string username,
                                                        string password)
       {
            switch (providerName.ToLower())
            {
                case "aurorafakeprovider":
                    return new AuroraFakeProvider(username, password);
                default:
                    throw new Exception(string.Format("The provider with name {0} has not been registered!", providerName));
            }
       }
    }
}