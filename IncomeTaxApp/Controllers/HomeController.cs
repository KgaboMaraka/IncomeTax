using IncomeTaxApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace IncomeTaxApp.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            _configuration = iConfig;
        }

        public IActionResult Index()
        {
            ModelState.Clear();
            return View("Index");
        }

        public async Task<IActionResult> Calculate(TaxCalculationRequest model)
        {
            var taxAmount = await CalculateAndSaveTax(model);
            return View(model);
        }

        private async Task<decimal> CalculateAndSaveTax(TaxCalculationRequest taxCalculationRequest)
        {
            using (var client = new HttpClient())
            {
                string baseUrl = _configuration["BaseUrl"] ?? "";
                string requestUri = "api/IncomeTax/Calculate?postalCode=" + taxCalculationRequest.PostalCode + "&income=" + taxCalculationRequest.Income;
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(requestUri, null);
                if (response.IsSuccessStatusCode)
                {
                    taxCalculationRequest.TaxAmount = decimal.Parse(response.Content.ReadAsStringAsync().Result);
                }
            }

            return taxCalculationRequest.TaxAmount;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}