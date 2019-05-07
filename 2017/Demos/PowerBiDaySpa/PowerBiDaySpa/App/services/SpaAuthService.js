var myApp;
(function (myApp) {
    var SpaAuthService = (function () {
        function SpaAuthService() {
        }
        return SpaAuthService;
    }());
    SpaAuthService.powerBiApiResourceId = "https://analysis.windows.net/powerbi/api";
    SpaAuthService.userName = "";
    SpaAuthService.userIsAuthenticated = false;
    SpaAuthService.login = function () {
        var config = {
            tenant: "common",
            clientId: "106e85da-729f-403a-9194-8610944cc6da",
            redirectUri: window.location.origin,
            cacheLocation: "sessionStorage",
            postLogoutRedirectUri: window.location.origin,
            endpoints: { "https://api.powerbi.com/v1.0/": "https://analysis.windows.net/powerbi/api" },
        };
        var authContext = new AuthenticationContext(config);
        if (authContext.isCallback(window.location.hash)) {
            // Handle redirect after token requests
            authContext.handleWindowCallback();
            var err = authContext.getLoginError();
            if (err) {
                // TODO: Handle errors signing in and getting tokens
                alert('ERROR:\n\n' + err);
            }
        }
        else {
            // If logged in, get access token and make an API request
            var user = authContext.getCachedUser();
            if (user) {
                // Get an access token to the Microsoft Graph API
                authContext.acquireToken(SpaAuthService.powerBiApiResourceId, function (error, token) {
                    console.log("7");
                    if (error || !token) {
                        // TODO: Handle error obtaining access token
                        alert('ERROR:\n\n' + error);
                        return;
                    }
                    // Use the access token
                    SpaAuthService.userIsAuthenticated = true;
                });
            }
            else {
                authContext.login();
            }
            var userDisplayName = authContext.getCachedUser().profile["name"];
            console.log(userDisplayName);
            $("#user-greeting").text("Hello " + userDisplayName);
            authContext.acquireToken(SpaAuthService.powerBiApiResourceId, function (message, token) {
                SpaAuthService.accessToken = token;
            });
            SpaAuthService.authContext = authContext;
        }
    };
    SpaAuthService.logout = function () {
        SpaAuthService.authContext.logOut();
        SpaAuthService.authContext.clearCache();
        SpaAuthService.userName = "";
        SpaAuthService.accessToken = "";
        $("#user-greeting").text("");
        $("#login").show();
        $("#logout").hide();
    };
    myApp.SpaAuthService = SpaAuthService;
})(myApp || (myApp = {}));
//# sourceMappingURL=SpaAuthService.js.map