using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskboardService.Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        [HttpGet("ItemList/{taskboardId:guid}")]
        public async Task<IActionResult> GetTaskItemList(Guid taskboardId) 
        {
            return Ok();
        }

        [HttpGet("ItemColumnList/{taskboardId:guid}")]
        public async Task<IActionResult> GetTaskItemColumnList(Guid taskboardId)
        {
            return Ok();
        }


    }
}