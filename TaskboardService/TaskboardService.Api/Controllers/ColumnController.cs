using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TaskboardService.Api.Models;
using TaskboardService.Business.Services.Abstract;
using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class ColumnController(ITaskboardService taskboardService, ITaskItemService taskItemService) : ControllerBase
    {
        private readonly ITaskboardService _taskboardService = taskboardService;
        private readonly ITaskItemService _taskItemService = taskItemService;

        [HttpPost("Item/{taskboardId:guid}")]
        public async Task<IActionResult> AddColumnAsync(Guid taskboardId, [FromBody()] TaskboardColumnDto columnDto)
        {
            var entity = new TaskboardColumn()
            {
                Id = Guid.NewGuid()                
            };
            columnDto.ToEntity(entity);
            await _taskboardService.AddColumnAsync(taskboardId, entity);
            columnDto.Id = entity.Id;

            return Ok(columnDto);
        }

        
    }
}