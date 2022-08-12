using System;
using CoursesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using CoursesApp.Data.Entities;

namespace CoursesApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController(/*ILogger logger*/)
        {
            //_logger = logger ?? throw new ArgumentNullException("Logger can't be null", nameof(logger));
        }
        
        public IActionResult Index()
        {
            //_logger.LogInformation("Act index method");

            return View("Index");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
