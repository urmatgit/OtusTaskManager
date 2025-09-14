using BoardService.Domain.Entity;

namespace WebApi.BoardService.Model
{
    public record class TaskBoardResponse: FilteredResponse<List<TaskBoard>>
    {
    }
}
