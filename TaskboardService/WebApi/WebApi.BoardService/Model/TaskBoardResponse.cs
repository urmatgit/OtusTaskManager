using Services.Contract.TaskBoard;

namespace WebApi.BoardService.Model
{
    public record class TaskBoardResponse: FilteredResponse<List<TaskBoardDto>>
    {
    }
}
