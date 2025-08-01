using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles = "ADMIN, CHIEF")]
    public class ArchivedTaskController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public ArchivedTaskController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = options.Value.BaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await GetArchivedTasks();
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> GetArchivedTasks()
        {
            try
            {
                var tasks = await GetArchivedTasksData();
                return Json(tasks ?? new List<TaskDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error has been occured during fetchin archived tasks: {ex.Message}" });
            }
        }

        private async Task<List<TaskDto>> GetArchivedTasksData()
        {
            var requestUrl = $"{_baseUrl}/task/list/archived";
            var response = await _httpClient.PostAsync(requestUrl, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Type: {errorContent}");
                return new List<TaskDto>();
            }

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Backend response: " + responseString);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            try
            {
                var tasks = JsonSerializer.Deserialize<List<TaskDto>>(responseString, options);
                Console.WriteLine("Deserialized tasks: " + (tasks != null ? tasks.Count : "null")); 
                return tasks ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Serialization error: " + ex.Message); 
                return new List<TaskDto>();
            }
        }
    }
}
