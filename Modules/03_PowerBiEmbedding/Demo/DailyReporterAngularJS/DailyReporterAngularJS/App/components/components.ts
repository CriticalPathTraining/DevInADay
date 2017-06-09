module myApp {

  let app = angular.module("myApp");

  class TopNavController {
    static $inject: string[] = ['adalAuthenticationService'];

    adal: adal.AdalAuthenticationService;
    isUserAuthenticated: boolean;
    userInfo: adal.UserInfo;
    login: Function;
    logout: Function;

    constructor(private adalAuthenticationService: adal.AdalAuthenticationService) {
      console.log("TopNav contructor...");
    };

    public $onInit() {
      console.log("TopNav $onInit...");
      this.adal = this.adalAuthenticationService;
      this.isUserAuthenticated = this.adalAuthenticationService.userInfo.isAuthenticated;
      this.userInfo = this.adalAuthenticationService.userInfo;
      this.login = this.adalAuthenticationService.login;
      this.logout = this.adalAuthenticationService.logOut;
    }

    public loadReport(reportId: string): void {
      console.log("Loading report " + reportId);
    }

  }

  class TopNav implements ng.IComponentOptions {

    public bindings: { [binding: string]: string };
    public controller: any;
    public templateUrl: any;

    constructor() {
      this.bindings = {};
      this.controller = TopNavController;
      this.templateUrl = '/App/components/topNav.html';
    }

  }

  app.component("topNav", new TopNav());

}