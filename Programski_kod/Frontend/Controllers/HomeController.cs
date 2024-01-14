using System.Diagnostics;
using System.Net.Http;
using ClassLibrary;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string apiUrl = "https://localhost:7223/api/data";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Datatable(string searchText, string filterColumn)
        {
            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(filterColumn))
            {
                try
                {
                    using (HttpClient _httpClient = new HttpClient())
                    {

                        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl + $"?searchText={searchText}&filterColumn={filterColumn}");

                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();

                            var apiResponseObject = JsonConvert.DeserializeObject<ApiResponse<List<NatjecanjeDto>>>(apiResponse);

                            return View(apiResponseObject.Data);
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            else
            {
                try
                {
                    using (HttpClient _httpClient = new HttpClient())
                    {

                        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();

                            var apiResponseObject = JsonConvert.DeserializeObject<ApiResponse<List<NatjecanjeDto>>>(apiResponse);

                            return View(apiResponseObject.Data);
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DownloadCsv()
        {
            var filePath = "C:\\Users\\filip\\Desktop\\Faks\\4. godina\\Zimski\\Otvoreno_racunarstvo\\Github\\sportska_natjecanja.csv";
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(fileStream, "application/csv", "sportska_natjecanja.csv");
        }

        public IActionResult DownloadJson()
        {
            var filePath = "C:\\Users\\filip\\Desktop\\Faks\\4. godina\\Zimski\\Otvoreno_racunarstvo\\Github\\sportska_natjecanja.json";
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(fileStream, "application/json", "sportska_natjecanja.json");
        }
    }
}