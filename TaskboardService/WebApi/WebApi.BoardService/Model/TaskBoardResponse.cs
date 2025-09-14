using Services.Contract.TaskBoard;

namespace WebApi.BoardService.Model
{
    /// <summary>
    /// TaskBoard Response
    /// </summary>
    public record class TaskBoardResponse : FilteredResponse<List<TaskBoardDto>>
    {
    }
}
