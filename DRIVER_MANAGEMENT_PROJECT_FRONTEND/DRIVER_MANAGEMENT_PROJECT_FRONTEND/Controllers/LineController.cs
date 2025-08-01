using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles = "ADMIN,CHIEF")]
    public class LineController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public LineController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = options.Value.BaseUrl;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetLineCard([FromForm] string lineCode)
        {
            if (string.IsNullOrWhiteSpace(lineCode))
                return BadRequest("Line code is required.");

            var formContent = new FormUrlEncodedContent(new[]
            {
             new KeyValuePair<string, string>("lineCode", lineCode)
            });

            var response = await _httpClient.PostAsync($"{_baseUrl}/line/find/code", formContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var line = JsonSerializer.Deserialize<LineDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (line == null)
                return NotFound();

            return PartialView("_LineCard", line);
        }

        [HttpPost]
        public async Task<List<LineDto>> GetLineList()
        {
            var requestUrl = $"{_baseUrl}/line/list";

            var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var lines = JsonSerializer.Deserialize<List<LineDto>>(responseString, options);

            return lines;
        }
    }
}
