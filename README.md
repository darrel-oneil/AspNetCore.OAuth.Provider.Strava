# AspNetCore.OAuth.Provider.Strava
ASP.NET Core 2.0 security middleware for Strava authentication.

http://developers.strava.com/docs/authentication/

Installation
---
The security middleware for Strava authentication is available as a [NuGet](https://www.nuget.org/packages/AspNetCore.OAuth.Provider.Strava) package.

To install from the package manager

```
PM> Install-Package AspNetCore.OAuth.Provider.Strava
```

To install from the .NET CLI

```
> dotnet add package AspNetCore.OAuth.Provider.Strava
```

## Getting started

To use add the following to the *ConfigureServices* methods in your *Startup.cs* class:

```
services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddStrava(options =>
{
    options.ClientId = Configuration.GetValue<int>("Strava:ClientId").ToString();
    options.ClientSecret = Configuration.GetValue<string>("Strava:ClientSecret");
});
```
## Access token scope

By default, Strava applications can only view a user's public data. The scope parameter can be used to request more access. It is recommended to only request the minimum amount of access necessary.

* `public`: default, private activities are not returned, privacy zones are respected in stream requests.
* `write`: modify activities, upload on the user’s behalf.
* `view_private`: view private activities and data within privacy zones.
* `view_private,write`: both ‘view_private’ and ‘write’ access.

For example, to change the scope from the `public` default to `view_private,write`:

```
app.UseStrava(options =>
{
    options.ClientId = "<Your Strava Apps OAuth 2.0 Client ID>";
    options.ClientSecret = "<Your Strava Apps OAuth 2.0 Client Secret>";
    options.Scope.Remove("public");
    options.Scope.Add("view_private");
    options.Scope.Add("write");
});
```
