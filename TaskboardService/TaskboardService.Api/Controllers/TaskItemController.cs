using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskboardService.Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetTaskItemList(Guid taskboardId) 
        {
            return Ok();
        }
    }
}
