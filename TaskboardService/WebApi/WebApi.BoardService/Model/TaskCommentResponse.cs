using Services.Contract.TaskComment;

namespace WebApi.BoardService.Model
{
    /// <summary>
    /// TaskComment Response
    /// </summary>
    public record class TaskCommentResponse : FilteredResponse<List<TaskCommentDto>>
    {
    }
}
