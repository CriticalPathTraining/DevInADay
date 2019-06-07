using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetWriter.Models;

namespace DatasetWriter {
  class Program {
    static void Main() {

      DatasetManager.CreateTemperatureReadingsDataset();
     
      // DatasetManager.CreateCampaignContributionsDataset();
      
    }
  }
}
