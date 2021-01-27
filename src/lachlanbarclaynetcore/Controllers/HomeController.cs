using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using lachlanbarclaynetcore.Models;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lachlanbarclaynetcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var exception = exceptionHandlerPathFeature?.Error;

            if (exception != null)
            {
                // Log this exception to disk or database or just post a notification somewhere
                Debug.WriteLine(exception.ToString());
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
