using Data.Entities;

namespace Domain.Services.Abstractions
{
    public interface ITaskService
    {
        Task<IEnumerable<UserTask>> GetAllTasks();
    }
}
