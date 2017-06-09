using DailyReporterMVC.Models;
using DailyReporterMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DailyReporterMVC.Controllers {


  public class HomeController : Controller {

    public async Task<ActionResult> Index(string reportId) {

      if (Request.IsAuthenticated) {
        ReportsViewModel reportsViewModel = await PowerBiService.GetReports(reportId);
        return View(reportsViewModel);
      }
      else {
        return View(new ReportsViewModel { UserIsAuthenticated = false });
      }

    }
  }
}