using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles ="ADMIN,CHIEF")]
    public class VehicleController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public VehicleController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = options.Value.BaseUrl;
        }

        /// <summary>
        ///     Returns Vehicle/Index.cshtml
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        ///     Displays a card modal that shows information about vehicle by it's door number.
        ///     Calls vehicle/find api and sends door number, returns partial view, or null if not found.
        ///     <paramref name="doorNo"/>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetVehicleCard([FromForm] string doorNo)
        {

            if (string.IsNullOrWhiteSpace(doorNo))
                return BadRequest("Door number is required.");

            var formContent = new FormUrlEncodedContent(new[]
            {
             new KeyValuePair<string, string>("doorNo", doorNo) 
            });

            var response = await _httpClient.PostAsync($"{_baseUrl}/vehicle/find", formContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var vehicle = JsonSerializer.Deserialize<VehicleDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (vehicle == null)
                return NotFound();

            return PartialView("_VehicleCard", vehicle);
        }

        /// <summary>
        ///     Retrieves a vehicle list by assocaited vehicles with chief's garage by chief's id.
        ///     Calls vehicle/list/garages/id api and sends chief id.
        ///     <paramref name="chief"/>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetVehicleListByChief(int chief)
        {
            var response = await _httpClient.PostAsync($"{_baseUrl}/vehicle/list/garages/id?id={chief}", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                return StatusCode((int)response.StatusCode, errorContent);
            }

            var vehicleJson = await response.Content.ReadAsStringAsync();
            return Content(vehicleJson, "application/json");
        }

        /// <summary>
        ///     Retrieves a vehicle list by it's status.
        ///     Calls vehicle/find/status api and sends vehicle's status as integer value.
        ///     <paramref name="status"/>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetVehicleByStatus([FromBody] int status)
        {
            try
            {
                var springApiUrl = $"{_baseUrl}/vehicle/find/status";
                var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(springApiUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Spring API hatası: {errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                int count = JsonConvert.DeserializeObject<int>(responseContent);

                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

    }
}
