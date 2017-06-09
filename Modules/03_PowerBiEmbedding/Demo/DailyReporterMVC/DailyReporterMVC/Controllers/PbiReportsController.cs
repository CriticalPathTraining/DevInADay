using DailyReporterMVC.Models;
using DailyReporterMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DailyReporterMVC.Controllers {

  [Authorize]
  public class PbiReportsController : Controller {
    public async Task<ActionResult> Index() {
      ReportsViewModel reportsViewModel = await PowerBiService.GetReports("");

      return View(reportsViewModel);
    }
  }


}