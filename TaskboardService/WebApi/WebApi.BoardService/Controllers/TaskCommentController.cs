using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер
    /// </summary>
    /// <param name="taskCommentRepository"></param>
    public class TaskCommentController(
        ITaskCommentRepository taskCommentRepository
        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
