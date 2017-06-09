using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReporterMVC.Models {

  public class ReportsViewModel {
    public bool UserIsAuthenticated;
    public List<Report> Reports;
    public Report CurrentReport;
    public string AccessToken;
  }

  public class ReportViewModel {
    public Report Report { get; set; }
    public string AccessToken { get; set; }
  }


}