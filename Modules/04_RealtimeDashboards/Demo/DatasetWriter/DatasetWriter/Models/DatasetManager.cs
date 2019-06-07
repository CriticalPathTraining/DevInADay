using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;

namespace DatasetWriter.Models {

  class DatasetManager {

    #region "Power BI Internals"

    // Power BI Service API Root URL
    const string urlPowerBiRestApiRoot = "https://api.powerbi.com/";

    static string[] scopesManageDashboards = new string[] {
      "https://analysis.windows.net/powerbi/api/Content.Create",
      "https://analysis.windows.net/powerbi/api/Dashboard.ReadWrite.All",
      "https://analysis.windows.net/powerbi/api/Group.Read.All",
      "https://analysis.windows.net/powerbi/api/Workspace.ReadWrite.All"
     };

    static PowerBIClient pbiClient = null;

    static DatasetManager() {
      string AccessToken = TokenManger.GetAccessTokenInteractive(scopesManageDashboards);
      pbiClient = new PowerBIClient(new Uri(urlPowerBiRestApiRoot),
                               new TokenCredentials(AccessToken, "Bearer"));
    }


    #endregion

    static readonly string appWorkspaceId = ConfigurationManager.AppSettings["workspace-id"];

    public static void DisplayAssetsInWorkspace() {

      Console.WriteLine();
      Console.WriteLine("Datasets:");
      var datasets = pbiClient.Datasets.GetDatasetsInGroup(appWorkspaceId).Value;
      foreach (var dataset in datasets) {
        Console.WriteLine(dataset.Name + " - " + dataset.DefaultMode);
      }

      Console.WriteLine();

      Console.WriteLine("Reports:");
      var reports = pbiClient.Reports.GetReportsInGroup(appWorkspaceId).Value;
      foreach (var report in reports) {
        Console.WriteLine(report.Name + " - " + report.Id);
      }

      Console.WriteLine();

    }

    private static string GetDatasetId(string DatasetName) {
      var datasets = pbiClient.Datasets.GetDatasetsInGroup(appWorkspaceId).Value;
      foreach (var dataset in datasets) {
        if (dataset.Name.Equals(DatasetName)) {
          return dataset.Id;
        }
      }
      return string.Empty;
    }

    private static bool ReportDoesNotExist(string ReportName) {
      var reports = pbiClient.Reports.GetReportsInGroup(appWorkspaceId).Value;
      foreach (var report in reports) {
        if (report.Name.Equals(ReportName)) {
          return false;
        }
      }
      return true;
    }

    public static void CreateTemperatureReadingsDataset() {

      string datasetName = "Temperature Readings";

      string datasetId = GetDatasetId(datasetName);

      if (datasetId == string.Empty) {
        Console.WriteLine("Creating " + datasetName + " dataset...");
        Table tableDef = TemperatureReadings.GetTableDefinition();

        Dataset datasetRequest = new Dataset(datasetName,
                                 new List<Table> { tableDef },
                                 defaultMode: DatasetMode.PushStreaming);
        datasetId = ((Dataset)pbiClient.Datasets.PostDatasetInGroup(appWorkspaceId, datasetRequest)).Id;
      }
      else {
        Console.WriteLine(datasetName + " dataset already exists");
      }

      PopulateTemperatureReadingsDataset(datasetId);

    }

    private static void PopulateTemperatureReadingsDataset(string datasetId){

      Console.WriteLine();
      Console.Write("Pushing rows");
      while (true) {
        var rows = TemperatureReadings.GetNextTemperatureRowset();
        pbiClient.Datasets.PostRows(appWorkspaceId, datasetId, "TemperatureReadings", rows);
        Console.Write(".");
        Thread.Sleep(1000);
      }

    }

    private static string CreateDatasetFromJSON(string json) {

      string accessToken = TokenManger.GetAccessTokenWithUserPassword(scopesManageDashboards);
      string restUri = urlPowerBiRestApiRoot + "v1.0/myorg/groups/" + appWorkspaceId + "/datasets/";

        HttpContent body = new StringContent(json);
        body.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        HttpResponseMessage response = client.PostAsync(restUri, body).Result;
      
      string datasetId = string.Empty;

      if (response.IsSuccessStatusCode) {
          string jsonResult = response.Content.ReadAsStringAsync().Result;        
        JsonDataset dataset = JsonConvert.DeserializeObject<JsonDataset>(jsonResult);
        datasetId = dataset.id;
      }

      return datasetId;

    }

    public static void CreateCampaignContributionsDataset() {
      Console.WriteLine("Creating Campaign Contributions Dataset...");

      string datasetName = "Campaign Contributions";
      string datasetId = GetDatasetId(datasetName);

      if (datasetId != string.Empty)
        Console.WriteLine(datasetName +  " exists");
      else {
        Console.WriteLine("Creating " + datasetName);
        string jsonCampaignContributions = Properties.Resources.CampaignContributions_json
                                                     .Replace("@DatasetName", datasetName);
        datasetId = CreateDatasetFromJSON(jsonCampaignContributions);
      }

      Console.WriteLine();
      Console.WriteLine("Datasets ID is " + datasetId);

      try {
        UploadAndBindCampaignContributionsReport(datasetId);
      }
      catch {}

      pbiClient.Datasets.DeleteRowsInGroup(appWorkspaceId, datasetId, "Contributions");

      Console.WriteLine();
      Console.Write("Pushing rows");
      while (true) {
        var rows = ContributionsDataFactory.GetContributionList();
        pbiClient.Datasets.PostRows(appWorkspaceId, datasetId, "Contributions", rows);
        Console.Write(".");
        Thread.Sleep(1000);
      }

    }

    private static void UploadAndBindCampaignContributionsReport(string datasetId) {
    
      // upload report PBIX
      string reportName = "Campaign Contributions Report";
      
      // delay until import process has completed
      if (ReportDoesNotExist(reportName)) {
        Console.WriteLine("Uploading report " + reportName);
        Stream pbixStream = new MemoryStream(Properties.Resources.CampaignContributionsReport_pbix);
        Import import = pbiClient.Imports.PostImportWithFileInGroup(appWorkspaceId, pbixStream, reportName);
        import = pbiClient.Imports.GetImportById(import.Id);

        Console.Write("Waiting for import process to complete");
        while (!import.ImportState.Equals("Succeeded")) {
          Console.Write(".");
          Thread.Sleep(1000);
          import = pbiClient.Imports.GetImportById(import.Id);
        }

        Console.WriteLine();
        Console.WriteLine("Binding " + reportName + " to dataset...");
        string reportID = import.Reports[0].Id;
        RebindReportRequest bindRequest = new RebindReportRequest(datasetId);
        pbiClient.Reports.RebindReportInGroup(appWorkspaceId, reportID, bindRequest);
      }

    }

  }

  #region "Classes for deserializing JSON after creating dataset with direct REST calls"

  public class JsonDataset {
    public string id { get; set; }
    public string name { get; set; }
  }

  public class JsonDatasetCollection {
    public List<JsonDataset> value { get; set; }
  }

  #endregion

}
