using BoardService.Domain.Entity;
using Services.Contract.TaskBoard;

namespace WebApi.BoardService.Mapper
{
    /// <summary>
    /// TaskBoard mapper
    /// </summary>
    public class TaskBoardMapper
    {
        /// <summary>
        /// Map from TaskBoard
        /// </summary>
        /// <param name="taskBoard"></param>
        /// <returns></returns>
        public static TaskBoardDto MapFromModel(TaskBoard taskBoard)
        {
            return new TaskBoardDto() { 
                CreateDate = taskBoard.CreateDate,
                Id = taskBoard.Id,
                Name = taskBoard.Name,
                Status = (int)taskBoard.Status,
            };
        }
    }
}
