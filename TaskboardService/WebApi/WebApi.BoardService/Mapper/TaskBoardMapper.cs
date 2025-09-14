using BoardService.Domain.Entity;
using Services.Contract.TaskBoard;

namespace WebApi.BoardService.Mapper
{
    public class TaskBoardMapper
    {
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
