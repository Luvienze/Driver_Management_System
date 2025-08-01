using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Data;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dto;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Dtos;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Users;
using DRIVER_MANAGEMENT_PROJECT_FRONTEND.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonException = System.Text.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DRIVER_MANAGEMENT_PROJECT_FRONTEND.Controllers
{
    [Authorize(Roles ="ADMIN,CHIEF, DRIVER")]
    public class DriverController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _baseUrl;

        public DriverController(IHttpClientFactory httpClientFactory, UserManager<ApplicationUser> userManager, IOptions<ApiSettings> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _userManager = userManager;
            _baseUrl = options.Value.BaseUrl;
        }
        public async Task<IActionResult> Index()
        {
            var drivers = await GetDriverAsync();
            return View(drivers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetDrivers()
        {
            var drivers = await GetDriverAsync();
            return Json(drivers);
        }
        private async Task<List<DriverDto>> GetDriverAsync()
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
        private async Task<DriverDto?> GetDriverByRegistrationNumberAsync(string registrationNumber)
        {
            var requestUrl = $"{_baseUrl}/driver/list";

            var content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUrl, content);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var drivers = JsonSerializer.Deserialize<List<DriverDto>>(responseString, options);

            return drivers?.FirstOrDefault(d => d.RegistrationNumber == registrationNumber);
        }

        [HttpPost]
        public async Task<IActionResult> GetDriverByRegistrationNumber(string registrationNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                    return BadRequest("Registration number is required.");

                var requestUrl = $"{_baseUrl}/driver/find/registrationNumber?registrationNumber={Uri.EscapeDataString(registrationNumber)}";

                var response = await _httpClient.PostAsync(requestUrl, null); 
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Failed to retrieve driver data: {response.ReasonPhrase}");

                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                    return NotFound("Driver data is empty.");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var driver = JsonSerializer.Deserialize<DriverDto>(responseString, options);
                if (driver == null)
                    return NotFound("Driver not found.");

                return Ok(driver);
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Error deserializing driver data: {ex.Message}");
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
        public async Task<IActionResult> GetDriverModalHtml(string registrationNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registrationNumber))
                    return BadRequest("Registration number is required.");

                var driverResult = await GetDriverByRegistrationNumber(registrationNumber);
                if (driverResult is not OkObjectResult okResult)
                    return driverResult; 

                var driver = okResult.Value as DriverDto;
                if (driver == null)
                    return NotFound("Driver data is invalid.");

                return PartialView("_EditDriver", driver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving modal HTML: {ex.Message}");
            }
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

        [HttpPost]
        public async Task<IActionResult> ToggleActive([FromForm] string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                return BadRequest();


            var driverDto = await GetDriverByRegistrationNumberAsync(registrationNumber);
            if (driverDto == null)
                return NotFound();

            driverDto.IsActive = !driverDto.IsActive;

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(driverDto),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/driver/set/active", jsonContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var updatedJson = await response.Content.ReadAsStringAsync();

            var updatedDriver = JsonSerializer.Deserialize<DriverDto>(updatedJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Json(new { isActive = updatedDriver.IsActive });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                return BadRequest("Registration number is required");

            var formContent = new FormUrlEncodedContent(new[]
            {
        new KeyValuePair<string, string>("registrationNumber", registrationNumber)
    });

            var response = await _httpClient.PostAsync($"{_baseUrl}/person/delete", formContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetTaskListCard([FromForm] string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                return BadRequest("Registration number is required.");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("registrationNumber", registrationNumber)
            });

            var response = await _httpClient.PostAsync($"{_baseUrl}/task/list/driver/assigned", formContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                // API boş içerik döndü, yani görev yok
                return PartialView("_TaskNotFound");
            }

            List<TaskDto> task;
            try
            {
                task = JsonSerializer.Deserialize<List<TaskDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                });
            }
            catch (JsonException)
            {
              
                        return PartialView("_TaskNotFound");
            }

                    if (task == null || !task.Any())
                        return PartialView("_TaskNotFound");

            return PartialView("_TaskListCard", task);
        }

        public async Task<IActionResult> GetTasksByDriver([FromBody] string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                return BadRequest("Registration number is required.");

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(new { registrationNumber }),
                Encoding.UTF8,
                "application/json");

            var url = $"{_baseUrl}/task/list/driver/assigned?registrationNumber={registrationNumber}";
            var response = await _httpClient.PostAsync(url, null);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
                return Ok(new List<TaskDto>());

            List<TaskDto> tasks;
            try
            {
                tasks = JsonSerializer.Deserialize<List<TaskDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                });
            }
            catch (JsonException)
            {
                return Ok(new List<TaskDto>());
            }

            return Ok(tasks ?? new List<TaskDto>());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDriver([FromBody] DriverDto driverDto)
        {
            if (driverDto == null)
            {
                return BadRequest("DriverDto is null.");
            }
            if (string.IsNullOrWhiteSpace(driverDto.RegistrationNumber))
            {
                return BadRequest("RegistrationNumber is empty or null.");
            }

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(driverDto, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }),
                Encoding.UTF8,
                "application/json"
            );

            var requestUrl = $"{_baseUrl}/driver/update";
            var response = await _httpClient.PostAsync(requestUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewDriver(IFormFile imageFile, [FromForm(Name ="data")] string jsonData)
        {
            var personDriverRequestDto = JsonSerializer.Deserialize<PersonDriverRequestDto>(jsonData);

            if (personDriverRequestDto == null)
                return BadRequest("PersonDriverRequestDto is null!");

            var requestUrl = $"{_baseUrl}/driver/add";
            using var formData = new MultipartFormDataContent();

            // JSON partı ekle
            var json = JsonSerializer.Serialize(personDriverRequestDto);
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            formData.Add(jsonContent, "data");

            // Image partı ekle
            if (imageFile != null && imageFile.Length > 0)
            {
                var streamContent = new StreamContent(imageFile.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
                formData.Add(streamContent, "image", imageFile.FileName);
            }

            var response = await _httpClient.PostAsync(requestUrl, formData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }

            var addedPerson = await response.Content.ReadFromJsonAsync<PersonDto>();

            var user = new ApplicationUser
            {
                UserName = personDriverRequestDto.PersonDto.Phone,
                PersonId = addedPerson.Id,
                PhoneNumber = personDriverRequestDto.PersonDto.Phone,
                Email = $"{personDriverRequestDto.PersonDto.Phone}@mail.com",
                RegistrationNumber = personDriverRequestDto.PersonDto.RegistrationNumber
            };

            var password = personDriverRequestDto.PersonDto.RegistrationNumber;

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                Console.WriteLine(string.Join(",", errors));
                return BadRequest(new { message = "Identity user creation failed", errors });
            }

            await _userManager.AddToRoleAsync(user, "DRIVER");

            return Ok(new
            {
                message = "Driver added successfully",
                personId = addedPerson.Id
            });
        }

        [HttpPost]
        public IActionResult GetAddDriverModalHTML()
        {
            try
            {
                return PartialView("_AddDriver");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving modal HTML: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetDailyDayOffDriverCount([FromForm] string day)
        {
            if (string.IsNullOrEmpty(day))
                return BadRequest("Day parameter is empty.");

            if (!DateTime.TryParseExact(day, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return BadRequest("Unrecognized date format. Please send data in yyyy-MM-dd format.");
            }

            var backendUrl = $"{_baseUrl}/driver/daily/dayOff";

            try
            {
                var parameters = new Dictionary<string, string>
        {
            { "day", day }
        };

                var content = new FormUrlEncodedContent(parameters);

                var response = await _httpClient.PostAsync(backendUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Ok(new { count = result });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Backend error: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Could not connect to backend: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetDailyFemaleDriverCount()
        {
            const int femaleGender = 1;
            var springApiUrl = $"{_baseUrl}/driver/daily/gender?gender={femaleGender}";

            var response = await _httpClient.PostAsync(springApiUrl, null);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Spring API error");
            }

            var content = await response.Content.ReadAsStringAsync();
            int count = JsonConvert.DeserializeObject<int>(content);

            return Ok(count);
        }

        public async Task<IActionResult> GetDailyMaleDriverCount()
        {
            const int maleGender = 0; 
            var springApiUrl = $"{_baseUrl}/driver/daily/gender?gender={maleGender}";

            var response = await _httpClient.PostAsync(springApiUrl, null);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Spring API error");
            }

            var content = await response.Content.ReadAsStringAsync();
            int count = JsonConvert.DeserializeObject<int>(content);

            return Ok(count);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var uploadsFolder = Path.Combine("wwwroot", "uploaded-images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(fileName);
        }
    }
}
