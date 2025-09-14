using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;
using Repository.Implementation.Extension;

namespace Repository.Implementation
{
    public class BoardColumnRepository(DatabaseContext context) : Repository<BoardColumn, Guid>(context), IBoardColumnRepository
    {
        public async Task<List<BoardColumn>> GetFilteredAsync(Guid taskBoardId, IPageFilter filter)
        {
            var query = GetAll(true)
                    .Where(x => x.TaskBoardId == taskBoardId)
                    .OrderBy(x => x.CreateDate)
                    .AddFilter(filter);

            return await GetQueryAsync(query);
        }
    }
}
