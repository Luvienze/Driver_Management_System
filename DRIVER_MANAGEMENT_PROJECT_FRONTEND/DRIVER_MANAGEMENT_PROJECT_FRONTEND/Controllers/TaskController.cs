using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles = "ADMIN,CHIEF")]
    public class TaskController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TaskController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = options.Value.BaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await GetDailyTasks();
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> GetDailyTasks()
        {
            try
            {
                var tasks = await GetDailyTasksData();
                return Json(tasks ?? new List<TaskDto>());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Top level controller exception: " + ex.Message);
                return StatusCode(500, new { error = $"An error has been occured: {ex.Message}" });
            }
        }

        public async Task<List<TaskDto>> GetDailyTasksData()
        {
            var requestUrl = $"{_baseUrl}/task/daily/tasks";
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

        public async Task<IActionResult> GetDailyTaskCount()
        {
            var springApiUrl = $"{_baseUrl}/task/daily/count";

            var startOfDay = DateTime.Now.Date.ToString("yyyy-MM-ddTHH:mm:ss");
            var endOfDay = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss");

            var requestUrl = $"{springApiUrl}?startOfDay={startOfDay}&endOfDay={endOfDay}";

            var response = await _httpClient.PostAsync(requestUrl, null);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Spring API error");
            }

            var content = await response.Content.ReadAsStringAsync();

            int count = JsonConvert.DeserializeObject<int>(content);

            return Ok(count);
        }

        public async Task<IActionResult> GetDailyDriverCount()
        {
            var springApiUrl = $"{_baseUrl}/task/daily/driver";

            var startOfDay = DateTime.Now.Date.ToString("yyyy-MM-dd'T'HH:mm:ss");
            var endOfDay = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd'T'HH:mm:ss");

            var requestUrl = $"{springApiUrl}?startOfDay={Uri.EscapeDataString(startOfDay)}&endOfDay={Uri.EscapeDataString(endOfDay)}";

            var response = await _httpClient.PostAsync(requestUrl, null);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Spring API error");
            }

            var content = await response.Content.ReadAsStringAsync();

            int count = JsonConvert.DeserializeObject<int>(content);

            return Ok(count);
        }

        public async Task<IActionResult> GetDailyPlannedTaskCount()
        {
            var springApiUrl = $"{_baseUrl}/task/daily/plannedTask";

            var startOfDay = DateTime.Now.Date.ToString("yyyy-MM-dd'T'HH:mm:ss");
            var endOfDay = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd'T'HH:mm:ss");
            var status = "PLANNED";
            var requestUrl = $"{springApiUrl}?startOfDay={Uri.EscapeDataString(startOfDay)}&endOfDay={Uri.EscapeDataString(endOfDay)}&status={status}";


            var response = await _httpClient.PostAsync(requestUrl, null);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Spring API error");
            }

            var content = await response.Content.ReadAsStringAsync();

            int count = JsonConvert.DeserializeObject<int>(content);

            return Ok(count);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
                return BadRequest("TaskDto is null!");

            var requestUrl = $"{_baseUrl}/task/saveOrUpdate";
            var response = await _httpClient.PostAsJsonAsync(requestUrl, taskDto);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, responseContent);

            return Ok("Task added successfully");
        }
        
        [HttpPost]
        public IActionResult GetAddTaskModalHTML()
        {
            try
            {
                return PartialView("_AddTask");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving modal HTML: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            try
            {
                if (taskId <= 0)
                    return BadRequest("Task Id is required.");

                string requestUrl = $"{_baseUrl}/task/find/id?taskId={taskId}";

                var response = await _httpClient.PostAsync(requestUrl, null);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Failed to retrieve task data: {response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                    return NotFound("Task data is empty.");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var task = JsonSerializer.Deserialize<TaskDto>(responseString, options);
                if (task == null)
                    return NotFound("Task not found.");

                return Ok(task);
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Error deserializing task data: {ex.Message}");
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
        public async Task<IActionResult> GetEditTaskModalHtml(int taskId)
        {
            try
            {
                if (taskId <= 0)
                    return BadRequest("Task Id is required.");

                var taskResult = await GetTaskById(taskId);
                if (taskResult is not OkObjectResult okResult)
                    return taskResult;

                var task = okResult.Value as TaskDto;
                if (task == null)
                    return NotFound("Task data is invalid.");

                return PartialView("_EditTask", task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving modal HTML: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest("TaskDto is null.");
            }
            if (string.IsNullOrWhiteSpace(taskDto.RegistrationNumber))
            {
                return BadRequest("RegistrationNumber is empty or null.");
            }

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(taskDto, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }),
                Encoding.UTF8,
                "application/json"
            );

            var requestUrl = $"{_baseUrl}/task/saveOrUpdate";
            var response = await _httpClient.PostAsync(requestUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }

            return Ok();
        }
    }
}