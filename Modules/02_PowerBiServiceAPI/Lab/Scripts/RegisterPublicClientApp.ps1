# log into Azure AD
$userName = "tedp@devinaday.onMicrosoft.com"
$password = "Pa`$`$word!"

$securePassword = ConvertTo-SecureString –String $password –AsPlainText -Force
$credential = New-Object –TypeName System.Management.Automation.PSCredential `
                         –ArgumentList $userName, $securePassword

$authResult = Connect-AzureAD -Credential $credential

# register a new public client app

$appDisplayName = "My Public Client App"

# get more info about the logged in user
$user = Get-AzureADUser -ObjectId $authResult.Account.Id

# create Azure AD Application
$replyUrl = "https://localhost/app1234"
$aadApplication = New-AzureADApplication `
                        -DisplayName "My Public Client App" `
                        -PublicClient $true `
                        -AvailableToOtherTenants $false `
                        -ReplyUrls @($replyUrl)

# create service principal for application
$appId = $aadApplication.AppId
$serviceServicePrincipal = New-AzureADServicePrincipal -AppId $appId

# assign current user as application owner
Add-AzureADApplicationOwner -ObjectId $aadApplication.ObjectId -RefObjectId $user.ObjectId

# configure delegated permisssions for the Power BI Service API
$requiredAccess = New-Object -TypeName "Microsoft.Open.AzureAD.Model.RequiredResourceAccess"
$requiredAccess.ResourceAppId = "00000009-0000-0000-c000-000000000000"

# create first delegated permission - Report.Read.All
$permission1 = New-Object -TypeName "Microsoft.Open.AzureAD.Model.ResourceAccess" `
                          -ArgumentList "4ae1bf56-f562-4747-b7bc-2fa0874ed46f","Scope"

# create second delegated permission - Dashboards.Read.All
$permission2 = New-Object -TypeName "Microsoft.Open.AzureAD.Model.ResourceAccess" `
                          -ArgumentList "2448370f-f988-42cd-909c-6528efd67c1a","Scope"

# add permissions to ResourceAccess list
$requiredAccess.ResourceAccess = $permission1, $permission2

# add permissions by updating application with RequiredResourceAccess object
Set-AzureADApplication -ObjectId $aadApplication.ObjectId -RequiredResourceAccess $requiredAccess

$outputFile = "$PSScriptRoot\PublicClientAppInfo.txt"
Out-File -FilePath $outputFile -InputObject "--- New Azure AD Public Client App Info ---"
Out-File -FilePath $outputFile -Append -InputObject "AppId: $appId"
Out-File -FilePath $outputFile -Append -InputObject "ReplyUrl: $replyUrl"

Notepad $outputFile