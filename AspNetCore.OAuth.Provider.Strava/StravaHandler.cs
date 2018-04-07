using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AspNetCore.OAuth.Provider.Strava
{
    public class StravaHandler : OAuthHandler<StravaOptions>
    {
        /// <summary>
        /// Authentication handler for Strava authentication
        /// </summary>
        public StravaHandler(
            IOptionsMonitor<StravaOptions> options,
            ILoggerFactory factory,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, factory, encoder, clock) {}

        protected override async Task<AuthenticationTicket> CreateTicketAsync(
            ClaimsIdentity identity, 
            AuthenticationProperties properties, 
            OAuthTokenResponse tokens)
        {
            // Get the Strava user
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

            var response = await Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError("An error occurred when retrieving Strava user information: the remote server " +
                                "returned a {Status} response with the following payload: {Headers} {Body}.",
                                response.StatusCode,
                                response.Headers.ToString(),
                                await response.Content.ReadAsStringAsync());

                throw new HttpRequestException($"An error occurred when retrieving Strava user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Strava API is enabled.");
            }

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload);
            context.RunClaimActions();

            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }

        protected override string FormatScope()
        {
            // Strava deviates from the OAuth spec and requires comma separated scopes instead of space separated.
            return string.Join(",", Options.Scope);
        }
    }
}
