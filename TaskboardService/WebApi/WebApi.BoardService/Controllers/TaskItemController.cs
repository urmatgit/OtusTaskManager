using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="taskItemRepository"></param>
    public class TaskItemController(
        ITaskItemRepository taskItemRepository
        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
