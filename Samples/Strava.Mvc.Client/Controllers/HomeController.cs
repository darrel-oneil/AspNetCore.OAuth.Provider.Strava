using Microsoft.AspNetCore.Mvc;
using Strava.Mvc.Client.Models;
using System.Diagnostics;

namespace Strava.Mvc.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
