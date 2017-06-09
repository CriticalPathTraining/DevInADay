module myApp {

  export class PowerBiService implements IPowerBiService {

    apiRoot: string = "https://api.powerbi.com/v1.0/myOrg/";
    

    //static $inject: string[] = ["$http", "adalAuthenticationService"];
    
    //constructor(private $http: ng.IHttpService, private adalAuthenticationService: adal.AdalAuthenticationService) {      }

    static $inject: string[] = ["$q", "$http", "adalAuthenticationService"];
    constructor(private $q: ng.IQService, private $http: ng.IHttpService, private adalAuthenticationService: adal.AdalAuthenticationService) {
    }

    GetReports(): ng.IPromise<IReportCollection> {
      console.log("GetReports...");
      var restUrl = this.apiRoot + "reports/";
      console.log("Calling across network to " + restUrl);
      return this.$http.get<IReportCollection>(restUrl, );
    }


    GetReports2(): ng.IPromise<IReportCollection> {
      var reports: IReportCollection = {
        "value": [
          {
            "id": "a8e7b314-2673-4d5f-8ef1-52181f6597cb",
            "name": "Northwindi Retro",
            "webUrl": "https://app.powerbi.com/reports/a8e7b314-2673-4d5f-8ef1-52181f6597cb",
            "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=a8e7b314-2673-4d5f-8ef1-52181f6597cb"
          },
          {
            "id": "77067def-00a2-4a93-80f6-e37eb3b633a4",
            "name": "Wingtip Sales",
            "webUrl": "https://app.powerbi.com/reports/77067def-00a2-4a93-80f6-e37eb3b633a4",
            "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=77067def-00a2-4a93-80f6-e37eb3b633a4"
          },
          {
            "id": "75aac1ff-61b2-443c-bf06-5d6f666ad293",
            "name": "Wingtip Sales DQ",
            "webUrl": "https://app.powerbi.com/reports/75aac1ff-61b2-443c-bf06-5d6f666ad293",
            "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=75aac1ff-61b2-443c-bf06-5d6f666ad293"
          }]
      };
      return this.$q.when(reports);
    }

    GetReport(id: string): ng.IPromise<IReport> {
      var report: IReport = {
        "id": "a8e7b314-2673-4d5f-8ef1-52181f6597cb",
        "name": "Northwindi Retro",
        "webUrl": "https://app.powerbi.com/reports/a8e7b314-2673-4d5f-8ef1-52181f6597cb",
        "embedUrl": "https://app.powerbi.com/reportEmbed?reportId=a8e7b314-2673-4d5f-8ef1-52181f6597cb"
      }
      return this.$q.when(report);
    }

    


    getReports(): string {
      console.log("getReports");
      return "Hey Man!!!!";
    }

    getDashboards() {}
    
  }
  
  angular.module('myApp').service('PowerBi', PowerBiService);

}



