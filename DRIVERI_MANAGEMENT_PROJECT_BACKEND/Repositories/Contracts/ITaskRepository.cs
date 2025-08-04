using Entities.Enums;
using Entities.Models;
using Task = Entities.Models.Task;

namespace Repositories.Contracts
{
    public interface ITaskRepository : IRepositoryBase<Task>
    {
        IQueryable<Task> GetAllTasks(bool trackChanges);
        IQueryable<Task> GetTaskListByRegistrationNumber(string registrationNumber, DateTime startOfDay, DateTime endOfDay);
        IQueryable<Task> GetArchivedTaskList(Tasks status);
        IQueryable<Task> GetDailyTaskList(DateTime startOfDay, DateTime endOfDay);
        Task GetTaskById(int taskId, bool trackChanges);
        Task GetTaskByRegistrationNumber(string registrationNumber, DateTime startOfDay, DateTime endOfDay);
        void SaveOrUpdateTask(Task task);
        void DeleteTask(Task task);
        int GetDailyTaskCount(DateTime startOfDay, DateTime endOfDay);
        int GetDailyDriverCount(DateTime startOfDay, DateTime endOfDay);
        int GetPlannedTaskCount(DateTime startOfDay, DateTime endOfDay);
    }
}
