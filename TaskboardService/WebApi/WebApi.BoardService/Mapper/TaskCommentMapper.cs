using BoardService.Domain.Entity;
using Services.Contract.TaskComment;

namespace WebApi.BoardService.Mapper
{
    /// <summary>
    /// TaskComment Mapper
    /// </summary>
    public class TaskCommentMapper
    {
        /// <summary>
        /// Map from TaskComment
        /// </summary>
        /// <param name="taskComment"></param>
        /// <returns></returns>
        public static TaskCommentDto MapFromModel(TaskComment taskComment)
        {
            return new TaskCommentDto()
            {
                CreateDate = taskComment.CreateDate,
                Id = taskComment.Id,
                Author = taskComment.Author,
                TaskId = taskComment.TaskId,
                Text = taskComment.Text
            };
        }
    }
}
