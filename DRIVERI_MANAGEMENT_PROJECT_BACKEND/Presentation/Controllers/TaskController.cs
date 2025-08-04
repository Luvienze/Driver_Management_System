using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contracts;
using Entities.Enums;

namespace Presentation.Controllers
{
    [Route("/task")]
    public class TaskController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public TaskController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult GetAllTasks()
        {
            try
            {
                _manager.TaskService.GetAllTasks(false);
                return Ok(_manager.TaskService.GetAllTasks(false));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("/find/registrationNumber")]
        public IActionResult GetTaskByRegistrationNumber([FromQuery] string registrationNumber)
        {
            try
            {
                var task = _manager.TaskService.GetTaskByRegistrationNumber(registrationNumber);
                if (task == null)
                {
                    return NotFound($"Task with registration number {registrationNumber} not found.");
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("list/driver")]
        public IActionResult GetListByRegistrationNumber([FromQuery] string registrationNumber)
        {
            try
            {
                var tasks = _manager.TaskService.GetTaskByRegistrationNumber(registrationNumber);
                if (tasks is null)
                {
                    return NotFound($"No tasks found for vehicle with registration number {registrationNumber}.");
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("find/id")]
        public IActionResult GetTaskById([FromForm] int id)
        {
            try
            {
                var task = _manager.TaskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound($"Task with ID {id} not found.");
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("list/driver/assigned")]
        public IActionResult GetTaskListByRegistrationNumber([FromForm] string registrationNumber)
        {
            try
            {
                var tasks = _manager.TaskService.GetTaskListByRegistrationNumber(registrationNumber);
                if (tasks is null)
                {
                    return NotFound($"No tasks found for vehicle with registration number {registrationNumber}.");
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("saveOrUpdate")]
        public IActionResult SaveOrUpdateTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest("Task data is null.");
            }
            try
            {
                _manager.TaskService.SaveOrUpdateTask(taskDto);
                return Ok("Task saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("list/archived")]
        public IActionResult GetArchivedTaskList()
        {
            try
            {
                var tasks = _manager.TaskService.GetArchivedTaskList(Tasks.COMPLETED);

                if (!tasks.Any())
                {
                    return NotFound($"No archived tasks found");
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("daily/tasks")]
        public IActionResult GetDailyTaskList()
        {
            try
            {
                var startOfDay = DateTime.Today;
                var endOfDay = DateTime.Today.AddDays(1).AddTicks(-1);
                var tasks = _manager.TaskService.GetDailyTaskList(startOfDay, endOfDay);
                if (tasks is null || !tasks.Any())
                {
                    return NotFound($"No tasks found for today.");
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("daily/count")]
        public IActionResult GetDailyTaskCount([FromQuery] DateTime startOfDay, [FromQuery] DateTime endOfDay)
        {
            try
            {
                if (startOfDay == null) startOfDay = DateTime.Today;
                if (endOfDay == null) endOfDay = DateTime.Today.AddDays(1).AddTicks(-1);
                var count = _manager.TaskService.GetDailyTaskCount(startOfDay, endOfDay);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("daily/driver")]
        public IActionResult GetDailyDriverCount([FromQuery] DateTime startOfDay, [FromQuery] DateTime endOfDay) 
        {
            try
            {
                if (startOfDay == null) startOfDay = DateTime.Today;
                if (endOfDay == null) endOfDay = DateTime.Today.AddDays(1).AddTicks(-1);
                var count = _manager.TaskService.GetDailyDriverCount(startOfDay, endOfDay);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
