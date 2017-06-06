using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DailyReporterMVC.Models;

namespace DailyReporterMVC.Services {
  public class PowerBiService {

    #region "Power BI REST API Urls"

    public const string urlPbiRestApiResource = "https://analysis.windows.net/powerbi/api";
    public const string urlPbiRestApiRoot = "https://api.powerbi.com/v1.0/";

    public const string restUrlWorkspaces = urlPbiRestApiRoot + "myorg/groups/";
    public const string restUrlDatasets = urlPbiRestApiRoot + "myorg/datasets/";
    public const string restUrlReports = urlPbiRestApiRoot + "myorg/reports/";
    public const string restUrlImports = urlPbiRestApiRoot + "myorg/imports/";

    #endregion

    #region "Utility Functions"

    private static async Task<string> ExecuteGetRequest(string urlRestEndpoint) {

      string accessToken = await TokenManagerService.GetPowerBiAccessToken();

      HttpClient client = new HttpClient();
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlRestEndpoint);
      request.Headers.Add("Authorization", "Bearer " + accessToken);
      request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

      HttpResponseMessage response = await client.SendAsync(request);

      if (response.StatusCode != HttpStatusCode.OK) {
        throw new ApplicationException("Error!!!!!");
      }

      return await response.Content.ReadAsStringAsync();
    }

    #endregion
    
    public static async Task<ReportsViewModel> GetReports() {

      string jsonResult = await ExecuteGetRequest(restUrlReports);
      ReportCollection reports = JsonConvert.DeserializeObject<ReportCollection>(jsonResult);
      string accessToken = await TokenManagerService.GetPowerBiAccessToken();
      ReportsViewModel reportsViewModel = new ReportsViewModel {
        reports = reports.value,
        defaultReport = reports.value[0],
        AccessToken = accessToken
      };

      return reportsViewModel;
    }
  }
}