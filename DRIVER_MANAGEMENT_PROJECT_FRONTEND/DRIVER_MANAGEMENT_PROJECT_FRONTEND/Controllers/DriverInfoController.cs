using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles ="DRIVER")]
    public class DriverInfoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public DriverInfoController (IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = options.Value.BaseUrl;
        }

        [HttpPost]
        public async Task<IActionResult> GetDriverDetail(string registrationNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                    return BadRequest("Registration number is required.");

                var requestUrl = $"{_baseUrl}/person/find/driver?registrationNumber={Uri.EscapeDataString(registrationNumber)}";

                var response = await _httpClient.PostAsync(requestUrl, null);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Failed to retrieve wrapper class data: {response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                    return NotFound("Wrapper class data is empty.");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var personDriverRequest = JsonSerializer.Deserialize<PersonDriverRequestDto>(responseString, options);
                if (personDriverRequest == null)
                    return NotFound("Driver not found.");

                return Ok(personDriverRequest);
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Error deserializing wrapper class data: {ex.Message}");
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
        public async Task<IActionResult> DriverInfoPage(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                return BadRequest("Registration number is required.");

            var requestUrl = $"{_baseUrl}/person/find/driver?registrationNumber={Uri.EscapeDataString(registrationNumber)}";
            var response = await _httpClient.PostAsync(requestUrl, null);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, $"Failed to retrieve wrapper class data: {response.ReasonPhrase}");

            var responseString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseString))
                return NotFound("Wrapper class data is empty.");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var personDriverRequest = JsonSerializer.Deserialize<PersonDriverRequestDto>(responseString, options);

            if (personDriverRequest == null)
                return NotFound("Driver not found.");

            return View(personDriverRequest);
        }
    }
}
