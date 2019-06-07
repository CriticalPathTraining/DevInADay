using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security;
using Microsoft.Identity.Client;

namespace RealtimeDashboardDemo.Models {

  class TokenManger {

    static readonly string clientId = ConfigurationManager.AppSettings["client-id"];
    static readonly string redirectUri = ConfigurationManager.AppSettings["reply-url"];
    static readonly string tenantName = ConfigurationManager.AppSettings["tenant-name"];

    // generic v2 endpoint references "organizations" instead of "common"
    const string tenantCommonAuthority = "https://login.microsoftonline.com/organizations";
    static readonly string tenantSpecificAuthority = "https://login.microsoftonline.com/" + tenantName;

    public static string GetAccessTokenInteractive(string[] scopes) {

      PublicClientApplicationOptions options = new PublicClientApplicationOptions();

      var appPublic = PublicClientApplicationBuilder.Create(clientId)
                       .WithAuthority(tenantCommonAuthority)
                       .WithRedirectUri(redirectUri)
                       .Build();

      var authResult = appPublic.AcquireTokenInteractive(scopes)
                                .WithPrompt(Prompt.NoPrompt)
                                .ExecuteAsync().Result;

      return authResult.AccessToken;
    }

    public static string GetAccessTokenWithUserPassword(string[] scopes) {

      var appPublic = PublicClientApplicationBuilder.Create(clientId)
                       .WithAuthority(tenantCommonAuthority)
                       .Build();

      string username = ConfigurationManager.AppSettings["user-name"];
      string userPassword = ConfigurationManager.AppSettings["user-password"];
      SecureString userPasswordSecure = new SecureString();
      foreach (char c in userPassword) {
        userPasswordSecure.AppendChar(c);
      }

      var authResult = appPublic.AcquireTokenByUsernamePassword(scopes, username, userPasswordSecure).ExecuteAsync().Result;
      return authResult.AccessToken;

    }

  }

}
