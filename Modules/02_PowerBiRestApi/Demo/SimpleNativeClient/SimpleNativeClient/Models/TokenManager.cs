using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;

namespace SimpleNativeClient.Models {
    class CustomTokenManager {

        const string authority = "https://login.microsoftonline.com/common";
        const string resourceMicrosoftGraphAPI = "https://graph.microsoft.com";
        readonly static string clientId = ConfigurationManager.AppSettings["clientId"];
        readonly static Uri replyUrl = new Uri(ConfigurationManager.AppSettings["replyUrl"]);

        public static string GetAccessToken() {

            AuthenticationContext authContext = new AuthenticationContext(authority);
            var authResult =
               authContext.AcquireTokenAsync(
                   resourceMicrosoftGraphAPI,
                   clientId,
                   replyUrl,
                   new PlatformParameters(PromptBehavior.Auto));

            return authResult.Result.AccessToken;            
        }
    }

}
