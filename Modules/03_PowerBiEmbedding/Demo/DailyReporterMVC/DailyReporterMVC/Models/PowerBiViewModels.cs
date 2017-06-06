using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReporterMVC.Models {

  public class ReportsViewModel {
    public List<Report> reports;
    public Report defaultReport;
    public string AccessToken;
  }

}