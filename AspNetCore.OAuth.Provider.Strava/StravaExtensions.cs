using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.OAuth.Provider.Strava
{
    public static class StravaExtensions
    {
        public static AuthenticationBuilder AddStrava(this AuthenticationBuilder builder) 
            => builder.AddStrava(StravaDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddStrava(this AuthenticationBuilder builder, Action<StravaOptions> configureOptions)
            => builder.AddStrava(StravaDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddStrava(this AuthenticationBuilder builder, string authenticationScheme, Action<StravaOptions> configureOptions)
            => builder.AddStrava(authenticationScheme, StravaDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddStrava(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<StravaOptions> configureOptions)
            => builder.AddOAuth<StravaOptions, StravaHandler>(authenticationScheme, displayName, configureOptions);
    }
}
