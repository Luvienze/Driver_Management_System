using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Exceptions;
using Repositories.Contracts;
using Services.Contracts;

using Task = Entities.Models.Task;

namespace Services
{
    public class TaskManager : ITaskService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public TaskManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public void SaveOrUpdateTask(TaskDto taskDto)
        {
            if (taskDto is null) throw new TaskNotFoundException(taskDto.Id);
            var task = _mapper.Map<Task>(taskDto);

            if(task.Id < 0) 
            { 
                _manager.Task.SaveOrUpdateTask(task);
                _manager.Save();
            }else if (task.Id > 0) 
            { 
                var existingTask = _manager.Task.GetTaskById(task.Id, false);
                if(existingTask is null)
                {
                    string message = $"Task with id {task.Id} not found.";
                    _logger.LogInfo(message);
                    throw new TaskNotFoundException(task.Id);
                }
                var mappedTask = _mapper.Map(task, existingTask);
                _manager.Task.SaveOrUpdateTask(mappedTask);
                _manager.Save();
            }
        }

        public void DeleteTask(int id)
        {
           if(id < 0) throw new ArgumentException("Task ID must be greater than zero.", nameof(id));
            var task = _manager.Task.GetTaskById(id, false);
            if (task is null)
            {
                string message = $"Task with id {id} not found.";
                _logger.LogInfo(message);
                throw new LineNotFoundException(id);
            }
            _manager.Task.Delete(task);
            _manager.Save();
        }

        public IEnumerable<TaskDto> GetAllTasks(bool trackChanges)
        {
            var tasks = _manager.Task.GetAllTasks(trackChanges);
            if (!tasks.Any())
            {
                _logger.LogInfo("No tasks found.");
                return Enumerable.Empty<TaskDto>();
            }
            return tasks.Select(task => _mapper.Map<TaskDto>(task)).ToList();
        }

        public IEnumerable<TaskDto> GetArchivedTaskList(Tasks status)
        {
            if (!Enum.IsDefined(typeof(Tasks), status))
                throw new ArgumentException("Invalid status value.", nameof(status));

            var tasks = _manager.Task.GetArchivedTaskList(status).ToList();

            if (!tasks.Any())
            {
                _logger.LogInfo("No archived tasks found for the specified status.");
                return Enumerable.Empty<TaskDto>();
            }

            return tasks.Select(task => _mapper.Map<TaskDto>(task)).ToList();
        }
        public IEnumerable<TaskDto> GetTaskListByRegistrationNumber(string registrationNumber)
        {
            if (registrationNumber is null) throw new ArgumentNullException(nameof(registrationNumber));
            DateTime startOfDay = DateTime.Today;
            DateTime endOfDay = DateTime.Today.AddDays(1).AddTicks(-1);
            var tasks = _manager.Task.GetTaskListByRegistrationNumber(registrationNumber, startOfDay, endOfDay);
            if (!tasks.Any())
            {
                _logger.LogInfo($"No tasks found for registration number {registrationNumber} in the specified date range.");
                return Enumerable.Empty<TaskDto>();
            }
            return tasks.Select(task => _mapper.Map<TaskDto>(task)).ToList();
        }

        public IEnumerable<TaskDto> GetDailyTaskList(DateTime startOfDay, DateTime endOfDay)
        {
            if (startOfDay > endOfDay) throw new ArgumentException("Start of day must be earlier than end of day.", nameof(startOfDay));
            var tasks = _manager.Task.GetDailyTaskList(startOfDay, endOfDay);
            if (!tasks.Any())
            {
                _logger.LogInfo("No tasks found for the specified date range.");
                return Enumerable.Empty<TaskDto>();
            }
            return tasks.Select(task => _mapper.Map<TaskDto>(task)).ToList();
        }

        public int GetDailyDriverCount(DateTime startOfDay, DateTime endOfDay)
        {
            if(startOfDay > endOfDay) throw new ArgumentException("Start of day must be earlier than end of day.", nameof(startOfDay));
            var tasks = _manager.Task.GetDailyTaskList(startOfDay, endOfDay);
            if (!tasks.Any())
            {
                _logger.LogInfo("No tasks found for the specified date range.");
                return 0;
            }
            return tasks.Select(t => t.DriverId).Distinct().Count();
        }

        public int GetDailyTaskCount(DateTime startOfDay, DateTime endOfDay)
        {
            if(startOfDay > endOfDay) throw new ArgumentException("Start of day must be earlier than end of day.", nameof(startOfDay));
            var tasks = _manager.Task.GetDailyTaskList(startOfDay, endOfDay);
            if (!tasks.Any())
            {
                _logger.LogInfo("No tasks found for the specified date range.");
                return 0;
            }
            return tasks.Count();
        }

        public int GetPlannedTaskCount(DateTime startOfDay, DateTime endOfDay)
        {
            if(startOfDay > endOfDay) throw new ArgumentException("Start of day must be earlier than end of day.", nameof(startOfDay));
            int taskCount = _manager.Task.GetPlannedTaskCount(startOfDay, endOfDay);
            if(taskCount < 0)
            {
                _logger.LogInfo("No planned tasks found for the specified date range.");
                return 0;
            }
            return taskCount;
        }

        public TaskDto GetTaskById(int id)
        {
            if(id <= 0) throw new ArgumentException("Task ID must be greater than zero.", nameof(id));
            var task = _manager.Task.GetTaskById(id, false);
            if (task is null)
            {
                string message = $"Task with id {id} not found.";
                _logger.LogInfo(message);
                throw new LineNotFoundException(id);
            }
            return _mapper.Map<TaskDto>(task);

        }

        public TaskDto GetTaskByRegistrationNumber(string registrationNumber)
        {
            if(registrationNumber is null) throw new ArgumentNullException(nameof(registrationNumber));
            DateTime startOfDay = DateTime.Today;
            DateTime endOfDay = DateTime.Today.AddDays(1).AddTicks(-1);
            var task = _manager.Task.GetTaskByRegistrationNumber(registrationNumber, startOfDay,endOfDay);
            if (task is null)
            {
                string message = $"Task with registration number {registrationNumber} not found.";
                _logger.LogInfo(message);
                throw new ArgumentException(message, nameof(registrationNumber));
            }
            return _mapper.Map<TaskDto>(task);
        }

  
    }
}
