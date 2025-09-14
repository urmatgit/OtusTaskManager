using Microsoft.AspNetCore.Mvc;
using Repository.Abstractions;

namespace WebApi.BoardService.Controllers
{
    /// <summary>
    /// 
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
