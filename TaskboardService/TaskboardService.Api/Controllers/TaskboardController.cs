using Microsoft.AspNetCore.Mvc;

namespace TaskboardService.Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class TaskboardController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetTaskboards(Guid projectId) 
        {
            return Ok();
        }
    }
}