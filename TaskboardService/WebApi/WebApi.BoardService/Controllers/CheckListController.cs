using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// Контроллер
    /// </summary>
    /// <param name="checkListRepository"></param>
    public class CheckListController(
        ICheckListRepository checkListRepository
        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
