using Microsoft.PowerBI.Api.V2.Models;
using System;
using System.Collections.Generic;

namespace DatasetWriter.Models {

  public class TemperatureReadingsRow {
    public string Run { get; set; }
    public DateTime Time { get; set; }
    public string TimeWindow { get; set; }
    public double TargetTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double BatchA { get; set; }
    public double BatchB { get; set; }
    public double BatchC { get; set; }
  }

  class TemperatureReadingsRows {
    public TemperatureReadingsRow[] rows { get; set; }
  }

  class TemperatureReadings {

    public static Table GetTableDefinition() {
      return new Table("TemperatureReadings",
              new List<Column> {
                new Column("Run", "string"),
                new Column("Time", "DateTime"),
                new Column("TimeWindow", "string"),
                new Column("TargetTemperature", "Double"),
                new Column("MinTemperature", "Double"),
                new Column("MaxTemperature", "Double"),
                new Column("BatchA", "Double"),
                new Column("BatchB", "Double"),
                new Column("BatchC", "Double")
              });
    }

    // data for simulation
    static int RunCount = 1;
    static string RunName = "Run " + RunCount.ToString("00");
    static double temperatureBatchA = 100;
    static double temperatureBatchB = 100;
    static double temperatureBatchC = 100;
    static Random rand = new Random(714);

    static Boolean tempOnTheRise = true;
    static Boolean inTransition = false;
    static int transitionCounter = 0;

    public static TemperatureReadingsRows GetNextTemperatureRowset() {

      if (inTransition) {
        transitionCounter += 1;
        int transitionCountMax = tempOnTheRise ? 15 : 3;
        if (transitionCounter >= transitionCountMax) {
          inTransition = false;
          transitionCounter = 0;
        }
      }
      else {
        if (tempOnTheRise) {
          temperatureBatchA += rand.Next(-40, 380) / (double)100;
          if (temperatureBatchA > 212) { temperatureBatchA = 212; }
          temperatureBatchB += rand.Next(0, 340) / (double)100;
          if (temperatureBatchB > 212) { temperatureBatchB = 212; }
          temperatureBatchC += rand.Next(20, 332) / (double)100;
          if (temperatureBatchC > 212) { temperatureBatchC = 212; }
          if (temperatureBatchA == 212 && temperatureBatchB == 212 && temperatureBatchC == 212) {
            tempOnTheRise = false;
            inTransition = true;
          }
        }
        else {
          temperatureBatchA -= rand.Next(0, 1020) / (double)100;
          if (temperatureBatchA < 100) { temperatureBatchA = 100; }
          temperatureBatchB -= rand.Next(100, 980) / (double)100;
          if (temperatureBatchB < 100) { temperatureBatchB = 100; }
          temperatureBatchC -= rand.Next(200, 1300) / (double)100;
          if (temperatureBatchC < 100) { temperatureBatchC = 100; }
          if (temperatureBatchA == 100 && temperatureBatchB == 100 && temperatureBatchC == 100) {
            tempOnTheRise = true;
            inTransition = true;
            RunCount += 1;
            RunName = "Run " + RunCount.ToString("00");
          }
        }
      }

      string currentTimeWindow = DateTime.Now.Hour.ToString("00") + ":" +
                                DateTime.Now.Minute.ToString("00") + ":" +
                                ((DateTime.Now.Second / 15) * 15).ToString("00");

      TemperatureReadingsRow row = new TemperatureReadingsRow {
        Run = RunName,
        Time = DateTime.Now,
        TimeWindow = currentTimeWindow,
        TargetTemperature = 212,
        MinTemperature = 100,
        MaxTemperature = 250,
        BatchA = temperatureBatchA,
        BatchB = temperatureBatchB,
        BatchC = temperatureBatchC,
      };

      TemperatureReadingsRow[] rows = { row };
      TemperatureReadingsRows temperatureReadingsRows = new TemperatureReadingsRows { rows = rows };
      return temperatureReadingsRows;
    }

  }
}
