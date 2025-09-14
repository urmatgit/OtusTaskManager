using BoardService.Domain.Entity;
using Services.Contract.TaskItem;

namespace WebApi.BoardService.Mapper
{
    /// <summary>
    /// TaskItem mapper
    /// </summary>
    public class TaskItemMapper
    {
        /// <summary>
        /// Map from TaskItem
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns></returns>
        public static TaskItemDto MapFromModel(TaskItem taskItem)
        {
            return new TaskItemDto()
            {                
                CreateDate = taskItem.CreateDate,
                Id = taskItem.Id,
                Name = taskItem.Name,
                BoardColumnId = taskItem.BoardColumnId,
                Description = taskItem.Description,
                FactDate = taskItem.FactDate,
                PlanDate = taskItem.PlanDate,
                Priority = (int)taskItem.Priority,
            };
        }
    }
}
