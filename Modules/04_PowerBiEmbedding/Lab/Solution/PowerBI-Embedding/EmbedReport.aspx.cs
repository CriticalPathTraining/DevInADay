using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;

namespace PowerBI_Embedding
{
    public partial class EmbedReport : System.Web.UI.Page
    {
        private static readonly string AuthorityUrl = ConfigurationManager.AppSettings["authorityUrl"];
        private static readonly string ResourceUrl = ConfigurationManager.AppSettings["resourceUrl"];
        private static readonly string ApiUrl = ConfigurationManager.AppSettings["apiUrl"];
        private static readonly string GroupId = ConfigurationManager.AppSettings["groupId"];
        private static readonly string ClientId = ConfigurationManager.AppSettings["clientId"];

        private static readonly string Username = ConfigurationManager.AppSettings["pbiUsername"];
        private static readonly string Password = ConfigurationManager.AppSettings["pbiPassword"];

        public string embedToken;
        public string embedUrl;
        public string reportId;

        protected void Page_Load(object sender, EventArgs e)
        {
            var credential = new UserPasswordCredential(Username, Password);

            // Authenticate using created credentials
            var authenticationContext = new AuthenticationContext(AuthorityUrl);
            var authenticationResult = authenticationContext.AcquireTokenAsync(ResourceUrl, ClientId, credential).Result;

            var tokenCredentials = new TokenCredentials(authenticationResult.AccessToken, "Bearer");

            // Create a Power BI Client object (it will be used to call Power BI APIs)
            using (var client = new PowerBIClient(new Uri(ApiUrl), tokenCredentials))
            {

                // Get a list of reports
                var reports = client.Reports.GetReportsInGroup(GroupId);

                // Get the first report in the group
                var report = reports.Value.FirstOrDefault();

                // Generate an embed token
                var generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
                var tokenResponse = client.Reports.GenerateTokenInGroup(GroupId, report.Id, generateTokenRequestParameters);

                embedToken = tokenResponse.Token;
                embedUrl = report.EmbedUrl;
                reportId = report.Id;
            }
        }
    }
}