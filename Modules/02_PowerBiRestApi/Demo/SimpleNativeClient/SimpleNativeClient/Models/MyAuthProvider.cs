
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;

namespace SimpleNativeClient.Models {

    class MyAuthProvider : IAuthenticationProvider {

        const string authority = "https://login.microsoftonline.com/common";
        const string resourceMicrosoftGraphAPI = "https://graph.microsoft.com";
        readonly static string clientId = ConfigurationManager.AppSettings["clientId"];
        readonly static Uri replyUrl= new Uri(ConfigurationManager.AppSettings["replyUrl"]);
        
        public async Task AuthenticateRequestAsync(HttpRequestMessage request) {
            
            // Use ADAL to obtain access token - ADAL performs tokn caching behind the scenes
            AuthenticationContext authContext = new AuthenticationContext(authority);           
            AuthenticationResult authResult =
               await  authContext.AcquireTokenAsync(
                        resourceMicrosoftGraphAPI, 
                        clientId,
                        replyUrl,
                        new PlatformParameters(PromptBehavior.Auto));

            // insert access token into authorization header for each outbound request
            request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);
        }
    }
}
