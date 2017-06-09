module myApp {

  let app = angular.module("myApp");

  class HomeController {
    static $inject: Array<string> = [];
    welcomeMessage = "Welcome to the Wingtip Product Manager";
    topic1Title = "Add a new product";
    topic1Copy = "Click the Add Product link on the navbar aboive to add a new product.";
    topic2Title = "See the Product Showcase";
    topic2Copy = "Click Product Showcase link in the navbar to see the full set of Wingtip products.";
    constructor() { }
  }

  app.controller('homeController', HomeController);

  class ReportsController {
    static $inject: Array<string> = ['PowerBi'];

    reports: IReport[];

    constructor(private PowerBi: IPowerBiService) {
      console.log("ReportsController  constructor");
      console.log("Calling GetReports...");
      if (this.reports === undefined) {
        this.PowerBi.GetReports().then(
          (response: ng.IHttpPromiseCallbackArg<IReportCollection>) => {
            console.log("GetReports Callback...");
            this.reports = response.data.value;
          }
        );
      }
    }

    public loadReport = (reportId: string): void => {
      console.log("LoadReport: " + reportId);
    }


  }

  app.controller('reportsController', ReportsController);

  class DashboardsController {
    static $inject: Array<string> = ['$location'];
    constructor(private $location: ng.ILocationService) {
    }
  }

  app.controller('dashboardsController', DashboardsController);


}
