using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Strava.Mvc.Client.Controllers
{
    public class StravaController : Controller
    {
        /// <summary>
        /// Instructs the middleware to redirect to the Strava OAuth 2.0 user login screen.
        /// After successful OAuth authentication the athletes profile response data is 
        /// added to the current user identity.
        /// </summary>
        public IActionResult Connect()
        {
            /// The authenticationSchemes parameter must be set to "Strava".
            return Challenge(new AuthenticationProperties { RedirectUri = "Strava/Connected" }, "Strava");
        }

        /// <summary>
        /// Strava login callback. 
        /// </summary>
        public IActionResult Connected()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Deletes the authentication cookie.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}