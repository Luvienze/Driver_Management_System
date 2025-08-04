using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Task = Entities.Models.Task;
namespace Repositories.EFCore
{
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(RepositoryContext context) : base(context)
        {
        }
        public void SaveOrUpdateTask(Task task)
        {
            if (task.Id <= 0)
                Create(task);
            else
                Update(task);
        }
        public void DeleteTask(Task task) => Delete(task);
        public IQueryable<Task> GetAllTasks(bool trackChanges) =>
         _context.Tasks
             .Include(t => t.Driver)
                 .ThenInclude(d => d.Person)
             .Include(t => t.Vehicle)
             .Include(t => t.Route)
             .Include(t => t.LineCode)
             .AsNoTracking();
        public IQueryable<Task> GetTaskListByRegistrationNumber(string registrationNumber, DateTime startOfDay, DateTime endOfDay) =>
          _context.Tasks
              .Include(t => t.Driver).ThenInclude(d => d.Person)
              .Include(t => t.Vehicle)
              .Include(t => t.Route)
              .Include(t => t.LineCode)
              .Where(t => t.Driver.Person.RegistrationNumber == registrationNumber
                          && t.DateOfStart >= startOfDay && t.DateOfStart <= endOfDay)
              .AsNoTracking();
        public IQueryable<Task> GetDailyTaskList(DateTime startOfDay, DateTime endOfDay) =>
            _context.Tasks
                .Include(t => t.Driver).ThenInclude(d => d.Person)
                .Include(t => t.Vehicle)
                .Include(t => t.Route)
                .Include(t => t.LineCode)
                .Where(t => t.DateOfStart >= startOfDay && t.DateOfStart < endOfDay)
                .AsNoTracking();
        public IQueryable<Task> GetArchivedTaskList(Tasks status) =>
              _context.Tasks
                  .Include(t => t.Driver).ThenInclude(d => d.Person)
                  .Include(t => t.Vehicle)
                  .Include(t => t.Route)
                  .Include(t => t.LineCode)
                  .Where(t => t.Status == status)
                  .AsNoTracking();

        public Task GetTaskByRegistrationNumber(string registrationNumber, DateTime startOfDay, DateTime endOfDay) =>
          _context.Tasks
              .Include(t => t.Driver).ThenInclude(d => d.Person)
              .Include(t => t.Vehicle)
              .Include(t => t.Route)
              .Include(t => t.LineCode)
              .FirstOrDefault(t => t.Driver.Person.RegistrationNumber == registrationNumber
                                 && t.DateOfStart >= startOfDay && t.DateOfStart < endOfDay);
        public Task GetTaskById(int taskId, bool trackChanges) =>
            _context.Tasks
                .Include(t => t.Driver).ThenInclude(d => d.Person)
                .Include(t => t.Vehicle)
                .Include(t => t.Route)
                .Include(t => t.LineCode)
                .FirstOrDefault(t => t.Id == taskId);

        public int GetDailyDriverCount(DateTime startOfDay, DateTime endOfDay) =>
            FindByCondition(t => t.DateOfStart >= startOfDay && t.DateOfStart < endOfDay, false)
            .Select(t => t.Driver.Id)
            .Distinct()
            .Count();

        public int GetDailyTaskCount(DateTime startOfDay, DateTime endOfDay) =>
            FindByCondition(t => t.DateOfStart >= startOfDay && t.DateOfStart < endOfDay, false)
            .Count();

        public int GetPlannedTaskCount(DateTime startOfDay, DateTime endOfDay) =>
            FindByCondition(t => t.DateOfStart >= startOfDay && t.DateOfStart < endOfDay && t.Status == Entities.Enums.Tasks.PLANNED, false)
           .Count();

    }
}
