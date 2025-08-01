using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _baseUrl;
        public AdminController(IHttpClientFactory httpClientFactory, UserManager<ApplicationUser> userManager, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _userManager = userManager;
            _baseUrl = options.Value.BaseUrl;

        }
        public IActionResult Index()
        {
            return View();
        }


        // DRIVERS PAGE CONTROLLER
        [HttpGet]
        public async Task<IActionResult> DriverIndex()
        {
            return View( "AdminDrivers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetDriversAjax()
        {
            var drivers = await GetDrivers();
            return Json(drivers);
        }

        private async Task<List<DriverDto>> GetDrivers()
        {
            var requestUrl = $"{_baseUrl}/driver/list";

            var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var drivers = JsonSerializer.Deserialize<List<DriverDto>>(responseString, options);
            return drivers;
        }

        // DAILY TASKS

        [HttpGet]
        public async Task<IActionResult> TaskIndex()
        {
            return View("AdminDailyTasks");
        }

        [HttpPost]
        public async Task<IActionResult> GetDailyTasksAjax()
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


        // ARCHIVED TASKS

        [HttpGet]
        public async Task<IActionResult> ArchivedTasksIndex()
        {
            return View("AdminArchivedTasks");
        }
        [HttpPost]
        public async Task<IActionResult> GetArchivedTasksAjax()
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

        // LINES

        [HttpGet]
        public async Task<IActionResult> LineIndex()
        {
            return View("AdminLines");
        }
        [HttpPost]
        public async Task<IActionResult> GetLinesAjax()
        {
            try
            {
                var lines = await GetLinesData();
                return Json(lines ?? new List<LineDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error has been occured during fetching lines: {ex.Message}" });
            }
        }

        private async Task<List<LineDto>> GetLinesData()
        {
            var requestUrl = $"{_baseUrl}/line/list";
            var response = await _httpClient.PostAsync(requestUrl, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Type: {errorContent}");
                return new List<LineDto>();
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
                var lines = JsonSerializer.Deserialize<List<LineDto>>(responseString, options);
                Console.WriteLine("Deserialized lines: " + (lines != null ? lines.Count : "null"));
                return lines ?? new List<LineDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Serialization error: " + ex.Message);
                return new List<LineDto>();
            }
        }

        // Routes

        [HttpGet]
        public async Task<IActionResult> RouteIndex()
        {
            return View("AdminRoutes");
        }
        [HttpPost]
        public async Task<IActionResult> GetRoutesAjax()
        {
            try
            {
                var routes = await GetRoutesData();
                return Json(routes ?? new List<RouteDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error has been occured during fetching routes: {ex.Message}" });
            }
        }

        private async Task<List<RouteDto>> GetRoutesData()
        {
            var requestUrl = $"{_baseUrl}/route/list";
            var response = await _httpClient.PostAsync(requestUrl, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Type: {errorContent}");
                return new List<RouteDto>();
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
                var routes = JsonSerializer.Deserialize<List<RouteDto>>(responseString, options);
                Console.WriteLine("Deserialized routes: " + (routes != null ? routes.Count : "null"));
                return routes ?? new List<RouteDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Serialization error: " + ex.Message);
                return new List<RouteDto>();
            }
        }

        // Vehicles

        [HttpGet]
        public async Task<IActionResult> VehicleIndex()
        {
            return View("AdminVehicles");
        }
        [HttpPost]
        public async Task<IActionResult> GetVehiclesAjax()
        {
            try
            {
                var vehicles = await GetVehiclesData();
                return Json(vehicles ?? new List<VehicleDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error has been occured during fetching vehicle: {ex.Message}" });
            }
        }

        private async Task<List<VehicleDto>> GetVehiclesData()
        {
            var requestUrl = $"{_baseUrl}/vehicle/list";
            var response = await _httpClient.PostAsync(requestUrl, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Type: {errorContent}");
                return new List<VehicleDto>();
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
                var vehicles = JsonSerializer.Deserialize<List<VehicleDto>>(responseString, options);
                Console.WriteLine("Deserialized vehicles: " + (vehicles != null ? vehicles.Count : "null"));
                return vehicles ?? new List<VehicleDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Serialization error: " + ex.Message);
                return new List<VehicleDto>();
            }
        }

    }
}
