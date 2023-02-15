using CachingExample.Models;
using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CachingExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SampleDataAccess _dataAccess;

        public HomeController(ILogger<HomeController> logger, SampleDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        public async Task<IActionResult> Index()
        {
            List<EmployeeModel> employees;
            employees = await _dataAccess.GetEmployeesCached();
            foreach(var e in employees)
            {
                Console.WriteLine($"Employee data: {e.FirstName}, {e.LastName}");
            }
            return View(employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}