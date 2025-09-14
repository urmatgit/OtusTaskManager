using BoardService.Domain.Abstraction;
using BoardService.Domain.Entity;
using Infrastructure.EntityFramework;
using Repository.Abstractions;
using Repository.Implementation.Extension;

namespace Repository.Implementation
{
    public class TaskBoardRepository(DatabaseContext context) : Repository<TaskBoard, Guid>(context), ITaskBoardRepository
    {
        public async Task<List<TaskBoard>> GetFilteredAsync(IPageFilter filter)
        {
            var query = GetAll(true)
                    .OrderBy(x => x.CreateDate)
                    .AddFilter(filter);

            return await GetQueryAsync(query);
        }
    }
}
