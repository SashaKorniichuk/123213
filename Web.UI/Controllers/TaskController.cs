using Domain.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _service;
        public TaskController(ITaskService service)
        {
            _service=service;   
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result=await _service.GetAllTasks();

            return Ok(result);
        }
    }
}
