using Entities.DataTransferObjects;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ITaskService
    {
        void SaveOrUpdateTask(TaskDto taskDto);
        void DeleteTask(int id);
        IEnumerable<TaskDto> GetAllTasks(bool trackChanges);
        IEnumerable<TaskDto> GetArchivedTaskList(Tasks status);
        IEnumerable<TaskDto> GetDailyTaskList(DateTime startOfDay, DateTime endOfDay);
        IEnumerable<TaskDto> GetTaskListByRegistrationNumber(string registrationNumber);
        TaskDto GetTaskById(int id);
        TaskDto GetTaskByRegistrationNumber(string registrationNumber);
        int GetDailyDriverCount(DateTime startOfDay, DateTime endOfDay);
        int GetDailyTaskCount(DateTime startOfDay, DateTime endOfDay);
        int GetPlannedTaskCount(DateTime startOfDay, DateTime endOfDay);
    }
}
