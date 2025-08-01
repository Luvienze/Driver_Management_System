using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles = "ADMIN,CHIEF")]
    public class ChiefController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ChiefController(HttpClient httpClient, IOptions<ApiSettings> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetChiefList()
        {
            var response = await _httpClient.PostAsync($"{_baseUrl}/chief/list/name", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                return StatusCode((int)response.StatusCode, errorContent);
            }
            var chiefJson = await response.Content.ReadAsStringAsync();
            return Content(chiefJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> GetChiefById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id is null or lower than zero.");

                var requestUrl = $"{_baseUrl}/chief/find?id={id}";

                var response = await _httpClient.PostAsync(requestUrl, null);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Failed to retrieve chief data: {response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                    return NotFound("Chief data is empty.");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var chief = JsonSerializer.Deserialize<ChiefDto>(responseString, options);
                if (chief == null)
                    return NotFound("Chief not found.");

                return Ok(chief);
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

        [HttpPost]
        public async Task<IActionResult> GetChiefByRegistrationNumber(string registrationNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                    return BadRequest("Registration number is required.");

                var requestUrl = $"{_baseUrl}/chief/find/registrationNumber?registrationNumber={Uri.EscapeDataString(registrationNumber)}";

                var response = await _httpClient.PostAsync(requestUrl, null);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Failed to retrieve driver data: {response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                    return NotFound("Chief data is empty.");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var chief = JsonSerializer.Deserialize<ChiefDto>(responseString, options);
                if (chief == null)
                    return NotFound("Chief not found.");

                return Ok(chief);
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

        [HttpPost]
        public async Task<IActionResult> GetPersonChiefByRegistrationNumber(string registrationNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                    return BadRequest("Registration number is required.");

                var requestUrl = $"{_baseUrl}/chief/find/driver?registrationNumber={Uri.EscapeDataString(registrationNumber)}";

                var response = await _httpClient.PostAsync(requestUrl, null);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Failed to retrieve driver data: {response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                    return NotFound("Chief data is empty.");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var chief = JsonSerializer.Deserialize<ChiefDto>(responseString, options);
                if (chief == null)
                    return NotFound("Chief not found.");

                return Ok(chief);
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

        public async Task<IActionResult> GetActiveDrivers([FromForm] ChiefDto request)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("chiefId", request.Id.ToString())
            });
     
            var javaApiUrl = $"{_baseUrl}/driver/list/active";

            var response = await _httpClient.PostAsync(javaApiUrl, formContent);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "An error has been occured from backend.");
            }

            var json = await response.Content.ReadAsStringAsync();

            var drivers = JsonConvert.DeserializeObject<List<DriverDto>>(json);

            return Ok(drivers);
        }

    }
}
