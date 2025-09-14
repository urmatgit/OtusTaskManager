using BoardService.Domain.Entity;
using Services.Contract.CheckList;

namespace WebApi.BoardService.Mapper
{
    /// <summary>
    /// CheckList Mapper
    /// </summary>
    public class CheckListMapper
    {
        /// <summary>
        /// Map from CheckList
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static CheckListDto MapFromModel(CheckList column)
        {
            return new CheckListDto()
            {                
                Id = column.Id,
                Name = column.Name,             
                TaskId = column.TaskId,
            };
        }
    }
}
