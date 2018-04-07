using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AspNetCore.OAuth.Provider.Strava
{
    public class StravaOptions : OAuthOptions
    {
        /// <summary>
        /// Configuration options for <see cref="StravaHandler"/>.
        /// </summary>
        public StravaOptions()
        {
            ClaimsIssuer = StravaDefaults.Issuer;

            CallbackPath = new PathString(StravaDefaults.CallbackPath);

            AuthorizationEndpoint = StravaDefaults.AuthorizationEndpoint;
            TokenEndpoint = StravaDefaults.TokenEndpoint;
            UserInformationEndpoint = StravaDefaults.UserInformationEndpoint;

            Scope.Add("public");

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
            ClaimActions.MapJsonKey(ClaimTypes.GivenName, "firstname");
            ClaimActions.MapJsonKey(ClaimTypes.Surname, "lastname");
            ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
            ClaimActions.MapJsonKey(ClaimTypes.StateOrProvince, "state");
            ClaimActions.MapJsonKey(ClaimTypes.Country, "country");
            ClaimActions.MapJsonKey(ClaimTypes.Gender, "sex");
            ClaimActions.MapJsonKey("urn:strava:city", "city");
            ClaimActions.MapJsonKey("urn:strava:profile", "profile");
            ClaimActions.MapJsonKey("urn:strava:profile-medium", "profile_medium");
            ClaimActions.MapJsonKey("urn:strava:created-at", "created_at");
            ClaimActions.MapJsonKey("urn:strava:updated-at", "updated_at");
            ClaimActions.MapJsonKey("urn:strava:premium", "premium");
        }
    }
}
