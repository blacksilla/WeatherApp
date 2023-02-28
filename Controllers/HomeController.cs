using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.Controllers
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


        public IActionResult Results()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> WeatherApiForm(Weather cw)
        {

            //API

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/" + cw.LocationName + "?key=B4ELH6C3FVGXL7Q5WY5QJZMYP");
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
                //lança um erro
            var body = await response.Content.ReadAsStringAsync();
            dynamic weather = JsonConvert.DeserializeObject(body);

            List<string> results = new List<string>();
            foreach (var day in weather.days)
            {
                results.Add("Forecast for date: "+ day.datetime);
                results.Add("General conditions will be: " + day.description);
                results.Add(" ");



            }

            ViewBag.output = results;

            return View("Results",cw);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}