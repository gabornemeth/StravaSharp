# Getting started

You can access the full functionality of **StravaSharp** through the ```Client``` class. Basically you have to do the following.

```csharp
using StarvaSharp;
...

var client = new Client(authenticator);
// now you can use the Client
var activities = await client.Activities.GetAthleteActivities();
```

Notice the constructor of ```Client``` requires an authenticator. This is the only tricky part.
You have to implement ```RestSharp.Portable.IAuthenticator``` interface here. The purpose of the authenticator is to provide an access token which lets you access Strava API.

Some possible authenticator implementations:
* [Static authenticator](https://github.com/gabornemeth/StravaSharp/blob/master/src/StravaSharp.Tests/TestAuthenticator.cs): The access token can be set manually.
* [OAuth2 authenticator for web application](https://github.com/gabornemeth/StravaSharp/blob/master/src/Samples/Sample.Web/Authentication/Authenticator.cs): It lets the user authenticate herself using OAuth2 authentication workflow. To better understand the workflow here, also check the [usage of the authenticator](https://github.com/gabornemeth/StravaSharp/blob/master/src/Samples/Sample.Web/Controllers/HomeController.cs) in the web sample app.
* OAuth2 authenticator for mobile apps: Providing a sample code is in progress.
