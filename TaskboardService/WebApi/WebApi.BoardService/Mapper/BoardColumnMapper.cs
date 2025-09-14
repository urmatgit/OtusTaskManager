using BoardService.Domain.Entity;
using Services.Contract.BorderColumn;

namespace WebApi.BoardService.Mapper
{
    public class BoardColumnMapper
    {
        public static BoardColumnDto MapFromModel(BoardColumn column)
        {
            return new BoardColumnDto()
            {
                CreateDate = column.CreateDate,
                ColumnType = (int)column.ColumnType,
                Id = column.Id,
                Name = column.Name,
                TaskBoardId = column.TaskBoardId,
                VipLimit = column.VipLimit,
            };
        }
    }
}
