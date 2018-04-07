# AspNetCore.OAuth.Provider.Strava
ASP.NET Core 2.0 security middleware for Strava authentication.

http://developers.strava.com/docs/authentication/

## Getting started

To use add the following to the *ConfigureServices* methods in your *Startup.cs* class:

```
app.UseStrava(options =>
{
    options.ClientId = "<Your Strava Apps OAuth 2.0 Client ID>";
    options.ClientSecret = "<Your Strava Apps OAuth 2.0 Client Secret>";
});
```
## Access token scope

By default, Strava applications can only view a user’s public data. The scope parameter can be used to request more access. It is recommended to only requested the minimum amount of access necessary.

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
