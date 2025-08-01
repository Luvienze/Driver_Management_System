using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles =("ADMIN, CHIEF"))]
    public class PersonController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public PersonController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = options.Value.BaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            var persons = await GetPersonsAsync();
            return View(persons);
        }

        private async Task<List<PersonDto>> GetPersonsAsync()
        {
            var requestUrl = $"{_baseUrl}/person/list";

            var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() } 
            };

            var persons = JsonSerializer.Deserialize<List<PersonDto>>(responseString, options);

            return persons;
        }

        [HttpPost]
        public async Task<IActionResult> GetPersonCard([FromForm] string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                return BadRequest("Registration number is required.");

            var formContent = new FormUrlEncodedContent(new[]
            {
             new KeyValuePair<string, string>("registrationNumber", registrationNumber)
            });

            var response = await _httpClient.PostAsync($"{_baseUrl}/person/find/registrationNumber", formContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<PersonDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (person == null)
                return NotFound();

            return PartialView("_PersonCard", person);
        }

    }
}