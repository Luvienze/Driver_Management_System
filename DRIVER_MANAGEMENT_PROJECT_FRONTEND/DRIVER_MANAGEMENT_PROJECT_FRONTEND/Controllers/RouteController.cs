using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
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
    public class RouteController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RouteController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = options.Value.BaseUrl;
        }

        /// <summary>
        ///     Returns Route/Index.cshtml.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Displays a card modal that shows information about route by it's name
        ///     Calls route/find/naöe api and sends route name, returns partial view.
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> GetRouteCard([FromForm] string routeName)
        {
            if (string.IsNullOrWhiteSpace(routeName))
                return BadRequest("Route name is required.");

            var formContent = new FormUrlEncodedContent(new[]
            {
             new KeyValuePair<string, string>("routeName", routeName)
            });

            var response = await _httpClient.PostAsync($"{_baseUrl}/route/find/name", formContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var route = JsonSerializer.Deserialize<RouteDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (route == null)
                return NotFound();

            return PartialView("_RouteCard", route);
        }

        /// <summary>
        ///     Retrieves all route list.
        ///     Calls route/list api and returns a list of routes.
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> GetRouteList()
        {
            var requestUrl = $"{_baseUrl}/route/list";

            var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Java API hatası");

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var routes = JsonSerializer.Deserialize<List<RouteDto>>(responseString, options);

            return Ok(routes);
        }

    }
}
