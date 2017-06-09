module myApp {

  export interface IReport {
    id?: string;
    modelId?: number;
    name: string;
    webUrl?: string;
    embedUrl?: string;
    isOwnedByMe?: boolean;
    isOriginalPbixReport?: boolean;
    datasetId?: string;
  }

  export interface IReportCollection{
    value: IReport[];
  }

  
  export interface IPowerBiService{
    GetReports(): ng.IPromise<IReportCollection>;
    GetReport(id: string): ng.IPromise<IReport>;
  }

}