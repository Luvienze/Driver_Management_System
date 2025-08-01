using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles = "ADMIN,CHIEF")]
    public class GarageController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public GarageController(HttpClient httpClient, IOptions<ApiSettings> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetGarageList()
        {
            var response = await _httpClient.PostAsync($"{_baseUrl}/garage/list/name", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                return StatusCode((int)response.StatusCode, errorContent);
            }

            var garageJson = await response.Content.ReadAsStringAsync();
            return Content(garageJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> GetGarageByRegistrationNumber(string registrationNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                    return BadRequest("Registration number is required.");

                var requestUrl = $"{_baseUrl}/garage/find/registrationNumber?registrationNumber={Uri.EscapeDataString(registrationNumber)}";

                var response = await _httpClient.PostAsync(requestUrl, null);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Failed to retrieve driver data: {response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                    return NotFound("Garage data is empty.");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var garage = JsonSerializer.Deserialize<GarageDto>(responseString, options);
                if (garage == null)
                    return NotFound("Garage not found.");

                return Ok(garage);
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Error deserializing garage data: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error communicating with external API: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
