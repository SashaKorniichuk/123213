using AutoMapper;
using Data.Entities;
using Data.Interfaces;
using Domain.Services.Abstractions;

namespace Domain.Services
{
    public class TaskService:ITaskService
    {
        private readonly IGenericRepository<UserTask> _tasks;
        private readonly IMapper _mapper;
        public TaskService(IGenericRepository<UserTask> tasks ,IMapper mapper)
        {
            _tasks = tasks;
            _mapper = mapper;   
        }

        public async Task<IEnumerable<UserTask>> GetAllTasks()
        {
            var tasks = await _tasks.GetAllTaskAsync();

            return _mapper.Map<IEnumerable<UserTask>>(tasks);
        }
    }
}
