using Microsoft.PowerBI.Api.V2.Models;
using System;
using System.Collections.Generic;

namespace RealtimeDashboardDemo.Models {

  public class StateRow {
    public string State { get; set; }
    public DateTime Admitted { get; set; }
    public Int64 Population { get; set; }
  }

  class StateRowSet {
    public StateRow[] rows { get; set; }
  }


  class StatesFactory {

    public static Table GetTableDefinition() {
      return new Table(
              name: "States",
              columns: new List<Column> {
                new Column("State", "string"),
                new Column("Admitted", "DateTime"),
                new Column("Population", "Int64")
              });
    }

    public static StateRowSet[] GetStates() {

      List<StateRowSet> stateRowsets = new List<StateRowSet>();

      foreach (var stateRow in StateRows) {
        stateRowsets.Add(
          new StateRowSet { rows = new StateRow[] { stateRow } }
        );
      }

      return stateRowsets.ToArray();
    }

    private static StateRow[] StateRows =
       new StateRow[] {
          new StateRow { State = "Delaware", Admitted = DateTime.Parse("1787-12-07"), Population =961939 },
          new StateRow { State = "Pennsylvania", Admitted = DateTime.Parse("1787-12-12"), Population =12805537 },
          new StateRow { State = "New Jersey", Admitted = DateTime.Parse("1787-12-18"), Population =9005644 },
          new StateRow { State = "Georgia", Admitted = DateTime.Parse("1788-01-02"), Population =10429379 },
          new StateRow { State = "Connecticut", Admitted = DateTime.Parse("1788-01-09"), Population =3588184 },
          new StateRow { State = "Massachusetts", Admitted = DateTime.Parse("1788-02-06"), Population =6859819 },
          new StateRow { State = "Maryland", Admitted = DateTime.Parse("1788-04-28"), Population =6052177 },
          new StateRow { State = "South Carolina", Admitted = DateTime.Parse("1788-05-23"), Population =5024369 },
          new StateRow { State = "New Hampshire", Admitted = DateTime.Parse("1788-06-21"), Population =1342795 },
          new StateRow { State = "Virginia", Admitted = DateTime.Parse("1788-06-25"), Population =8470020 },
          new StateRow { State = "New York", Admitted = DateTime.Parse("1788-07-26"), Population =19849399 },
          new StateRow { State = "North Carolina", Admitted = DateTime.Parse("1789-11-21"), Population =10273419 },
          new StateRow { State = "Rhode Island", Admitted = DateTime.Parse("1790-05-29"), Population =1059639 },
          new StateRow { State = "Vermont", Admitted = DateTime.Parse("1791-03-04"), Population =623657 },
          new StateRow { State = "Kentucky", Admitted = DateTime.Parse("1792-06-01"), Population =4454189 },
          new StateRow { State = "Tennessee", Admitted = DateTime.Parse("1796-06-01"), Population =6715984 },
          new StateRow { State = "Ohio", Admitted = DateTime.Parse("1803-03-01"), Population =11658609 },
          new StateRow { State = "Louisiana", Admitted = DateTime.Parse("1812-04-30"), Population =4684333 },
          new StateRow { State = "Indiana", Admitted = DateTime.Parse("1816-12-11"), Population =6666818 },
          new StateRow { State = "Mississippi", Admitted = DateTime.Parse("1817-12-10"), Population =2984100 },
          new StateRow { State = "Illinois", Admitted = DateTime.Parse("1818-12-03"), Population =12802023 },
          new StateRow { State = "Alabama", Admitted = DateTime.Parse("1819-12-19"), Population =4874747 },
          new StateRow { State = "Maine", Admitted = DateTime.Parse("1820-03-15"), Population =1335907 },
          new StateRow { State = "Missouri", Admitted = DateTime.Parse("1821-08-10"), Population =6113532 },
          new StateRow { State = "Arkansas", Admitted = DateTime.Parse("1836-06-15"), Population =3004279 },
          new StateRow { State = "Michigan", Admitted = DateTime.Parse("1837-01-26"), Population =9962311 },
          new StateRow { State = "Florida", Admitted = DateTime.Parse("1845-03-03"), Population =20984400 },
          new StateRow { State = "Texas", Admitted = DateTime.Parse("1845-12-29"), Population =28304596 },
          new StateRow { State = "Iowa", Admitted = DateTime.Parse("1846-12-28"), Population =3145711 },
          new StateRow { State = "Wisconsin", Admitted = DateTime.Parse("1848-05-29"), Population =5795483 },
          new StateRow { State = "California", Admitted = DateTime.Parse("1850-09-09"), Population =39536653 },
          new StateRow { State = "Minnesota", Admitted = DateTime.Parse("1858-05-11"), Population =5576606 },
          new StateRow { State = "Oregon", Admitted = DateTime.Parse("1859-02-14"), Population =4142776 },
          new StateRow { State = "Kansas", Admitted = DateTime.Parse("1861-01-29"), Population =2913123 },
          new StateRow { State = "West Virginia", Admitted = DateTime.Parse("1863-06-20"), Population =1815857 },
          new StateRow { State = "Nevada", Admitted = DateTime.Parse("1864-10-31"), Population =2998039 },
          new StateRow { State = "Nebraska", Admitted = DateTime.Parse("1867-03-01"), Population =1920076 },
          new StateRow { State = "Colorado", Admitted = DateTime.Parse("1876-08-01"), Population =5607154 },
          new StateRow { State = "North Dakota", Admitted = DateTime.Parse("1889-11-02"), Population =755393 },
          new StateRow { State = "South Dakota", Admitted = DateTime.Parse("1889-11-02"), Population =869666 },
          new StateRow { State = "Montana", Admitted = DateTime.Parse("1889-11-08"), Population =1050493 },
          new StateRow { State = "Washington", Admitted = DateTime.Parse("1889-11-11"), Population =7405743 },
          new StateRow { State = "Idaho", Admitted = DateTime.Parse("1890-07-03"), Population =1716943 },
          new StateRow { State = "Wyoming", Admitted = DateTime.Parse("1890-07-10"), Population =579315 },
          new StateRow { State = "Utah", Admitted = DateTime.Parse("1896-01-04"), Population =3101833 },
          new StateRow { State = "Oklahoma", Admitted = DateTime.Parse("1907-11-16"), Population =3930864 },
          new StateRow { State = "New Mexico", Admitted = DateTime.Parse("1912-01-26"), Population =2088070 },
          new StateRow { State = "Arizona", Admitted = DateTime.Parse("1912-02-14"), Population =7016270 },
          new StateRow { State = "Alaska", Admitted = DateTime.Parse("1959-01-03"), Population =739795 },
          new StateRow { State = "Hawaii", Admitted = DateTime.Parse("1959-08-21"), Population =1427538 }
       };
  };

}

