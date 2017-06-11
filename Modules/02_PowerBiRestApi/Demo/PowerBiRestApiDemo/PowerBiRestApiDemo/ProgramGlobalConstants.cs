using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBiRestApiDemo {

  class ProgramGlobalConstants {

    public const string AzureAuthorizationEndpoint = "https://login.microsoftonline.com/common";
    public const string PowerBiServiceResourceUri = "https://analysis.windows.net/powerbi/api";
    public const string PowerBiServiceRootUrl = "https://api.powerbi.com/v1.0/myorg/";

    public const string ClientID = "17ffd1fa-ca5a-468c-9f29-74956a2a74ff";
    public const string RedirectUri = "https://localhost/PbixInstallerForPowerBI";

    public const string DatasetName = "My Custom Dataset";

  }
}

