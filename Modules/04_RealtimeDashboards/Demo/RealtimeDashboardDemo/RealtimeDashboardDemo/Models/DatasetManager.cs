using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;


namespace RealtimeDashboardDemo.Models {

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

    public static void DisplayDatsetsInWorkspace() {

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

    public static void CreateStatesDataset() {

      string datasetName = "States Dataset";
      string tableName = StatesFactory.GetTableDefinition().Name;

      string datasetId = GetDatasetId(datasetName);

      if (datasetId == string.Empty) {
        Console.WriteLine("Creating the States streaming datasets...");

        Table tableDef = StatesFactory.GetTableDefinition();

        Dataset datasetRequest = new Dataset(datasetName,
                                 new List<Table> { tableDef },
                                 defaultMode: DatasetMode.PushStreaming);

        var datasetResponse = (Dataset)pbiClient.Datasets.PostDatasetInGroup(appWorkspaceId, datasetRequest);
        datasetId = datasetResponse.Id;
      }

      while (true) {
        Console.Write("Adding rows to the States datasets");

        pbiClient.Datasets.DeleteRows(appWorkspaceId, datasetId, tableName);

        foreach (var stateRowset in StatesFactory.GetStates()) {
          Console.Write(".");
          pbiClient.Datasets.PostRows(appWorkspaceId, datasetId, tableName, stateRowset);
          Thread.Sleep(1500);
        }

        Console.WriteLine();
        Console.WriteLine("All states have been added...");
        Thread.Sleep(5000);
        Console.Clear();
        Console.WriteLine("Starting again...");
        Console.WriteLine();

      }



    }

    public static void UploadReport(string datasetId) {
      //string reportName = "Campaign Report";
      //if (ReportDoesNotExist(reportName)) {
      //  Stream pbixStream = new MemoryStream(Properties.Resources.CampaignContributionsReport_pbix);
      //  Import import = pbiClient.Imports.PostImportWithFileInGroup(appWorkspaceId, pbixStream, reportName);
      //  import = pbiClient.Imports.GetImportById(import.Id);
      //  while (!import.ImportState.Equals("Succeeded")) {
      //    Thread.Sleep(1000);
      //    import = pbiClient.Imports.GetImportById(import.Id);
      //  }
      //  string reportID = import.Reports[0].Id;
      //  RebindReportRequest bindRequest = new RebindReportRequest(datasetId);
      //  pbiClient.Reports.RebindReportInGroup(appWorkspaceId, reportID, bindRequest);
      //}

    }

  }

}
