using Services.Contract.TaskItem;

namespace WebApi.BoardService.Model
{
    /// <summary>
    /// 
    /// </summary>
    public record class TaskItemResponse : FilteredResponse<List<TaskItemDto>>
    {
    }
}
