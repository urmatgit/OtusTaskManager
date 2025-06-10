using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskboardService.Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class ColumnController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetColumnList(Guid taskboardId) 
        {
            return Ok();
        }
    }
}