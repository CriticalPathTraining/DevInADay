using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using DailyReporterMVC.Models;
using System.Threading.Tasks;

namespace DailyReporterMVC.Services {
  public class TokenManagerService {

    public static async Task<string> GetPowerBiAccessToken() {
      return await GetAccessToken("https://analysis.windows.net/powerbi/api");
    }

    public static async Task<string> GetAccessToken(string resource) {

      // get ClaimsPrincipal for current user
      ClaimsPrincipal currentUserClaims = ClaimsPrincipal.Current;
      string signedInUserID = currentUserClaims.FindFirst(ClaimTypes.NameIdentifier).Value;
      string tenantID = currentUserClaims.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
      string userObjectID = currentUserClaims.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

      ApplicationDbContext db = new ApplicationDbContext();
      ADALTokenCache userTokenCache = new ADALTokenCache(signedInUserID);

      string urlAuthorityRoot = ConfigurationManager.AppSettings["ida:AADInstance"];
      string urlAuthorityTenant = urlAuthorityRoot + tenantID;

      AuthenticationContext authenticationContext =
        new AuthenticationContext(urlAuthorityTenant, userTokenCache);

      Uri uriReplyUrl = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path));

      string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
      string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
      ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);

      UserIdentifier userIdentifier = new UserIdentifier(userObjectID, UserIdentifierType.UniqueId);

      AuthenticationResult authenticationResult =
        await authenticationContext.AcquireTokenSilentAsync(resource, clientCredential, userIdentifier);

      return authenticationResult.AccessToken;

    }

  }
}